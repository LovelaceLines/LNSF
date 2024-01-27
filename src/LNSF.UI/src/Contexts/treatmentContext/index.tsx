import { createContext, useCallback } from "react";
import { iTreatment, iTreatmentFilter, iTreatmentPost, iTreatmentProvider, iTreatmentTypes } from "./type";
import { Api } from "../../services/api/axios";

export const TreatmentContext = createContext({} as iTreatmentTypes);

export const TreatmentProvider = ({ children }: iTreatmentProvider) => {

    const getTreatments = useCallback(async (filter?: iTreatmentFilter): Promise<iTreatment[]> => {
        const res = await Api.get<iTreatment[]>('/Treatment', { params: filter });
        const treatments = res.data;
        return treatments;
    }, []);

    const getTreatmentById = useCallback(async (id: number): Promise<iTreatment> => {
        const res = await Api.get<iTreatment>(`/Treatment/${id}`);
        const treatment = res.data;
        return treatment;
    }, []);

    const getCount = useCallback(async (): Promise<number> => {
        const res = await Api.get<number>('/Treatment/count');
        const count = res.data;
        return count;
    }, []);

    const postTreatment = useCallback(async (data: iTreatmentPost): Promise<iTreatment> => {
        const res = await Api.post<iTreatment>('/Treatment', data);
        console.log("res: ", res)
        const treatment = res.data;
        return treatment;
    }, []);

    const putTreatment = useCallback(async (data: iTreatment): Promise<iTreatment> => {
        const res = await Api.put<iTreatment>('/Treatment', data);
        const treatment = res.data;
        return treatment;
    }, []);


    return (
        <TreatmentContext.Provider
            value={{
                getTreatments,
                getTreatmentById,
                getCount,
                postTreatment,
                putTreatment
            }}>
            {children}
        </TreatmentContext.Provider>
    )
}