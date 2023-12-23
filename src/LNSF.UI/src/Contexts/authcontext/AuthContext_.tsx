import { createContext, useCallback, useState } from 'react';
import { iAuthTypes, iDataLogin, iToken, iUser } from './type';
import { Api } from '../../services/api/axios';

// TODO - usar outra abordagem
export const AuthContext = createContext<iAuthTypes>({} as iAuthTypes);

export const AuthProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [token, setToken] = useState<iToken>({} as iToken);

  const saveToken = useCallback(() => {
    setIsAuthenticated(true);

    localStorage.setItem('@lnsf:accessToken', token.accessToken);
    localStorage.setItem('@lnsf:refreshToken', token.refreshToken);
  }, [token]);

  const login = useCallback(async (data: iDataLogin) => {
    const res = await Api.post<iToken>('/Auth/login', data);
    setToken(res.data);
    saveToken();
  }, [saveToken]);
  
  const refreshToken = useCallback(async () => {
    localStorage.setItem('@lnsf:accessToken', token.refreshToken);
    const res = await Api.post<iToken>('/Auth/refresh-token');
    setToken(res.data);
    saveToken();
  }, [saveToken, token]);

  const logout = useCallback(() => {
    setIsAuthenticated(false);

    localStorage.removeItem('@lnsf:accessToken');
    localStorage.removeItem('@lnsf:refreshToken');
  }, []);

  const getUser = useCallback(async (): Promise<iUser> => {
    const res = await Api.get<iUser>('/Auth/user');
    const user: iUser = res.data;
    return user;
  }, []);

  return (
    <AuthContext.Provider value={{ isAuthenticated, login, refreshToken, logout, getUser }}>
      {children}
    </AuthContext.Provider>
  )
}