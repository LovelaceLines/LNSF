import { createContext, useCallback, useState } from "react";
import { iHospital, iHospitalFilter, iHospitalObject, iHospitalProvider, iHospitalTypes } from "./type";
import { Api } from "../../services/api/axios";
import { toast } from "react-toastify";
import { Environment } from "../../environment";

export const HospitalContext = createContext({} as iHospitalTypes);

export const HospitalProvider = ({ children }: iHospitalProvider) => {
    
    const [hospital, setHospital] = useState<iHospitalObject>({} as iHospitalObject)
    
    const viewHospital= async (page = 1, filter = '', textFilter = '') => {
        try {
            const urlRelativa = `/Hospital?Page.Page=${page}&Page.PageSize=${Environment.LIMITE_DE_LINHA}&${textFilter}=${filter}`;
            const response = await Api.get(urlRelativa);

            if (response.status === 200) {
                return response.data as iHospitalObject[];
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
    
    const registerHospital = useCallback(async (data: iHospitalObject) => {
        try {

            const objetoJSON = JSON.stringify(data);
           
            const response = await Api.post('/Hospital', objetoJSON)

            if (response.status === 200) {
                toast.success('Hospital cadastrado!');
                return response.data as iHospitalObject;
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
        return {} as iHospitalObject;
    }, []);



    const updateHospital= useCallback(async (data: iHospitalObject) => {
        try {
            const objetoJSON = JSON.stringify(data);
           
            const response = await Api.put('/Hospital', objetoJSON);

            if (response.status === 200) {
                toast.success('Hospital atualizado!');
                return response.data as iHospitalObject;
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
        return {} as iHospitalObject;

    }, []);

    const countHospital = useCallback(async () => {
        try {
            const response = await Api.get('/Hospital/count');

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

    const deleteHospital = useCallback(async (data: string) => {
        try {
            
            const response = await Api.delete(`/Hospital/${data}`);

            if (response.status === 200) {
                toast.success('Hospital deletado!');
                return response.data as iHospitalObject;
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
        return {} as iHospitalObject;

    }, []);

    const getHospitals = useCallback(async (filter?: iHospitalFilter): Promise<iHospitalObject[]> => {
        const res = await Api.get<iHospitalObject[]>('/Hospital', { params: filter });
        const hospitals = res.data;
        return hospitals;
    }, []);

    const getHospitalById = useCallback(async (id: number): Promise<iHospitalObject> => {
        const res = await Api.get<iHospitalObject[]>(`/Hospital/`, { params: { id } });
        const hospital = res.data[0];
        return hospital;
    }, []);

    const getCount = useCallback(async (): Promise<number> => {
        const res = await Api.get<number>('/Hospital/count');
        const count = res.data;
        return count;
    }, []);

    const postHospital = useCallback(async (data: iHospital): Promise<iHospitalObject> => {
        const res = await Api.post<iHospitalObject>('/Hospital', data);
        const hospital = res.data;
        return hospital;
    }, []);

    const putHospital = useCallback(async (data: iHospitalObject): Promise<iHospitalObject> => {
        const res = await Api.put<iHospitalObject>('/Hospital', data);
        const hospital = res.data;
        return hospital;
    }, []);

    const deleteHospitalById = useCallback(async (id: string): Promise<iHospitalObject> => {
        const res = await Api.delete<iHospitalObject>(`/Hospital/${id}`);
        const hospital = res.data;
        return hospital;
    }, []);


    return (
        <HospitalContext.Provider
            value={{
                hospital,
                setHospital,
                viewHospital,
                registerHospital,
                updateHospital,
                countHospital,
                deleteHospital,
                getHospitals,
                getHospitalById,
                getCount,
                postHospital,
                putHospital
            }}>
            {children}
        </HospitalContext.Provider>
    )
}