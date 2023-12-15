import { createContext, useCallback, useState } from 'react';
import { iAuthProvider, iAuthTypes, iDataLogin, iObjectUser, iToken } from './type';
import { Api } from '../../services/api/axios';
import { toast } from 'react-toastify';
import { useNavigate } from 'react-router-dom';

export const AuthContext = createContext({} as iAuthTypes);

export const AuthProvider = ({ children }: iAuthProvider) => {
  const [user, setUser] = useState<iObjectUser>({} as iObjectUser);
  const [tokens, setTokens] = useState<iToken>({} as iToken);

  const navigate = useNavigate();

  const loginUser = useCallback(async (data: iDataLogin) => {
    const res = await Api.post<iToken>('/Auth/login', data);
    const token: iToken = res.data;

    setTokens(token);

    localStorage.setItem('@lnsf:accessToken', token.accessToken);
    localStorage.setItem('@lnsf:expires', token.expires);

    toast.success(`Seja bem vindo :)`);

    navigate("/inicio");
  }, [navigate]);

  const getUsers = useCallback(async (useName: string) => {
      try {

          const response = await Api.get(`/Account/exist/${useName}`);
          if (response.status === 200) {
              return response.data as iObjectUser;
          }

      } catch (error: any) {
          if (error.response) {
              toast.error(error.response.data.message);
          } else {
              toast.error('Ocorreu um erro ao processar a requisição.');
              console.error('Error Message:', error.message);
          }
          return new Error((error as { message: string }).message || 'Ops, não foi possível acessar o banco.')
      }
      return {} as iObjectUser
  }, []);

  const logoutUser = useCallback(() => {
      navigate("/")
      toast.success("Até logo! :)")
      localStorage.clear();
  }, []);

  return (
      <AuthContext.Provider value={{ user, loginUser, getUsers, logoutUser, tokens, setTokens, setUser }}>
          {children}
      </AuthContext.Provider>
  )
}