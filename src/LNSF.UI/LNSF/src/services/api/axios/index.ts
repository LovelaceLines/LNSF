import axios from 'axios';
 import { errorInterceptor, responseInterceptor } from './interceptors';
import { Environment } from '../../../environment';


// const accessToken = localStorage.getItem('APP_ACCESS_TOKEN');

// if (!accessToken) {
//   throw new Error('Token de acesso não encontrado.');
// }

// const authorizationHeader = `Bearer ${accessToken}`;


export const Api = axios.create({
    baseURL: Environment.URL_BASE,
    timeout: 10000,
    headers: {
        'Content-Type': 'application/json',
    },
});

Api.interceptors.response.use(
    (response) => responseInterceptor(response),
    (error) => errorInterceptor(error),
);


  // headers: {
       
    //     // 'Access-Control-Allow-Origin': 'http://localhost:5173',
    //     // 'Access-Control-Allow-Headers': 'Authorization', 
    // // 'Access-Control-Allow-Methods': 'GET, POST, PUT, DELETE',
    //     //Authorization: authorizationHeader,
      
    //    // Authorization: `Bearer ${JSON.parse(localStorage.getItem('APP_ACCESS_TOKEN') || '')}`,
    // }