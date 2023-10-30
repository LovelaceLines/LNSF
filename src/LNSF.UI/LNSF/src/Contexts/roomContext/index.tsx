import { createContext, useCallback, useState } from "react";
import { iRoomObject, iRoomProvider, iRoomRegister, iRoomTypes } from "./type";
import { toast } from "react-toastify";
import { Api } from "../../services/api/axios";
import { Environment } from "../../environment";


export const RoomContext = createContext({} as iRoomTypes);

export const RoomProvider = ({ children }: iRoomProvider) => {
    const [room, setRoom] = useState<iRoomObject>({} as iRoomObject)

    const viewRoom = async (page = 1, filter = '', textFilter = '', qntLine = 0) => {
        try {
            const urlRelativa = `/Room?Page.Page=${page}&Page.PageSize=${qntLine === 0 ? Environment.LIMITE_DE_LINHA : qntLine}&${textFilter}=${filter}`;
            const response = await Api.get(urlRelativa);

            if (response.status === 200) {
                return response.data as iRoomObject[];
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

    const registerRoom = useCallback(async (data: iRoomRegister) => {
        try {

            const objetoJSON = JSON.stringify(data);
            console.log('enviar no post: ', objetoJSON)

            const response = await Api.post('/Room', objetoJSON)

            if (response.status === 200) {
                toast.success('Quarto cadastrado!');
                return response.data as iRoomObject;
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
        return {} as iRoomObject;
    }, []);

    const updateRoom = useCallback(async (data: iRoomObject) => {
        try {
            const objetoJSON = JSON.stringify(data);
            console.log('enviar: ', objetoJSON)
            const response = await Api.put('/Room', objetoJSON);

            if (response.status === 200) {
                toast.success('Quarto atualizado!');
                return response.data as iRoomObject;
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
        return {} as iRoomObject;
    }, []);


    const returnQuantity = useCallback(async () => {
        try {
            const response = await Api.get('/Room/count');

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
        <RoomContext.Provider value={{ room, setRoom, viewRoom, registerRoom, updateRoom, returnQuantity }}>
            {children}
        </RoomContext.Provider>
    )
}