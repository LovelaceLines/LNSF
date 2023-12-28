
import {InternalAxiosRequestConfig} from "axios"

export const requestInterceptor = (config: InternalAxiosRequestConfig) => {
  const token = localStorage.getItem('@lnsf:accessToken');
  config.headers['Authorization'] = `Bearer ${token}`;
  
  return config;  
}