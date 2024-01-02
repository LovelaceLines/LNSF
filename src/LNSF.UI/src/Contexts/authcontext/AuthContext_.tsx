import { createContext, useCallback, useState } from 'react';
import { iAuthTypes, iDataLogin, iToken, iUser } from './type';
import { Api } from '../../services/api/axios';
import { LocalStorage } from '../../Global';

export const AuthContext = createContext<iAuthTypes>({} as iAuthTypes);

export const AuthProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  // TODO: Implementar outra forma de verificar se o usuário está autenticado no front-end
  const [isAuthenticated, setIsAuthenticated] = useState(
    !!LocalStorage.getAccessToken() && 
    !!LocalStorage.getRefreshToken() && 
    !!LocalStorage.getUser()
  );

  const login = useCallback(async (data: iDataLogin) => {
    const res = await Api.post<iToken>('/Auth/login', data);

    LocalStorage.setAccessToken(res.data.accessToken);
    LocalStorage.setRefreshToken(res.data.refreshToken);
    LocalStorage.setUser(await getUser());
    setIsAuthenticated(true);
  }, []);

  const refreshToken = useCallback(async () => {
    LocalStorage.setAccessToken(LocalStorage.getRefreshToken() || '');
    const res = await Api.post<iToken>('/Auth/refresh-token');
   
    LocalStorage.setAccessToken(res.data.accessToken);
    LocalStorage.setRefreshToken(res.data.refreshToken);
    LocalStorage.setUser(await getUser());
    setIsAuthenticated(true);
  }, []);

  const logout = () => {
    LocalStorage.clearTokens();
    LocalStorage.clearUser();
    setIsAuthenticated(false);
  };

  const getUser = useCallback(async (): Promise<iUser> => {
    console.log("AuthContext getUser");

    let user: iUser | null = LocalStorage.getUser();

    if (!!user) return user;

    const res = await Api.get<iUser>('/Auth/user');
    user = res.data;

    console.log("AuthContext getUser: ", user);

    LocalStorage.setUser(user);

    return user;
  }, []);

  return (
    <AuthContext.Provider value={{ isAuthenticated, login, refreshToken, logout, getUser }}>
      {children}
    </AuthContext.Provider>
  )
}