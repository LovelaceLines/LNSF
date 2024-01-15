import { createContext, useCallback, useState } from "react";
import { iEscortObject, iEscortProvider, iEscortTypes } from "./type";
import { Api } from "../../services/api/axios";
import { toast } from "react-toastify";
import { Environment } from "../../environment";
import { number } from "yup";

export const EscortContext = createContext({} as iEscortTypes);

export const EscortProvider = ({ children }: iEscortProvider) => {
    
    const [Escort, setEscort] = useState<iEscortObject>({} as iEscortObject)
    
    const viewEscort= async (page = 1, filter = '', textFilter = '') => {
        try {
            const urlRelativa = `/Escort?Page.Page=${page}&Page.PageSize=${Environment.LIMITE_DE_LINHA}&${textFilter}=${filter}`;
            const response = await Api.get(urlRelativa);

            if (response.status === 200) {
                setEscort(response.data)
                return response.data as iEscortObject[];
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
    
    const registerEscort = useCallback(async (data: iEscortObject) => {
        try {

            const objetoJSON = JSON.stringify(data);
           
            const response = await Api.post('/Escort', objetoJSON)

            if (response.status === 200) {
                toast.success('Acompanhante cadastrado!');
                return response.data as iEscortObject;
            }
        } catch (error: any) {
            if (error.response) {
                toast.error(error.response.data.message);
            }else {
                toast.error('Ocorreu um erro ao processar a requisição.');
                console.error('Error Message:', error.message);
            }
            return new Error((error as { message: string }).message || 'Erro ao cadastrar.')
        }
        return {} as iEscortObject;
    }, []);



    const updateEscort= useCallback(async (data: iEscortObject) => {
        try {
            const objetoJSON = JSON.stringify(data);
           
            const response = await Api.put('/Escort', objetoJSON);

            if (response.status === 200) {
                toast.success('Acompanhante atualizado!');
                return response.data as iEscortObject;
            } 
        } catch (error: any) {
            if (error.response) {
                toast.error(error.response.data.message);
            }else {
                toast.error('Ocorreu um erro ao processar a requisição.');
                console.error('Error Message:', error.message);
            }
            return new Error((error as { message: string }).message || 'Erro ao atualizar os registros de quarto.')
        }
        return {} as iEscortObject;

    }, []);

    const countEscort = useCallback(async () => {
        try {
            const response = await Api.get('/Escort/Count');

            if (response.status === 200) {
                return response.data as number;
            }
        } catch (error: any) {
            if (error.response) {
                toast.error(error.response.data.message);
            }else {
                toast.error('Ocorreu um erro ao processar a requisição.');
                console.error('Error Message:', error.message);
            }
        }
        return 0;
    }, []);

    const deleteEscort = useCallback(async (data: iEscortObject) => {
        try {

            const response = await Api.delete(`/Escort/${data.id}`);

            if (response.status === 200) {
                toast.success('O acompanhante foi removido!');
                return response.data as iEscortObject;
            } 
        } catch (error: any) {
            if (error.response) {
                toast.error(error.response.data.message);
            }else {
                toast.error('Ocorreu um erro ao processar a requisição.');
                console.error('Error Message:', error.message);
            }
            return new Error((error as { message: string }).message || 'Erro ao atualizar.')
        }
        return {} as iEscortObject;

    }, []);

    const getEscortById = useCallback(async (filter: { id: number, getPeople: boolean }): Promise<iEscortObject> => {
        const res = await Api.get<iEscortObject[]>('/Escort/', { params: filter });
        const escort = res.data[0];
        return escort;
    }, []);

    return (
        <EscortContext.Provider
            value={{
                Escort,
                setEscort,
                viewEscort,
                registerEscort,
                updateEscort,
                countEscort,
                deleteEscort,

                getEscortById
            }}>
            {children}
        </EscortContext.Provider>
    )
}