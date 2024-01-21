import { toast } from "react-toastify";
import { iDotNetException } from "../../types";
import { AxiosError } from "axios";

export const requestErrorInterceptor = (error: AxiosError) => {
  console.debug("RequestErrorInterceptor: ", error);
  
  // Verifica se o erro é do tipo iDotNetException
  if (error.response!.data instanceof Object && 'type' in error.response!.data && 'title' in error.response!.data && 'status' in error.response!.data && 'errors' in error.response!.data && 'traceId' in error.response!.data ) {
    const dotNetException = error.response!.data as iDotNetException;
    toast.error(dotNetException.title);
    return;
  }
  
  toast.error('Erro inesperado ao realizar requisição.');
  return;
}