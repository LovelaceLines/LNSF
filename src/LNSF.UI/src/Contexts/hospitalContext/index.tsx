import { createContext, useCallback, useState } from "react";
import { iHospital, iHospitalFilter, iHospitalObject, iHospitalProvider, iHospitalTypes } from "./type";
import { Api } from "../../services/api/axios";

export const HospitalContext = createContext({} as iHospitalTypes);

export const HospitalProvider = ({ children }: iHospitalProvider) => {

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
                getHospitals,
                getHospitalById,
                getCount,
                postHospital,
                putHospital,
                deleteHospitalById
            }}>
            {children}
        </HospitalContext.Provider>
    )
}