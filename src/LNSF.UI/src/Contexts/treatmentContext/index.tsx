import { createContext, useCallback, useState } from "react";
import { iTreatment, iTreatmentObject, iTreatmentProvider, iTreatmentTypes } from "./type";
import { Api } from "../../services/api/axios";
import { toast } from "react-toastify";
import { Environment } from "../../environment";

export const TreatmentContext = createContext({} as iTreatmentTypes);

export const TreatmentProvider = ({ children }: iTreatmentProvider) => {
    
    const [treatment, setTreatment] = useState<iTreatment[]>([]);

    const viewTreatment= async (page = 1, filter = '', textFilter = '') => {
        try {
            const urlRelativa = `/Treatment?Page.Page=${page}&Page.PageSize=${Environment.LIMITE_DE_LINHA}&${textFilter}=${filter}`;
            const response = await Api.get(urlRelativa);

            if (response.status === 200) {
                return response.data as iTreatment[];
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
    
    const registerTreatment = useCallback(async (data: iTreatmentObject) => {
        try {
           
            const response = await Api.post('/Treatment', JSON.stringify(data))

            if (response.status === 200) {
                toast.success('Tratamento cadastrado!');
                setTreatment(response.data)
                return response.data as iTreatment;
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
        return {} as iTreatment;
    }, []);



    const updateTreatment= useCallback(async (data: iTreatmentObject) => {
        try {
   
            const response = await Api.put('/Treatment', JSON.stringify(data));

            if (response.status === 200) {
                toast.success('Tratamento atualizado!');
                return response.data as iTreatmentObject;
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
        return {} as iTreatmentObject;

    }, []);

    const countTreatment = useCallback(async () => {
        try {
            const response = await Api.get('/Treatment/count');

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

    const deleteTreatment = useCallback(async (data: string) => {
        try {
            
            const response = await Api.delete(`/Treatment/${data}`);

            if (response.status === 200) {
                toast.success('Tratamento deletado!');
                return response.data as iTreatment;
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
        return {} as iTreatment;

    }, []);


    return (
        <TreatmentContext.Provider
            value={{
                treatment,
                setTreatment,
                viewTreatment,
                registerTreatment,
                updateTreatment,
                countTreatment,
                deleteTreatment
            }}>
            {children}
        </TreatmentContext.Provider>
    )
}