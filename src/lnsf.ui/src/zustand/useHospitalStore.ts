import { create } from "zustand";

import { hospital, hospitalFilter, queryResult } from "@/types";
import { Axios } from "@/http";

type state = {
	hospitals: hospital[];
	queryResult: queryResult<hospital>;

	getHospital: (id: string) => Promise<hospital>;
	getHospitals: (filters?: hospitalFilter) => Promise<void>;
	postHospital: (hospital: hospital) => Promise<hospital>;
	putHospital: (hospital: hospital) => Promise<hospital>;
};

export const useHospitalStore = create<state>((set) => ({
	hospitals: [] as hospital[],
	queryResult: {} as queryResult<hospital>,

	getHospital: async (id) => {
		const res = await Axios.get<queryResult<hospital>>("/Hospital", { params: { id: id } });
		return res.data.items[0];
	},

	getHospitals: async (filters?: hospitalFilter) => {
		const res = await Axios.get<queryResult<hospital>>("/Hospital", { params: filters });
		set({ hospitals: res.data.items });
		set({ queryResult: res.data });
	},

	postHospital: async (hospital) => {
		const res = await Axios.post<hospital>("/Hospital", hospital);
		set((state) => ({ hospitals: [...state.hospitals, res.data] }));
		set((state) => ({ queryResult: { ...state.queryResult, totalCount: state.queryResult.totalCount + 1 } }));
		return res.data;
	},

	putHospital: async (hospital) => {
		const res = await Axios.put<hospital>("/Hospital", hospital);
		set((state) => ({ hospitals: state.hospitals.map((p) => (p.id === hospital.id ? res.data : p)) }));
		return res.data;
	},
}));
