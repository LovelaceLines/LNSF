import { createContext, useCallback, useState } from "react";
import { iRoomFilter, iRoomObject, iRoomProvider, iRoomRegister, iRoomTypes } from "./type";
import { toast } from "react-toastify";
import { Api } from "../../services/api/axios";
import { Environment } from "../../environment";
import { Filter } from "@mui/icons-material";


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

    const getRooms = useCallback(async (Filter?: iRoomFilter) => {
        const res = await Api.get<iRoomObject[]>('/Room', { params: Filter });
        const rooms = res.data;
        return rooms;
    }, []);

    const getRoomById = useCallback(async (id: number) => {
        const res = await Api.get<iRoomObject>(`/Room/`, { params: id });
        const room = res.data;
        return room;
    }, []);

    const getCount = useCallback(async () => {
        const res = await Api.get<number>('/Room/count');
        const count = res.data;
        return count;
    }, []);

    const postRoom = useCallback(async (data: iRoomRegister) => {
        const res = await Api.post<iRoomObject>('/Room', data);
        const room = res.data;
        return room;
    }, []);

    const putRoom = useCallback(async (data: iRoomObject) => {
        const res = await Api.put<iRoomObject>('/Room', data);
        const room = res.data;
        return room;
    }, []);


    return (
        <RoomContext.Provider value={{ room, setRoom, viewRoom, registerRoom, updateRoom, returnQuantity, getRooms, getRoomById, getCount, postRoom, putRoom }}>
            {children}
        </RoomContext.Provider>
    )
}