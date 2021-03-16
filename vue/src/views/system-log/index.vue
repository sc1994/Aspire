<template>
  <div class="app-container">
    <parser v-if="formConf" :form-conf="formConf" @submit="query" />
  </div>
</template>

<script>
import parser from 'form-gen-parser'
import request from '@/utils/request'

export default {
  components: {
    parser
  },
  data() {
    return {
      formConf: {
        fields: [
          {
            __config__: {
              label: '',
              tag: 'el-date-picker',
              tagIcon: 'date-range',
              defaultValue: null,
              span: 9,
              showLabel: true,
              labelWidth: null,
              layout: 'colFormItem',
              regList: [],
              changeTag: true,
              document: 'https://element.eleme.cn/#/zh-CN/component/date-picker',
              formId: 101,
              renderKey: 1615800770197
            },
            style: {
              width: '100%'
            },
            type: 'datetimerange',
            'range-separator': '至',
            'start-placeholder': '开始日期',
            'end-placeholder': '结束日期',
            disabled: false,
            clearable: true,
            format: 'yyyy-MM-dd HH:mm:ss',
            'value-format': 'yyyy-MM-dd HH:mm:ss',
            readonly: false,
            __vModel__: 'createdAtRange'
          },
          {
            __config__: {
              label: '',
              showLabel: true,
              labelWidth: null,
              tag: 'el-select',
              tagIcon: 'select',
              layout: 'colFormItem',
              span: 6,
              regList: [],
              changeTag: true,
              document: 'https://element.eleme.cn/#/zh-CN/component/select',
              formId: 104,
              renderKey: 1615802005945
            },
            __slot__: {
              options: []
            },
            placeholder: '请求方法',
            style: {
              width: '100%'
            },
            clearable: true,
            disabled: false,
            filterable: false,
            multiple: false,
            __vModel__: 'apiMethod'
          },
          {
            __config__: {
              label: '',
              url: '/api/SerilogElasticSearch/SelectItems',
              method: 'get',
              dataKey: 'list',
              showLabel: true,
              labelWidth: null,
              tag: 'el-select',
              tagIcon: 'select',
              layout: 'colFormItem',
              defaultValue: '',
              dataType: 'dynamic',
              span: 9,
              regList: [],
              changeTag: true,
              document: 'https://element.eleme.cn/#/zh-CN/component/select',
              formId: 102,
              renderKey: 1615800839631
            },
            __slot__: {
              options: []
            },
            placeholder: '请求路由',
            style: {
              width: '100%'
            },
            clearable: true,
            disabled: false,
            filterable: false,
            multiple: false,
            __vModel__: 'apiRouter'
          },
          {
            __config__: {
              label: '',
              showLabel: true,
              labelWidth: null,
              tag: 'el-select',
              tagIcon: 'select',
              layout: 'colFormItem',
              span: 6,
              regList: [],
              changeTag: true,
              document: 'https://element.eleme.cn/#/zh-CN/component/select',
              formId: 111,
              renderKey: 1615802230148
            },
            __slot__: {
              options: [
                {
                  label: 'Information',
                  value: 0
                },
                {
                  label: 'Warning',
                  value: 1
                },
                {
                  label: 'Error',
                  value: 2
                }
              ]
            },
            placeholder: '日志等级',
            style: {
              width: '100%'
            },
            clearable: true,
            disabled: false,
            filterable: false,
            multiple: false,
            __vModel__: 'level'
          },
          {
            __config__: {
              label: '',
              showLabel: true,
              labelWidth: null,
              tag: 'el-select',
              tagIcon: 'select',
              layout: 'colFormItem',
              span: 6,
              regList: [],
              changeTag: true,
              document: 'https://element.eleme.cn/#/zh-CN/component/select',
              formId: 103,
              renderKey: 1615801984165
            },
            __slot__: {
              options: []
            },
            placeholder: '标题',
            style: {
              width: '100%'
            },
            clearable: true,
            disabled: false,
            filterable: false,
            multiple: false,
            __vModel__: 'title'
          },
          {
            __config__: {
              label: '',
              labelWidth: null,
              showLabel: true,
              changeTag: true,
              tag: 'el-input',
              tagIcon: 'input',
              layout: 'colFormItem',
              span: 6,
              document: 'https://element.eleme.cn/#/zh-CN/component/input',
              regList: [],
              formId: 110,
              renderKey: 1615802152405
            },
            __slot__: {
              prepend: '',
              append: ''
            },
            placeholder: '客户端地址',
            style: {
              width: '100%'
            },
            clearable: true,
            'prefix-icon': '',
            'suffix-icon': '',
            maxlength: null,
            'show-word-limit': false,
            readonly: false,
            disabled: false,
            __vModel__: 'clientAddress'
          },
          {
            __config__: {
              label: '',
              showLabel: true,
              labelWidth: null,
              tag: 'el-select',
              tagIcon: 'select',
              layout: 'colFormItem',
              span: 6,
              regList: [],
              changeTag: true,
              document: 'https://element.eleme.cn/#/zh-CN/component/select',
              formId: 108,
              renderKey: 1615802133983
            },
            __slot__: {
              options: []
            },
            placeholder: '服务端地址',
            style: {
              width: '100%'
            },
            clearable: true,
            disabled: false,
            filterable: false,
            multiple: false,
            __vModel__: 'serverAddress'
          },
          {
            __config__: {
              label: '',
              labelWidth: null,
              showLabel: true,
              changeTag: true,
              tag: 'el-input',
              tagIcon: 'input',
              layout: 'colFormItem',
              span: 6,
              document: 'https://element.eleme.cn/#/zh-CN/component/input',
              regList: [],
              formId: 105,
              renderKey: 1615802099428
            },
            __slot__: {
              prepend: '',
              append: ''
            },
            placeholder: '过滤1',
            style: {
              width: '100%'
            },
            clearable: true,
            'prefix-icon': '',
            'suffix-icon': '',
            maxlength: null,
            'show-word-limit': false,
            readonly: false,
            disabled: false,
            __vModel__: 'filter1'
          },
          {
            __config__: {
              label: '',
              labelWidth: null,
              showLabel: true,
              changeTag: true,
              tag: 'el-input',
              tagIcon: 'input',
              layout: 'colFormItem',
              span: 6,
              document: 'https://element.eleme.cn/#/zh-CN/component/input',
              regList: [],
              formId: 106,
              renderKey: 1615802103063
            },
            __slot__: {
              prepend: '',
              append: ''
            },
            placeholder: '过滤2',
            style: {
              width: '100%'
            },
            clearable: true,
            'prefix-icon': '',
            'suffix-icon': '',
            maxlength: null,
            'show-word-limit': false,
            readonly: false,
            disabled: false,
            __vModel__: 'filter2'
          }
        ],
        formRef: 'elForm',
        formModel: 'formData',
        size: 'medium',
        labelPosition: 'right',
        labelWidth: 0,
        formRules: 'rules',
        gutter: 5,
        disabled: false,
        span: 9,
        formBtns: true
      }
    }
  },
  async mounted() {
    // TODO parser 好像有BUG, 不能设置这个title
    window.document.getElementsByClassName(
      'el-button el-button--primary el-button--large'
    )[0].innerText = '查询'

    var res = await request.get('/api/SerilogElasticSearch/SelectItems')
    Object.keys(res).forEach(x => {
      var value = res[x]
      if (!(value && value.length)) return

      var item = this.formConf.fields.find(f => f.__vModel__ === x)
      console.log('查找配置的项设置, 下拉内容', item)
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
  },
  methods: {
    async query(formData) {
      var res = await request.post('/api/SerilogElasticSearch/Filter', formData)
      console.log(res)
    }
  }
}
</script>
