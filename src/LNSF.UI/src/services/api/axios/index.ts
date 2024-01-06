import axios from 'axios';
import { requestErrorInterceptor, requestInterceptor, responseErrorInterceptor, responseInterceptor } from './interceptors';
import { apiUrl } from '../../../environment/environment.temp';

export const Api = axios.create({
  baseURL: apiUrl,
  timeout: 10000,
  headers: {
    'Content-Type': 'application/json',
  },
});

Api.interceptors.response.use(
  response => responseInterceptor(response),
  async error => await responseErrorInterceptor(error),
);

Api.interceptors.request.use(
  config => requestInterceptor(config),
  error => requestErrorInterceptor(error),
)


// headers: {

//     // 'Access-Control-Allow-Origin': 'http://localhost:5173',
//     // 'Access-Control-Allow-Headers': 'Authorization',
// // 'Access-Control-Allow-Methods': 'GET, POST, PUT, DELETE',
//     //Authorization: authorizationHeader,

//    // Authorization: `Bearer ${JSON.parse(localStorage.getItem('APP_ACCESS_TOKEN') || '')}`,
// }