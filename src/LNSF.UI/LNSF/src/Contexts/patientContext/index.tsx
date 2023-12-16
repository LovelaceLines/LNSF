import { createContext, useCallback, useState } from "react";
import { Api } from "../../services/api/axios";
import { toast } from "react-toastify";
import { Environment } from "../../environment";
import { iPatient, iPatientObject, iPatientProvider, iPatientTypes } from "./type";

export const PatientContext = createContext({} as iPatientTypes);

export const PatientProvider = ({ children }: iPatientProvider) => {

    const [Patient, setPatient] = useState<iPatientObject>({} as iPatientObject)

    const viewPatient = async (page = 1, filter = '', textFilter = ''): Promise<iPatientObject[] | Error> => {
        try {
            const urlRelativa = `/Patient?Page.Page=${page}&Page.PageSize=${Environment.LIMITE_DE_LINHA}&${textFilter}=${filter}`;
            const response = await Api.get(urlRelativa);

            if (response.status === 200) {
                return response.data as iPatientObject[];
            }
        } catch (error) {
            if (error.response) {
                toast.error(error.response.data.message);
            } else {
                toast.error('Ocorreu um erro ao processar a requisição.');
                console.error('Error Message:', error.message);
            }
            throw new Error((error as { message: string }).message || 'Ops, não foi possível acessar o banco.');
        }

        // If the response status is not 200, you may want to throw an error or handle it appropriately.
        throw new Error('Unexpected response status from the server.');
    };

    const registerPatient = useCallback(async (data: iPatient) => {
        try {

            const objetoJSON = JSON.stringify(data);
            console.log("obejto: ", objetoJSON)

            const response = await Api.post('/Patient', objetoJSON)

            if (response.status === 200) {
                toast.success('Paciente cadastrado!');
                return response.data as iPatientObject;
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
        return {} as iPatientObject;
    }, []);



    const updatePatient = useCallback(async (data: iPatientObject) => {
        try {
            const objetoJSON = JSON.stringify(data);

            const response = await Api.put('/Patient', objetoJSON);

            if (response.status === 200) {
                toast.success('Paciente atualizado!');
                return response.data as iPatientObject;
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
        return {} as iPatientObject;

    }, []);

    const countPatient = useCallback(async () => {
        try {
            const response = await Api.get('/Patient/Count');

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


    return (
        <PatientContext.Provider
            value={{
                Patient,
                setPatient,
                viewPatient,
                registerPatient,
                updatePatient,
                countPatient
            }}>
            {children}
        </PatientContext.Provider>
    )
}