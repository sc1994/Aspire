import request from '@/utils/request'

export function login(data) {
  return request({
    url: '/api/Authorization/Login',
    method: 'post',
    data
  })
}

export function getInfo() {
  return request({
    url: '/api/Authorization/CurrentUser',
    method: 'get'
  })
}

export function logout() {
  return request({
    url: '/api/Authorization/Logout',
    method: 'post'
  })
}
