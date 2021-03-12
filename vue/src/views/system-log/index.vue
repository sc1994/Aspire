<template>
  <div class="app-container">
    <el-form :inline="true" :model="queryForm">
      <el-form-item label="Time Scope">
        <el-date-picker
          v-model="queryForm.createdAtRange"
          type="datetimerange"
          :picker-options="pickerOptions"
          range-separator="至"
          start-placeholder="开始日期"
          end-placeholder="结束日期"
          align="right"
          format="yyyy-MM-dd HH:mm"
          value-format="yyyy-MM-dd HH:mm"
          clearable
        />
      </el-form-item>
      <el-form-item label="Router">
        <el-cascader
          v-model="queryForm.apiRouters"
          :options="selectItems.apiRouters"
          :props="{ checkStrictly: true }"
          clearable
        />
      </el-form-item>
      <el-form-item label="Title">
        <el-select v-model="queryForm.title" clearable>
          <el-option
            v-for="item in selectItems.titles"
            :key="item"
            :label="item"
            :value="item"
          />
        </el-select>
      </el-form-item>
      <el-form-item label="Server Address">
        <el-select v-model="queryForm.serverAddress" clearable>
          <el-option
            v-for="item in selectItems.serverAddress"
            :key="item"
            :label="item"
            :value="item"
          />
        </el-select>
      </el-form-item>
      <el-form-item label="Level">
        <el-select v-model="queryForm.level" clearable>
          <el-option label="Information" value="0" />
          <el-option label="Warning" value="1" />
          <el-option label="Error" value="2" />
        </el-select>
      </el-form-item>
      <el-form-item label="Trace">
        <el-input v-model="queryForm.traceId" clearable />
      </el-form-item>
      <el-form-item label="Filter1">
        <el-input v-model="queryForm.filter1" clearable />
      </el-form-item>
      <el-form-item label="Filter2">
        <el-input v-model="queryForm.filter2" clearable />
      </el-form-item>
      <el-form-item label="Trace">
        <el-input v-model="queryForm.traceId" clearable />
      </el-form-item>

      <el-form-item>
        <el-button type="primary" @click="query()">查询</el-button>
      </el-form-item>
    </el-form>
  </div>
</template>

<script>
import request from '@/utils/request'

export default {
  data() {
    return {
      queryForm: {
        apiMethod: '',
        apiRouters: [],
        title: '',
        traceId: '',
        filter1: '',
        filter2: '',
        clientAddress: '',
        serverAddress: '',
        level: '',
        createdAtRange: [],
        pageIndex: 1,
        pageSize: 10
      },
      totalCount: 0,
      selectItems: {},
      pickerOptions: {
        shortcuts: [
          {
            text: '最近30分钟',
            onClick(picker) {
              const end = new Date()
              const start = new Date()
              start.setTime(start.getTime() - 3600 * 1000 * 0.5)
              picker.$emit('pick', [start, end])
            }
          },
          {
            text: '最近一小时',
            onClick(picker) {
              const end = new Date()
              const start = new Date()
              start.setTime(start.getTime() - 3600 * 1000)
              picker.$emit('pick', [start, end])
            }
          },
          {
            text: '最近6小时',
            onClick(picker) {
              const end = new Date()
              const start = new Date()
              start.setTime(start.getTime() - 3600 * 1000 * 6)
              picker.$emit('pick', [start, end])
            }
          },
          {
            text: '最近1天',
            onClick(picker) {
              const end = new Date()
              const start = new Date()
              start.setTime(start.getTime() - 3600 * 1000 * 24)
              picker.$emit('pick', [start, end])
            }
          }
        ]
      }
    }
  },
  async mounted() {
    this.selectItems = await request.get('/api/SerilogElasticSearch/SelectItems')
  },
  methods: {
    async query() {
      if (this.queryForm.apiRouters) {
        this.queryForm.apiRouter = this.queryForm.apiRouters.join('/')
      }
      var res = await request.post('/api/SerilogElasticSearch/Filter', this.queryForm)
      console.log(res)
    }
  }
}
</script>
