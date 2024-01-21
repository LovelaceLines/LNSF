import {InternalAxiosRequestConfig} from "axios"
import { LocalStorage } from "../../../../Global";

export const requestInterceptor = (config: InternalAxiosRequestConfig) => {
  let paramsString = '';
  const params = config.params;
  if (params && params.page)
    paramsString += `page.page=${params.page.page}&page.pageSize=${params.page.pageSize}`;

  if (paramsString.length > 0)
    config.url += `?${paramsString}`;

  console.debug("RequestInterceptor: ", config);
  config.headers['Authorization'] = `Bearer ${LocalStorage.getAccessToken()}`;
  return config;  
}