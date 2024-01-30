import { createContext, useCallback, useContext, useState } from "react";
import { Api } from "../../services/api/axios";
import { toast } from "react-toastify";
import { iHostingFilter, iHostingObject, iHostingProvider, iHostingRegister, iHostingTypes, iHosting_ } from "./type";
import { EscortContext } from "../escortContext";
import { HostingEscortContext } from "../hostingEscortContext/hostingEscortContext";
import { iEscortObject } from "../escortContext/type";
import { iHostingEscort } from "../hostingEscortContext/hostingEscortType";

export const HostingContext = createContext({} as iHostingTypes);

export const HostingProvider = ({ children }: iHostingProvider) => {
    const { getEscortById } = useContext(EscortContext);
    const { getEscortsByHostingId } = useContext(HostingEscortContext);
    const [Hosting, setHosting] = useState<iHostingObject>({} as iHostingObject)

    const getHostings = useCallback(async (filter: iHostingFilter): Promise<iHostingObject[]> => {
        const res = await Api.get<iHostingObject[]>('/Hosting', { params: filter });
        const hostings = res.data;
        return hostings;
    }, []);

    const getHostingById = useCallback(async (id: number): Promise<iHostingObject> => {
        const res = await Api.get<iHostingObject[]>(`/Hosting/`, { params: { id } });
        const hosting = res.data[0];
        return hosting;
    }, []);

    const getCount = useCallback(async (): Promise<number> => {
        const res = await Api.get<number>('/Hosting/count');
        const count = res.data;
        return count;
    }, []);

    const postHosting = useCallback(async (data: iHostingRegister): Promise<iHostingObject> => {
        const res = await Api.post<iHostingObject>('/Hosting', data);
        const hosting = res.data;
        return hosting;
    }, []);

    const putHosting = useCallback(async (data: iHostingObject): Promise<iHostingObject> => {
        const res = await Api.put<iHostingObject>('/Hosting', data);
        const hosting = res.data;
        return hosting;
    }, []);


    return (
        <HostingContext.Provider
            value={{
                Hosting,
                setHosting,
               

                getHostings,
                getHostingById,
                getCount,
                postHosting,
                putHosting
            }}>
            {children}
        </HostingContext.Provider>
    )
}