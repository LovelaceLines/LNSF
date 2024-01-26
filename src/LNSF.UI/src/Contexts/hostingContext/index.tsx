import { createContext, useCallback, useContext, useState } from "react";
import { Api } from "../../services/api/axios";
import { toast } from "react-toastify";
import { iHostingFilter, iHostingObject, iHostingProvider, iHostingRegister, iHostingTypes, iHosting_ } from "./type";
import { EscortContext } from "../escortContext";
import { HostingEscortContext } from "../hostingEscortContext/hostingEscortContext";
import { iEscortObject } from "../escortContext/type";
import { iHostingEscort } from "../hostingEscortContext/hostingEscortType";

export const HostingContext = createContext({} as iHostingTypes);

export const HostingProvider = ({ children }: iHostingProvider) => {
    const { getEscortById } = useContext(EscortContext);
    const { getEscortsByHostingId } = useContext(HostingEscortContext);
    const [Hosting, setHosting] = useState<iHostingObject>({} as iHostingObject)

    const viewHosting = async (page = 1, filter = '', textFilter = '') => {
        try {
            const urlRelativa = `/Hosting?Page.Page=${page}&Page.PageSize=5&${textFilter}=${filter}`;
            const response = await Api.get(urlRelativa);

            if (response.status === 200) {
                return response.data as iHostingObject[];
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
            } else {
                toast.error('Ocorreu um erro ao processar a requisição.');
                console.error('Error Message:', error.message);
            }
            return new Error((error as { message: string }).message || 'Erro ao cadastrar.')
        }
        return {} as iHostingObject;
    }, []);



    const updateHosting = useCallback(async (data: iHostingObject) => {
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
            } else {
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
            } else {
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
            } else {
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
            } else {
                toast.error('Ocorreu um erro ao processar a requisição.');
                console.error('Error Message:', error.message);
            }
            return new Error((error as { message: string }).message || 'Erro ao cadastrar.')
        }
        return {} as iHostingObject;
    }, []);

    const viewHostingEscort = async (page = 1, filter = '', textFilter = '') => {
        try {
            const urlRelativa = `/HostingEscort?Page.Page=${page}&Page.PageSize=5&${textFilter}=${filter}`;
            const response = await Api.get(urlRelativa);

            if (response.status === 200) {
                return response.data as iHosting_[];
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
            } else {
                toast.error('Ocorreu um erro ao processar a requisição.');
                console.error('Error Message:', error.message);
            }
        }
        return 0;
    }, []);

    const getHostings = useCallback(async (filter: iHostingFilter): Promise<iHostingObject[]> => {
        const res = await Api.get<iHostingObject[]>('/Hosting', { params: filter });
        const hostings = res.data;
        return hostings;
    }, []);

    const getHostingById = useCallback(async (id: number): Promise<iHostingObject> => {
        const res = await Api.get<iHostingObject[]>(`/Hosting/`, { params: { id } });
        const hosting = res.data[0];
        return hosting;
    }, []);

    const getCount = useCallback(async (): Promise<number> => {
        const res = await Api.get<number>('/Hosting/count');
        const count = res.data;
        return count;
    }, []);

    const postHosting = useCallback(async (data: iHostingRegister): Promise<iHostingObject> => {
        const res = await Api.post<iHostingObject>('/Hosting', data);
        const hosting = res.data;
        return hosting;
    }, []);

    const putHosting = useCallback(async (data: iHostingObject): Promise<iHostingObject> => {
        const res = await Api.put<iHostingObject>('/Hosting', data);
        const hosting = res.data;
        return hosting;
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

                getHostings,
                getHostingById,
                getCount,
                postHosting,
                putHosting
            }}>
            {children}
        </HostingContext.Provider>
    )
}