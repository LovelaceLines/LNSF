import { create } from "zustand";

import { room, roomFilter, queryResult } from "@/types";
import { Axios } from "@/http";

type state = {
  rooms: room[];
  queryResult: queryResult<room>;

  getRoom: (id: string) => Promise<room>;
  getRooms: (filters?: roomFilter) => Promise<void>;
  postRoom: (room: room) => Promise<room>;
  putRoom: (room: room) => Promise<room>;
};

export const useRoomStore = create<state>((set) => ({
  rooms: [] as room[],
  queryResult: {} as queryResult<room>,

  getRoom: async (id) => {
    const res = await Axios.get<queryResult<room>>("/Room", { params: { id: id } });
    return res.data.items[0];
  },

  getRooms: async (filters?: roomFilter) => {
    const res = await Axios.get<queryResult<room>>("/Room", { params: filters });
    set({ rooms: res.data.items });
    set({ queryResult: res.data });
  },

  postRoom: async (room) => {
    const res = await Axios.post<room>("/Room", room);
    set((state) => ({ rooms: [...state.rooms, res.data] }));
    set((state) => ({ queryResult: { ...state.queryResult, totalCount: state.queryResult.totalCount + 1 } }));
    return res.data;
  },

  putRoom: async (room) => {
    const res = await Axios.put<room>("/Room", room);
    set((state) => ({ rooms: state.rooms.map((p) => (p.id === room.id ? res.data : p)) }));
    return res.data;
  },
}));
