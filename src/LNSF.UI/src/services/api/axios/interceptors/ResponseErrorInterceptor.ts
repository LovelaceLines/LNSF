import { toast } from "react-toastify";
import { AxiosError } from "axios";
import { iAppException, iDotNetException } from "../../types";
import { LocalStorage } from "../../../../Global";

// 401 unauthorized 403 forbidden 404 not found 500 internal server error

export const responseErrorInterceptor = async (error: AxiosError) => { 
  console.log("ResponseErrorInterceptor: ", error);

  // Verifica se o erro é do tipo iAppException
  if (error.response!.data instanceof Object && 'id' in error.response!.data && 'statusCode' in error.response!.data && 'message' in error.response!.data ) {
    const appException = error.response!.data as iAppException;
    toast.error(appException.message);
    return;
  }

  // Verifica se o erro é do tipo iDotNetException
  if (error.response!.data instanceof Object && 'type' in error.response!.data && 'title' in error.response!.data && 'status' in error.response!.data && 'errors' in error.response!.data && 'traceId' in error.response!.data ) {
    const dotNetException = error.response!.data as iDotNetException;
    toast.error(dotNetException.title);
    return;
  }

  if (!!error.response && error.response.status == 401) {
    LocalStorage.clearTokens();
    toast.error('Acesso não autorizado.');
    return;
  }

  if (!!error.response && error.response.status == 403) {
    toast.error('Acesso negado.');
    return;
  }

  if (!!error.response && error.response.status == 404) {
    toast.error('Recurso não encontrado.');
    return;
  }

  // TODO - Salvar Log de erro

  // TODO - Erro de Access Token Expirado

  toast.error('Erro inesperado ao receber resposta.');
  return;
}