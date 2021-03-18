import axios from 'axios'
import { MessageBox, Notification } from 'element-ui'
import store from '@/store'
import { getToken } from '@/utils/auth'

// create an axios instance
const service = axios.create({
  baseURL: process.env.VUE_APP_BASE_API, // url = base url + request url
  timeout: 30000 // request timeout
})

// request interceptor
service.interceptors.request.use(
  config => {
    // do something before request is sent

    if (store.getters.token) {
      // let each request carry token
      // ['X-Token'] is a custom headers key
      // please modify it according to the actual situation
      config.headers['Authorization'] = getToken()
    }
    return config
  },
  error => {
    // do something with request error
    console.log(error) // for debug
    return Promise.reject(error)
  }
)

// response interceptor
service.interceptors.response.use(
  /**
   * If you want to get http information such as headers or status
   * Please return  response => response
  */

  /**
   * Determine the request status by custom code
   * Here is just an example
   * You can also judge the status by HTTP Status Code
   */
  response => {
    const res = response.data
    // if the custom code is not 20000, it is judged as an error.
    if (res.code !== 20000) {
      var msg
      if (res.code === 40101) {
        msg = '当前用户未被授权访问本数据, 是否跳转登录?'
      }
      else if (res.code === 40103 || res.code === 40104) {
        msg = '授权无效或者授权过期, 是否重新登入?'
      } else {
        if (!res.messages) {
          res.messages = [];
        }
        Notification({
          title: res.title,
          dangerouslyUseHTMLString: true,
          message: res.messages || res.messages.join('<br/>'),
          type: 'error',
          duration: 8 * 1000
        })
      }
      if (msg) {
        // to re-login
        MessageBox.confirm(msg, {
          confirmButtonText: 'Re-Login',
          cancelButtonText: 'Cancel',
          type: 'warning'
        }).then(() => {
          store.dispatch('user/resetToken').then(() => {
            location.reload()
          })
        })
      }
      return Promise.reject(new Error(res.message || 'Error'))
    } else {
      return res.result
    }
  },
  error => {
    console.log('err' + error) // for debug
    Notification({
      title: error.message,
      type: 'error',
      duration: 5 * 1000
    })
    return Promise.reject(error)
  }
)

export default service
