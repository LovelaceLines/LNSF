import { createContext, useCallback, useState } from "react";
import { iAccountProvider, iAccountTypes, iaccount, iattAccount, idelAccount, iregisterAccount } from "./type";
import { toast } from "react-toastify";
import { Api } from "../../services/api/axios";

export const AccountContext = createContext({} as iAccountTypes);

export const AccountProvider = ({ children }: iAccountProvider) => {
    const [account, setAccount] = useState<iaccount>({} as iaccount)

    const viewAccount = async (userName = '') => {
        try {

            const urlRelativa = `/Account?userName=${userName}`;
            const response = await Api.get(urlRelativa);

            if (response.status === 200) {
                return response.data as iaccount[];
            }
        } catch (error: any) {
            if (error.response) {
                toast.error(error.response.data.message);
            }else {
                toast.error('Ocorreu um erro ao processar a requisição.');
                console.error('Error Message:', error.message);
            }
            return new Error((error as { message: string }).message || 'Ops, não foi possível acessar o banco.')
        }
        return [];
    }

    const registerAccount = useCallback(async (data: iregisterAccount) => {
        try {

            const objetoJSON = JSON.stringify(data);
           
            const response = await Api.post('/Account', objetoJSON)

            if (response.status === 200) {
                toast.success('Usuário cadastrado!');
                return response.data as iaccount;
            }
        }catch (error: any) {
            if (error.response) {
                toast.error(error.response.data.message);
            }else {
                toast.error('Ocorreu um erro ao processar a requisição.');
                console.error('Error Message:', error.message);
            }
            return new Error((error as { message: string }).message || 'Ops, não foi possível acessar o banco.')
        }
        return {} as iaccount;
    }, []);

    const updateAccount = useCallback(async (data: iattAccount, verificador = false) => {
        try {
            const objetoJSON = JSON.stringify(data);
           
            const url = verificador ? '/Account/password' : '/Account';
            const response = await Api.put(url, objetoJSON);

            if (response.status === 200) {
                toast.success('Usuário atualizado!');
                return response.data as iaccount;
            }
        } catch (error: any) {
            if (error.response) {
                toast.error(error.response.data.message);
            }else {
                toast.error('Ocorreu um erro ao processar a requisição.');
                console.error('Error Message:', error.message);
            }
            return new Error((error as { message: string }).message || 'Ops, não foi possível acessar o banco.')
        }
        return {} as iaccount;

    }, []);


    const deleteAccount = useCallback(async (data: idelAccount) => {
        try {
            
            const response = await Api.delete(`/Account/${data.id}`);

            if (response.status === 200) {
                toast.success('Usuário deletado!');
                return response.data as iaccount;
            }
        } catch (error: any) {
            if (error.response) {
                toast.error(error.response.data.message);
            }else {
                toast.error('Ocorreu um erro ao processar a requisição.');
                console.error('Error Message:', error.message);
            }
            return new Error((error as { message: string }).message || 'Ops, não foi possível acessar o banco.')
        }
        return {} as iaccount;

    }, []);

    return (
        <AccountContext.Provider value={{ account, setAccount, viewAccount, registerAccount, updateAccount, deleteAccount }}>
            {children}
        </AccountContext.Provider>
    )
}