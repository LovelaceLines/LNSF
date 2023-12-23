import { toast } from "react-toastify";
import { AxiosError } from "axios";
import { iAppException, iDotNetException } from "../../types";

// 401 unauthorized 403 forbidden 404 not found 500 internal server error

export const errorInterceptor = (error: AxiosError) => {
  // Verifica se o erro é do tipo iAppException
  if (error.response!.data instanceof Object && 'id' in error.response!.data && 'statusCode' in error.response!.data && 'message' in error.response!.data ) {
    const appException = error.response!.data as iAppException;
    toast.error(appException.message);
    return;
  }

  // TODO - Salvar Log de erro

  // Verifica se o erro é do tipo iDotNetException
  if (error.response!.data instanceof Object && 'type' in error.response!.data && 'title' in error.response!.data && 'status' in error.response!.data && 'errors' in error.response!.data && 'traceId' in error.response!.data) {
    const dotNetException = error.response!.data as iDotNetException;
    toast.error(dotNetException.title);
    return;
  }

  // TODO - Erro de Access Token Expirado

  toast.error('Erro inesperado.');
  return;
}