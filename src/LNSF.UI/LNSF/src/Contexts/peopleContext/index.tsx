import { createContext, useCallback, useState } from "react";
import { iAddPeopleRoom, iPeopleObject, iPeopleProvider, iPeopleRegister, iPeopleTypes, iPeopleUpdate, iRemovePeopleRoom } from "./type";
import { toast } from "react-toastify";
import { Api } from "../../services/api/axios";
import { Environment } from "../../environment";


export const PeopleContext = createContext({} as iPeopleTypes);

export const PeopleProvider = ({ children }: iPeopleProvider) => {
    const [people, setPeople] = useState<iPeopleObject>({} as iPeopleObject)


    const viewPeople = async (page = 1, filter = '', textFilter = '') => {
        try {
            const urlRelativa = `/People?Page.Page=${page}&Page.PageSize=${Environment.LIMITE_DE_LINHA}&${textFilter}=${filter}`;
            const response = await Api.get(urlRelativa);

            if (response.status === 200) {
                return response.data as iPeopleObject[];
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

    const registerPeople = useCallback(async (data: iPeopleRegister) => {
        try {

            const objetoJSON = JSON.stringify(data);
            console.log('enviar no post: ', objetoJSON)

            const response = await Api.post('/People', objetoJSON)

            if (response.status === 200) {
                toast.success('Quarto cadastrado!');
                return response.data as iPeopleObject;
            }
        } catch (error: any) {
            if (error.response) {
                toast.error(error.response.data.message);
            }else {
                toast.error('Ocorreu um erro ao processar a requisição.');
                console.error('Error Message:', error.message);
            }
            return new Error((error as { message: string }).message || 'Erro ao cadastrar os registros de quarto.')
        }
        return {} as iPeopleObject;
    }, []);

    const updatePeople = useCallback(async (data: iPeopleUpdate) => {
        try {
            const objetoJSON = JSON.stringify(data);
            console.log('enviar: ', objetoJSON)
            const response = await Api.put('/People', objetoJSON);

            if (response.status === 200) {
                toast.success('Quarto atualizado!');
                return response.data as iPeopleObject;
            } else {
                console.log(response)
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
        return {} as iPeopleObject;

    }, []);


    const addPeopleRoom = useCallback(async (data: iAddPeopleRoom) => {
        try {
            const objetoJSON = JSON.stringify(data);
            console.log('enviar: ', objetoJSON)
            const response = await Api.put('/People/add-people-to-room', objetoJSON);

            if (response.status === 200) {
                toast.success('A pessoa foi adicionada ao quarto!');
                return response.data as iPeopleObject;
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
        return {} as iPeopleObject;

    }, []);




    const removePeopleRoom = useCallback(async (data: iRemovePeopleRoom) => {
        try {
            const objetoJSON = JSON.stringify(data);
            console.log('enviar: ', objetoJSON)
            const response = await Api.put('/People/remove-people-from-room', objetoJSON);

            if (response.status === 200) {
                toast.success('A pessoa foi removida do quarto!');
                return response.data as iPeopleObject;
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
        return {} as iPeopleObject;

    }, []);


    const returnQuantity = useCallback(async () => {
        try {
            const response = await Api.get('/People/count');

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
            // return new Error((error as { message: string }).message || 'Erro ao retornar os quantidade de quartos.')
        }
        return 0;
    }, []);


    return (
        <PeopleContext.Provider value={{ people, setPeople, viewPeople, registerPeople, updatePeople, addPeopleRoom, removePeopleRoom, returnQuantity }}>
            {children}
        </PeopleContext.Provider>
    )
}