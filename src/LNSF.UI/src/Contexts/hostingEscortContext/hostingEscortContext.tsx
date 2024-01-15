import { createContext, useCallback } from "react";
import { iHostingEscort, iHostingEscortProvider, iHostingEscortTypes } from "./hostingEscortType";
import { Api } from "../../services/api/axios";

export const HostingEscortContext = createContext({} as iHostingEscortTypes);

export const HostingEscortProvider = ({ children }: iHostingEscortProvider) => {
  const getEscortsByHostingId = useCallback(async (id: number): Promise<iHostingEscort[]> => {
    const res = await Api.get<iHostingEscort[]>(`/HostingEscort/`, { params: { id: id } });
    const hostingEscort = res.data;
    return hostingEscort;
  }, []);

  return (
    <HostingEscortContext.Provider value={{ getEscortsByHostingId }}>
      {children}
    </HostingEscortContext.Provider>
  )
}