import {AxiosResponse} from "axios"

export const responseInterceptor = (response: AxiosResponse) => {
  console.log("ResponseInterceptor: ", response); 
  return response;  
}