import { createContext, useCallback, useState } from "react";
import { toast } from "react-toastify";
import { Api } from "../../services/api/axios";
import { Environment } from "../../environment";
import { iTourFilter, iTourObject, iTourRegister, iTourTypes, iTourUpdate } from "./type";
import { apiUrl } from "../../environment/environment.temp";

export const TourContext = createContext({} as iTourTypes);

export const TourProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
    const [tour, setTour] = useState<iTourObject>({} as iTourObject)

    const viewTour = async (page = 1, filter = '', textFilter = '', qntLine = 0) => {
        try {

            const urlRelativa = `/Tour?Page.Page=${page}&Page.PageSize=${qntLine === 0 ? Environment.LIMITE_DE_LINHA : qntLine}&${textFilter}=${filter}`;
            const response = await Api.get(urlRelativa);

            if (response.status === 200) {
                return response.data as iTourObject[];
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

    const viewTourOutput = async (input: boolean) => {
        try {

            const urlRelativa = `/Tour?InOpen=${input}`;
            const response = await Api.get(urlRelativa);

            if (response.status === 200) {
                return response.data as iTourObject[];
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

    const registerTour = useCallback(async (data: iTourRegister) => {
        try {

            const objetoJSON = JSON.stringify(data);
            const response = await Api.post('/Tour', objetoJSON)

            if (response.status === 200) {
                toast.success('Saída cadastrada!');
                return response.data as iTourObject;
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
        return {} as iTourObject;
    }, []);

    const updateTour = useCallback(async (data: iTourUpdate) => {
        try {
            const objetoJSON = JSON.stringify(data);
            const response = await Api.put('/Tour', objetoJSON);

            if (response.status === 200) {
                toast.success('Retorno confirmado!');
                return response.data as iTourObject;
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
        return {} as iTourObject;

    }, []);


    const updateAllTour = useCallback(async (data: iTourObject) => {
        try {
            const objetoJSON = JSON.stringify(data);
            const response = await Api.put('/Tour', objetoJSON);

            if (response.status === 200) {
                toast.success('Atualizado!');
                return response.data as iTourObject;
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
        return {} as iTourObject;

    }, []);


    const returnQuantity = useCallback(async () => {
        try {
            const response = await Api.get('/Tour/count');

            if (response.status === 200) {
                return response.data as number;
            }
        } catch (error) {
            toast.error('Ocorreu um erro ao processar a requisição.');
        }
        return 0;
    }, []);

    const getTours = async (filter?: iTourFilter): Promise<iTourObject[]> => {
        const res = await Api.get<iTourObject[]>(`${apiUrl}/Tour`, { params: filter });
        const tours = res.data;
        return tours;
    }

    return (
        <TourContext.Provider value={{ tour, setTour, viewTour, viewTourOutput, registerTour, updateTour, updateAllTour, returnQuantity, getTours }}>
            {children}
        </TourContext.Provider>
    )
}