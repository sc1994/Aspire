<template>
  <div class="app-container">
    <el-form :inline="true" :model="queryForm">
      <el-form-item label="Method">
        <el-select v-model="queryForm.apiMethod">
          <el-option
            v-for="item in selectItems.apiMethods"
            :key="item"
            :label="item"
            :value="item"
          />
        </el-select>
      </el-form-item>
      <el-form-item label="Router">
        <el-cascader
          v-model="queryForm.apiRouter"
          :options="selectItems.apiRouters"
          :props="{ checkStrictly: true }"
          clearable
        />
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
        apiRouter: '',
        title: '',
        traceId: '',
        filter1: '',
        filter2: '',
        clientAddress: '',
        serverAddress: '',
        level: 0,
        createdAtRange: [],
        pageIndex: 1,
        pageSize: 10
      },
      totalCount: 0,
      selectItems: {}
    }
  },
  async mounted() {
    this.selectItems = await request.get('/api/SerilogElasticSearch/SelectItems')
  },
  methods: {
    async query() {
      var res = await request.post('/api/SerilogElasticSearch/Filter', this.queryForm)
      console.log(res)
    }
  }
}
</script>
