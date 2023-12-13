import { createContext, useCallback, useState } from "react";
import { Api } from "../../services/api/axios";
import { toast } from "react-toastify";
import { iHostingObject, iHostingProvider, iHostingTypes, iHosting_ } from "./type";

export const HostingContext = createContext({} as iHostingTypes);

export const HostingProvider = ({ children }: iHostingProvider) => {
    
    const [Hosting, setHosting] = useState<iHostingObject>({} as iHostingObject)
    
    const viewHosting= async (page = 1, filter = '', textFilter = '') => {
        try {
            const urlRelativa = `/Hosting?Page.Page=${page}&Page.PageSize=5&${textFilter}=${filter}`;
            const response = await Api.get(urlRelativa);

            if (response.status === 200) {
                return response.data as iHostingObject[]; 
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
    
    const registerHosting = useCallback(async (data: iHostingObject) => {
        try {
           
            const response = await Api.post('/Hosting', JSON.stringify(data))

            if (response.status === 200) {
                toast.success('Hospedagem cadastrada!');
                return response.data as iHostingObject;
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
        return {} as iHostingObject;
    }, []);



    const updateHosting= useCallback(async (data: iHostingObject) => {
        try {
            const objetoJSON = JSON.stringify(data);
           
            const response = await Api.put('/Hosting', objetoJSON);

            if (response.status === 200) {
                toast.success('Hospedagem atualizada!');
                return response.data as iHostingObject;
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
        return {} as iHostingObject;

    }, []);

    const countHosting = useCallback(async () => {
        try {
            const response = await Api.get('/Hosting/count');

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


    const registerEscortToFromHosting = useCallback(async (data: iHosting_) => {
        try {
           
            const response = await Api.post('/Hosting/add-escort-to-hosting', JSON.stringify(data))

            if (response.status === 200) {
                toast.success('Acompanhante cadastrado hospedagem!');
                return response.data as iHosting_;
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
        return {} as iHosting_;
    }, []);

    const deleteEscortHosting = useCallback(async (data: iHosting_) => {
        try {
           
            const response = await Api.post('/Hosting/remove-escort-from-hosting', JSON.stringify(data))

            if (response.status === 200) {
                toast.success('Acompanhante removido da hospedagem!');
                return response.data as iHostingObject;
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
        return {} as iHostingObject;
    }, []);

    const viewHostingEscort= async (page = 1, filter = '', textFilter = '') => {
        try {
            const urlRelativa = `/HostingEscort?Page.Page=${page}&Page.PageSize=5&${textFilter}=${filter}`;
            const response = await Api.get(urlRelativa);

            if (response.status === 200) {
                return response.data as iHosting_[]; 
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

    const countHostingEscort = useCallback(async () => {
        try {
            const response = await Api.get('/HostingEscort/count');

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

    return (
        <HostingContext.Provider
            value={{
                Hosting,
                setHosting,
                viewHosting,
                registerHosting,
                updateHosting,
                countHosting,
                registerEscortToFromHosting,
                deleteEscortHosting,
                viewHostingEscort,
                countHostingEscort,
            }}>
            {children}
        </HostingContext.Provider>
    )
}