import { createContext, useCallback, useState } from "react";
import { iEmergencyContactObject, iEmergencyContactProvider, iEmergencyContactTypes } from "./type";
import { Api } from "../../services/api/axios";
import { toast } from "react-toastify";
import { Environment } from "../../environment";

export const EmergencyContactContext = createContext({} as iEmergencyContactTypes);

export const EmergencyContactProvider = ({ children }: iEmergencyContactProvider) => {
    
    const [emergencyContact, setEmergencyContact] = useState<iEmergencyContactObject>({} as iEmergencyContactObject)
    
    const viewEmergencyContact= async (page = 1, filter = '', textFilter = '') => {
        try {
            const urlRelativa = `/EmergencyContact?Page.Page=${page}&Page.PageSize=${Environment.LIMITE_DE_LINHA}&${textFilter}=${filter}`;
            const response = await Api.get(urlRelativa);

            if (response.status === 200) {
                return response.data as iEmergencyContactObject[];
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
    
    const registerEmergencyContact = useCallback(async (data: iEmergencyContactObject) => {
        try {
           
            const response = await Api.post('/EmergencyContact', JSON.stringify(data))

            if (response.status === 200) {
                toast.success('Contato de emergência cadastrado!');
                return response.data as iEmergencyContactObject;
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
        return {} as iEmergencyContactObject;
    }, []);



    const updateEmergencyContact= useCallback(async (data: iEmergencyContactObject) => {
        try {
         
            const response = await Api.put('/EmergencyContact', JSON.stringify(data));

            if (response.status === 200) {
                toast.success('Contato de emergência atualizado!');
                return response.data as iEmergencyContactObject;
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
        return {} as iEmergencyContactObject;

    }, []);

    const countEmergencyContact = useCallback(async () => {
        try {
            const response = await Api.get('/EmergencyContact/count');

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
    
    const deleteEmergencyContact = useCallback(async (data: iEmergencyContactObject) => {
        try {

            const response = await Api.delete(`/EmergencyContact/${data.id}`);

            if (response.status === 200) {
                toast.success('O contato  de emergência foi removido!');
                return response.data as iEmergencyContactObject;
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
        return {} as iEmergencyContactObject;

    }, []);

    return (
        <EmergencyContactContext.Provider
            value={{
                emergencyContact,
                setEmergencyContact,
                viewEmergencyContact,
                registerEmergencyContact,
                updateEmergencyContact,
                countEmergencyContact,
                deleteEmergencyContact
            }}>
            {children}
        </EmergencyContactContext.Provider>
    )
}