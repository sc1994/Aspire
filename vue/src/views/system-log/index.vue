<template>
  <div class="app-container">
    <parser v-if="formConfig" :form-conf="formConfig" @submit="query" />

    <el-table v-if="tableConfig" :data="tableData" border style="width: 100%">
      <el-table-column
        v-for="item in tableConfig.cols"
        :key="item.prop"
        :prop="item.prop"
        :label="item.label"
        :min-width="item.minWidth || undefined"
        :show-overflow-tooltip="true"
      >
        <template slot-scope="scope">
          <span v-if="item.prop === 'level'">
            <el-tag v-if="scope.row[item.prop] === 'Information'"> Information </el-tag>
            <el-tag v-else-if="scope.row[item.prop] === 'Warning'" type="warning">
              Warning
            </el-tag>
            <el-tag v-else-if="scope.row[item.prop] === 'Error'" type="danger">
              Error
            </el-tag>
            <el-tag v-else type="info"> {{ scope.row[item.prop] }} </el-tag>
          </span>
          <span v-else>{{ scope.row[item.prop] }}</span>
        </template>
      </el-table-column>
      <el-table-column label="操作" :width="100" fixed="right">
        <template slot-scope="scope">
          <el-button type="text" style="margin-right: 4px" @click="detail(scope.row)">
            详情
          </el-button>
          /<el-button type="text" style="margin-left: 6px" @click="trace(scope.row)">
            追踪
          </el-button>
        </template>
      </el-table-column>
    </el-table>
    <el-pagination
      @size-change="query"
      @current-change="query"
      :current-page.sync="pageIndex"
      :page-sizes="[20, 50, 100, 200]"
      :page-size.sync="pageSize"
      layout="total, sizes, prev, pager, next, jumper"
      :total.sync="totalCount"
    >
    </el-pagination>

    <el-dialog title="详情" :visible.sync="detailDialog" width="70%">
      <json-view v-if="detailJson" :data="detailJson" />
    </el-dialog>
    <el-dialog title="追踪" :visible.sync="traceDialog" fullscreen>
      <el-tag>Ticks: {{ traceInfo.elapsedTime }}</el-tag>
      <br /><br />
      <el-table :data="traceInfo.list" border style="width: 100%">
        <el-table-column prop="createdAt" label="时间" width="190" />
        <el-table-column prop="title" label="标题" width="200" />
        <el-table-column prop="message" label="消息" :show-overflow-tooltip="true" />
        <el-table-column label="Mark" width="350">
          <template slot-scope="scope">
            <el-tag :style="`width: ${scope.row.mark || 0}%;padding:0px;`">{{
              scope.row.mark || 0
            }}</el-tag>
          </template>
        </el-table-column>
        <el-table-column label="操作" :width="60">
          <template slot-scope="scope">
            <el-button type="text" style="margin-right: 4px" @click="detail(scope.row)">
              详情
            </el-button>
          </template>
        </el-table-column>
      </el-table>
    </el-dialog>
  </div>
</template>

<script>
import parser from 'form-gen-parser'
import request from '@/utils/request'
import jsonView from 'vue-json-views'

export default {
  components: {
    parser,
    jsonView
  },
  data() {
    return {
      pageIndex: 1,
      pageSize: 20,
      totalCount: 0,
      tableData: [],
      formConfig: undefined,
      tableConfig: undefined,
      detailJson: undefined,
      detailDialog: false,
      traceDialog: false,
      traceInfo: {
        elapsedTime: 300,
        marks: {},
        list: []
      }
    }
  },
  watch: {},
  async mounted() {
    // 调整配置中的内容让其动态加载
    var { formConfig, tableConfig } = await request.get(
      '/api/SerilogElasticSearch/PageConfig'
    )

    var res = await request.get('/api/SerilogElasticSearch/SelectItems')
    Object.keys(res).forEach(x => {
      var value = res[x]
      if (!(value && value.length)) return
      var item = formConfig.fields.find(f => f.__vModel__ === x)
      console.log('查找配置的项设置, 下拉内容', item, value)
      if (!item) return
      item.__slot__ = {
        ...item.__slot__,
        options: value.map(m => {
          return {
            label: m,
            value: m
          }
        })
      }
    })

    this.tableConfig = tableConfig
    this.formConfig = formConfig
    this.$forceUpdate()

    this.$nextTick(() => {
      // parser 似乎不能设置这个title
      var formBtu = window.document.getElementsByClassName(
        'el-button el-button--primary el-button--large'
      )[0]
      formBtu.innerText = '查询'
    })

    await this.query()
  },
  methods: {
    async query(formData) {
      var res = await request.post('/api/SerilogElasticSearch/Filter', {
        ...formData,
        pageIndex: this.pageIndex,
        pageSize: this.pageSize
      })
      console.log(res)
      this.tableData = res.items
      this.totalCount = res.totalCount
    },
    async detail(row) {
      var res = await request.get('/api/SerilogElasticSearch?id=' + row.id)

      var jsonMessage
      try {
        jsonMessage = JSON.parse(res.message)
      } catch {}

      if (jsonMessage) res.message = jsonMessage
      this.detailJson = res
      this.detailDialog = true
    },
    async trace(row) {
      // 根据追踪值搜索
      var { items } = await request.post('/api/SerilogElasticSearch/Filter', {
        traceId: row.traceId,
        pageIndex: 1,
        pageSize: 9999
      })
      if (!items || !items.length) {
        this.$message.error('没有任何数据')
      }
      // 反转数组
      items = items.reverse()

      // 数组的第一位为开始时间
      var startAt = items[0].createdAtTicks
      var elapsedTime

      // 数组的最后一位减去开始时间得到总时间差
      this.traceInfo.elapsedTime = elapsedTime =
        items[items.length - 1].createdAtTicks - startAt

      if (elapsedTime > 0) {
        items.forEach(x => {
          x.mark = (((x.createdAtTicks - startAt) / elapsedTime) * 100).toFixed(0)
        })
      }

      this.traceInfo.list = items
      this.traceDialog = true
    }
  }
}
</script>
