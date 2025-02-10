import { create } from "zustand";

import { escort, baseFilter, queryResult } from "@/types";
import { Axios } from "@/http";

type state = {
  escorts: escort[];
  queryResult: queryResult<escort>;

  getEscort: (id: string) => Promise<escort>;
  getEscorts: (filters?: baseFilter) => Promise<void>;
  postEscort: (escort: escort) => Promise<escort>;
  putEscort: (escort: escort) => Promise<escort>;
  deleteEscort: (id: number) => Promise<escort>;
};

export const useEscortStore = create<state>((set) => ({
  escorts: [] as escort[],
  queryResult: {} as queryResult<escort>,

  getEscort: async (id) => {
    const res = await Axios.get<queryResult<escort>>("/Escort", { params: { id: id } });
    return res.data.items[0];
  },

  getEscorts: async (filters?: baseFilter) => {
    const res = await Axios.get<queryResult<escort>>("/Escort", { params: filters });
    set({ escorts: res.data.items });
    set({ queryResult: res.data });
  },

  postEscort: async (escort) => {
    const res = await Axios.post<escort>("/Escort", escort);
    set((state) => ({ escorts: [...state.escorts, res.data] }));
    set((state) => ({
      queryResult: { ...state.queryResult, totalCount: state.queryResult.totalCount + 1 },
    }));
    return res.data;
  },

  putEscort: async (escort) => {
    const res = await Axios.put<escort>("/Escort", escort);
    set((state) => ({ escorts: state.escorts.map((p) => (p.id === escort.id ? res.data : p)) }));
    return res.data;
  },

  deleteEscort: async (id) => {
    const res = await Axios.delete<escort>(`/Escort/${id}`);
    set((state) => ({ escorts: state.escorts.filter((p) => p.id !== id) }));
    set((state) => ({
      queryResult: { ...state.queryResult, totalCount: state.queryResult.totalCount - 1 },
    }));
    return res.data;
  },
}));
