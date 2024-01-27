import { toast } from "react-toastify";
import { AxiosError, AxiosRequestConfig } from "axios";
import { iAppException, iDotNetException } from "../../types";
import { Api } from "../index";
import { LocalStorage } from "../../../../Global";
import { iToken } from "../../../../Contexts";

export const responseErrorInterceptor = async (error: AxiosError) => { 
  console.debug("ResponseErrorInterceptor: ", error);

  if (!!error.response && error.response.status == 401) {
    LocalStorage.setAccessToken(LocalStorage.getRefreshToken() || '');
    LocalStorage.setTryToRefreshToken();

    Api.get<iToken>('/Auth/refresh-token')
    .then(res => {
      const token = res.data;
      LocalStorage.setAccessToken(token.accessToken);
      LocalStorage.setRefreshToken(token.refreshToken);
    })
    .then(() => {
      const originalRequestConfig = error.config;
      originalRequestConfig!.headers['Authorization'] = `Bearer ${LocalStorage.getAccessToken()}`;
      Api(originalRequestConfig as AxiosRequestConfig);
      LocalStorage.clearTryToRefreshToken();
    })
    .catch(() => {
      LocalStorage.setTryToRefreshToken();
      LocalStorage.clearUser();
      LocalStorage.clearTokens();
      toast.error('Acesso não autorizado.');
    })
    .finally(() => { return });
  } else if (!!error.response && error.response.status == 403) {
    toast.error('Acesso negado.');
    return;
  } else if (!!error.response && error.response.status == 404) {
    toast.error('Recurso não encontrado.');
    return;
  } else if (!!error.response && error.response.status == 405) {
    toast.error('Método não permitido.');
    return;
  } else if (error.response!.data instanceof Object && 'id' in error.response!.data && 'statusCode' in error.response!.data && 'message' in error.response!.data ) {
    const appException = error.response!.data as iAppException;
    toast.error(appException.message);
    return;
  } else if (error.response!.data instanceof Object && 'type' in error.response!.data && 'title' in error.response!.data && 'status' in error.response!.data && 'errors' in error.response!.data && 'traceId' in error.response!.data ) {
    const dotNetException = error.response!.data as iDotNetException;
    toast.error(dotNetException.title);
    return;
  } else {
    toast.error('Erro inesperado ao receber resposta.');
    return;
  }
}