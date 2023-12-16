import {AxiosResponse} from "axios"

export const responseInterceptor = (response: AxiosResponse) => {
  const token = localStorage.getItem('@lnsf:accessToken');
  response.headers['Authorization'] = `Bearer ${token}`;
  
  return response;  
}