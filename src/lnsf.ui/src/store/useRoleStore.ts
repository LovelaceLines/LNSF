import { create } from "zustand";

import { Axios } from "@/http";
import { queryResult, role, roleFilter } from "@/types";

type state = {
  roles: role[];
  queryResult: queryResult<role>;

  getRole: (id: number) => Promise<role>;
  getRoles: (filter?: roleFilter) => Promise<void>;
  postRole: (role: role) => Promise<role>;
  putRole: (role: role) => Promise<role>;
};

export const useRoleStore = create<state>((set) => ({
  roles: [],
  queryResult: {} as queryResult<role>,

  getRole: async (id: number): Promise<role> => {
    const res = await Axios.get<role>(`/Role/${id}`);
    return res.data;
  },

  getRoles: async (filter?: roleFilter): Promise<void> => {
    const res = await Axios.get<queryResult<role>>("/Role", { params: filter });
    set({ roles: res.data.items });
    set({ queryResult: res.data });
  },

  postRole: async (role: role): Promise<role> => {
    const res = await Axios.post<role>("/Role", role);
    set((state) => ({ roles: [...state.roles, res.data] }));
    set((state) => ({ queryResult: { ...state.queryResult, totalCount: state.queryResult.totalCount + 1 } }));
    return res.data;
  },

  putRole: async (role: role): Promise<role> => {
    const res = await Axios.put<role>("/Role", role);
    set((state) => ({ roles: state.roles.map((u) => (u.id === role.id ? res.data : u)) }));
    return res.data;
  },
}));
