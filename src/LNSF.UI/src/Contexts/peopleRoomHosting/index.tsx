import { createContext, useCallback } from "react";
import { iPeopleRoomHostingFilter, iPeopleRoomHostingObject, iPeopleRoomHostingTypes } from "./type";
import { Api } from "../../services/api/axios";

export const PeopleRoomHostingContext = createContext({} as iPeopleRoomHostingTypes);

export const PeopleRoomHostingProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const getPeoplesRoomsHostings = useCallback(async (filter?: iPeopleRoomHostingFilter): Promise<iPeopleRoomHostingObject[]> => {
    const res = await Api.get<iPeopleRoomHostingObject[]>('/peopleRoomHosting', { params: filter });
    const peoplesRoomsHostings = res.data;
    return peoplesRoomsHostings;
  }, []);

  const getPeopleRoomHostingById = useCallback(async ({ peopleId, roomId, hostingId }: iPeopleRoomHostingFilter): Promise<iPeopleRoomHostingObject> => {
    const res = await Api.get<iPeopleRoomHostingObject[]>('/peopleRoomHosting', { params: { peopleId, roomId, hostingId } });
    const peopleRoomHosting = res.data[0];
    return peopleRoomHosting;
  }, []);

  const getCount = useCallback(async (): Promise<number> => {
    const res = await Api.get<number>('/peopleRoomHosting/count');
    const count = res.data;
    return count;
  }, []);

  return (
    <PeopleRoomHostingContext.Provider value={{ getPeoplesRoomsHostings, getPeopleRoomHostingById, getCount }}>
      {children}
    </PeopleRoomHostingContext.Provider>
  )
}