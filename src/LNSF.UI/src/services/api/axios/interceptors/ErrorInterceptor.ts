import { toast } from "react-toastify";
import { AxiosError } from "axios"

interface iAppException {
  id: string;
  statusCode: number;
  message: string;
}

interface iDotNetException {
  type: string;
  title: string;
  status: number;
  errors: {
    [key: string]: string[];
  }
  traceId: string;
}

export const errorInterceptor = (error: AxiosError) => {
  if (error.response!.data instanceof Object && 'id' in error.response!.data && 'statusCode' in error.response!.data && 'message' in error.response!.data ) {
    const appException = error.response!.data as iAppException;
    toast.error(appException.message);
    return;
  }

  if (error.response!.data instanceof Object && 'type' in error.response!.data && 'title' in error.response!.data && 'status' in error.response!.data && 'errors' in error.response!.data && 'traceId' in error.response!.data) {
    const dotNetException = error.response!.data as iDotNetException;
    toast.error(dotNetException.title);
    return;
  }

  toast.error('Erro inesperado.');
  return;
}