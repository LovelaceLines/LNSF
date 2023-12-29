import { toast } from "react-toastify";
import { AxiosError } from "axios";
import { iAppException } from "../../types";

// 401 unauthorized 403 forbidden 404 not found 500 internal server error

export const responseErrorInterceptor = (error: AxiosError) => {
  // Verifica se o erro é do tipo iAppException
  if (error.response!.data instanceof Object && 'id' in error.response!.data && 'statusCode' in error.response!.data && 'message' in error.response!.data ) {
    const appException = error.response!.data as iAppException;
    toast.error(appException.message);
    return;
  }

  // TODO - Salvar Log de erro

  // TODO - Erro de Access Token Expirado

  toast.error('Erro inesperado ao receber resposta.');
  return;
}