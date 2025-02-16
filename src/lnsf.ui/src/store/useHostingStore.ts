import { create } from "zustand";

import { hosting, hostingFilter, queryResult, hostingEscort, baseFilter } from "@/types";
import { Axios } from "@/http";

type state = {
  hostings: hosting[];
  queryResult: queryResult<hosting>;

  getHosting: (id: number) => Promise<hosting>;
  getHostings: (filters?: hostingFilter) => Promise<void>;
  postHosting: (hosting: hosting) => Promise<hosting>;
  putHosting: (hosting: hosting) => Promise<hosting>;
  deleteHosting: (id: number) => Promise<hosting>;

  addEscortToHosting: (hostingEscort: hostingEscort) => Promise<hostingEscort>;
  removeEscortFromHosting: (hostingEscort: hostingEscort) => Promise<hostingEscort>;

  hostingsEscorts: hostingEscort[];
  getHostingsEscorts: (filters?: baseFilter) => Promise<hostingEscort[]>;
};

export const useHostingStore = create<state>((set) => ({
  hostings: [] as hosting[],
  queryResult: {} as queryResult<hosting>,

  getHosting: async (id: number): Promise<hosting> => {
    const res = await Axios.get<queryResult<hosting>>("/Hosting", { params: { id: id } });
    return res.data.items[0];
  },

  getHostings: async (filters?: hostingFilter): Promise<void> => {
    const res = await Axios.get<queryResult<hosting>>("/Hosting", { params: filters });
    set({ hostings: res.data.items });
    set({ queryResult: res.data });
  },

  postHosting: async (hosting): Promise<hosting> => {
    const res = await Axios.post<hosting>("/Hosting", hosting);
    set((state) => ({ hostings: [...state.hostings, res.data] }));
    set((state) => ({
      queryResult: { ...state.queryResult, totalCount: state.queryResult.totalCount + 1 },
    }));
    return res.data;
  },

  putHosting: async (hosting): Promise<hosting> => {
    const res = await Axios.put<hosting>("/Hosting", hosting);
    set((state) => ({ hostings: state.hostings.map((p) => (p.id === hosting.id ? res.data : p)) }));
    return res.data;
  },

  deleteHosting: async (id: number): Promise<hosting> => {
    const res = await Axios.delete<hosting>(`/Hosting/${id}`);
    set((state) => ({ hostings: state.hostings.filter((p) => p.id !== id) }));
    set((state) => ({
      queryResult: { ...state.queryResult, totalCount: state.queryResult.totalCount - 1 },
    }));
    return res.data;
  },

  addEscortToHosting: async (hostingEscort: hostingEscort): Promise<hostingEscort> => {
    const res = await Axios.post<hostingEscort>("/Hosting/add-escort-to-hosting", hostingEscort);
    return res.data;
  },

  removeEscortFromHosting: async (hostingEscort: hostingEscort): Promise<hostingEscort> => {
    const res = await Axios.delete<hostingEscort>("/Hosting/remove-escort-from-hosting", {
      data: hostingEscort,
    });
    return res.data;
  },

  hostingsEscorts: [] as hostingEscort[],

  getHostingsEscorts: async (filters?: baseFilter): Promise<hostingEscort[]> => {
    const res = await Axios.get<queryResult<hostingEscort>>("/HostingEscort", { params: filters });
    set({ hostingsEscorts: res.data.items });
    return res.data.items;
  },
}));
