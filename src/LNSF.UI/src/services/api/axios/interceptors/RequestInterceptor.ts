import {InternalAxiosRequestConfig} from "axios"
import { LocalStorage } from "../../../../Global";

export const requestInterceptor = (config: InternalAxiosRequestConfig) => {
  console.log("RequestInterceptor: ", config);
  config.headers['Authorization'] = `Bearer ${LocalStorage.getAccessToken()}`;
  return config;  
}