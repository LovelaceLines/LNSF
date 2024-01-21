import {AxiosResponse} from "axios"

export const responseInterceptor = (response: AxiosResponse) => {
  console.debug("ResponseInterceptor: ", response); 
  return response;  
}