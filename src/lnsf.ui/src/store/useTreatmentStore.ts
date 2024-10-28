import { create } from "zustand";

import { treatment, baseFilter, queryResult } from "@/types";
import { Axios } from "@/http";

type state = {
	treatments: treatment[];
	queryResult: queryResult<treatment>;

	getTreatment: (id: number) => Promise<treatment>;
	getTreatments: (filters?: baseFilter) => Promise<void>;
	postTreatment: (treatment: treatment) => Promise<treatment>;
	putTreatment: (treatment: treatment) => Promise<treatment>;
	deleteTreatment: (id: number) => Promise<treatment>;
};

export const useTreatmentStore = create<state>((set) => ({
	treatments: [] as treatment[],
	queryResult: {} as queryResult<treatment>,

	getTreatment: async (id) => {
		const res = await Axios.get<queryResult<treatment>>("/Treatment", { params: { id: id } });
		return res.data.items[0];
	},

	getTreatments: async (filters?: baseFilter) => {
		const res = await Axios.get<queryResult<treatment>>("/Treatment", { params: filters });
		set({ treatments: res.data.items });
		set({ queryResult: res.data });
	},

	postTreatment: async (treatment) => {
		const res = await Axios.post<treatment>("/Treatment", treatment);
		set((state) => ({ treatments: [...state.treatments, res.data] }));
		set((state) => ({ queryResult: { ...state.queryResult, totalCount: state.queryResult.totalCount + 1 } }));
		return res.data;
	},

	putTreatment: async (treatment) => {
		const res = await Axios.put<treatment>("/Treatment", treatment);
		set((state) => ({ treatments: state.treatments.map((p) => (p.id === treatment.id ? res.data : p)) }));
		return res.data;
	},

	deleteTreatment: async (id): Promise<treatment> => {
		const res = await Axios.delete<treatment>(`/Treatment/${id}`);
		set((state) => ({ treatments: state.treatments.filter((p) => p.id !== id) }));
		set((state) => ({ queryResult: { ...state.queryResult, totalCount: state.queryResult.totalCount - 1 } }));
		return res.data;
	},
}));
