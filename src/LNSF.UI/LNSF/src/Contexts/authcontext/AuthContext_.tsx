import { createContext, useCallback, useEffect, useState } from 'react';
import { iAuthProvider, iAuthTypes, iDataLogin, iObjectUser, iTokens } from './type';
import { Api } from '../../services/api/axios';
import { toast } from 'react-toastify';
import { useNavigate } from 'react-router-dom';

export const AuthContext = createContext({} as iAuthTypes);

export const AuthProvider = ({ children }: iAuthProvider) => {
    const [user, setUser] = useState<iObjectUser>({} as iObjectUser);
    const [tokens, setTokens] = useState<iTokens>({} as iTokens);
   // const accessToken = localStorage.getItem("@lnsf:accessToken") || ''

    const navigate = useNavigate();

    //const userName = localStorage.getItem('@lnsf:userName') || ''

    // useEffect(() => {
    //     if (accessToken) {
    //         getUsers(userName)
    //             .then((response) => {
    //                 if (response instanceof Error) {
    //                     localStorage.clear();
    //                     toast.error(response.message);
    //                 } else {
    //                     setUser(response);
    //                 }
    //             })
    //             .catch((error) => {
    //                 console.error('Detalhes do erro:', error);
    //             });
    //     }
    // }, []);


    const loginUser = useCallback(async (data: iDataLogin) => {

        try {
            const response = await Api.post('/Auth/login', data);

            if (response.status === 200) {
                setTokens(response.data);
                localStorage.setItem('@lnsf:userName', data.userName);
                localStorage.setItem('@lnsf:accessToken', response.data.accessToken);// verificar resposta da api
                localStorage.setItem('@lnsf:refreshToken', response.data.refreshToken);// verificar resposta da api
                localStorage.setItem('@lnsf:expires', response.data.expires);// verificar resposta da api
                toast.success(`Seja bem vindo :)`)
                navigate("/inicio")
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
    }, []);

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