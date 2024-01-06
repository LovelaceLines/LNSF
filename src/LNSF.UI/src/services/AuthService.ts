import { iToken } from "../Contexts";
import { LocalStorage } from "../Global";
import { Api } from "./api/axios";

export const refreshToken = async () => {
  LocalStorage.setAccessToken(LocalStorage.getRefreshToken() || '');
  LocalStorage.setTryToRefreshToken();

  return Api.get<iToken>('/Auth/refresh-token')
  .then(res => {
    const token = res.data;
    LocalStorage.setAccessToken(token.accessToken);
    LocalStorage.setRefreshToken(token.refreshToken);
  });
};