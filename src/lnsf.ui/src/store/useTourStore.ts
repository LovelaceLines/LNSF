import { create } from "zustand";

import { tour, tourFilter, queryResult } from "@/types";
import { Axios } from "@/http";

type state = {
  tours: tour[];
  openTours: tour[];
  queryResult: queryResult<tour>;

  getTours: (filters?: tourFilter) => Promise<void>;
  getOpenTours: () => Promise<tour[]>;
  postTour: (tour: tour) => Promise<tour>;
  putTour: (tour: tour) => Promise<tour>;
  putAllTour: (tour: tour) => Promise<tour>;
};

export const useTourStore = create<state>((set) => ({
  tours: [] as tour[],
  openTours: [] as tour[],
  queryResult: {} as queryResult<tour>,

  getTours: async (filters?: tourFilter) => {
    const res = await Axios.get<queryResult<tour>>("/Tour", { params: filters });
    set({ tours: res.data.items });
    set({ queryResult: res.data });
  },

  getOpenTours: async (): Promise<tour[]> => {
    const res = await Axios.get<queryResult<tour>>("/Tour", {
      params: { isOpen: true, sort: "output", page: 1, perPage: 9999 } as tourFilter,
    });
    set({ openTours: res.data.items });
    return res.data.items;
  },

  postTour: async (tour: tour): Promise<tour> => {
    const res = await Axios.post<tour>("/Tour", { ...tour, input: undefined, output: undefined } as tour);
    set((state) => ({ tours: [...state.tours, res.data] }));
    set((state) => ({
      queryResult: { ...state.queryResult, totalCount: state.queryResult.totalCount + 1 },
    }));
    return res.data;
  },

  putTour: async (tour: tour): Promise<tour> => {
    const res = await Axios.put<tour>("/Tour", { ...tour, input: undefined, output: undefined } as tour);
    set((state) => ({ tours: state.tours.map((p) => (p.id === tour.id ? res.data : p)) }));
    return res.data;
  },

  putAllTour: async (tour: tour): Promise<tour> => {
    const res = await Axios.put<tour>("/Tour/put-all", {
      ...tour,
      input: tour.input || undefined,
      output: tour.output || undefined,
    } as tour);
    set((state) => ({ tours: state.tours.map((p) => (p.id === tour.id ? res.data : p)) }));
    return res.data;
  },
}));
