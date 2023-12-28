import axios from 'axios';
import { errorInterceptor, responseInterceptor } from './interceptors';
import { apiUrl } from '../../../environment/environment.temp';
import { config } from 'process';
import { requestInterceptor } from './interceptors/RequestInterceptor';

export const Api = axios.create({
  baseURL: apiUrl,
  timeout: 10000,
  headers: {
    'Content-Type': 'application/json',
    Authorization: `Bearer ${localStorage.getItem('@lnsf:accessToken')}`,
  },
});

Api.interceptors.response.use(
  (response) => responseInterceptor(response),
  (error) => errorInterceptor(error),
);

Api.interceptors.request.use(
 (config)  => requestInterceptor(config)
 // to do (error) = 
)


// headers: {

//     // 'Access-Control-Allow-Origin': 'http://localhost:5173',
//     // 'Access-Control-Allow-Headers': 'Authorization',
// // 'Access-Control-Allow-Methods': 'GET, POST, PUT, DELETE',
//     //Authorization: authorizationHeader,

//    // Authorization: `Bearer ${JSON.parse(localStorage.getItem('APP_ACCESS_TOKEN') || '')}`,
// }