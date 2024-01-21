import { toast } from "react-toastify";
import { AxiosError, AxiosRequestConfig } from "axios";
import { iAppException, iDotNetException } from "../../types";
import { Api } from "../index";
import { LocalStorage } from "../../../../Global";
import { refreshToken } from "../../../AuthService";

// 401 unauthorized 403 forbidden 404 not found 500 internal server error

export const responseErrorInterceptor = async (error: AxiosError) => { 
  console.debug("ResponseErrorInterceptor: ", error);

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
    if (LocalStorage.getTryToRefreshToken()) {
      LocalStorage.clearAll();
      toast.error('Acesso não autorizado.');
      return;
    }

    return refreshToken()
    .then(() => {
      const originalRequestConfig = error.config;
      Api(originalRequestConfig as AxiosRequestConfig);
      LocalStorage.clearTryToRefreshToken();
    });
  }

  if (!!error.response && error.response.status == 403) {
    toast.error('Acesso negado.');
    return;
  }

  if (!!error.response && error.response.status == 404) {
    toast.error('Recurso não encontrado.');
    return;
  }

  if (!!error.response && error.response.status == 405) {
    // TODO - Exibir apenas em ambiente de desenvolvimento
    toast.error('Método não permitido.');
    return;
  }

  // TODO - Salvar Log de erro

  // TODO - Erro de Access Token Expirado

  toast.error('Erro inesperado ao receber resposta.');
  return;
}