import { create } from "zustand";

import {
	chainCountPeopleHostedFilter,
	people,
	peopleHosted,
	peopleRoomHosting,
	queryResult,
	treatment,
} from "@/types";
import { Axios } from "@/http";
import { chainIntervalCheckFilter, chainDayFilter } from "@/types/chain";

export type typeTreatmentCount = treatment & { totalCount: number };

type state = {
	peopleHosted: peopleHosted;
	peopleWillHosted: people[];
	peopleWillBirthdate: peopleRoomHosting[];
	typeTreatmentCount: typeTreatmentCount[];

	getCountPeopleHosted: (filter: chainCountPeopleHostedFilter) => Promise<void>;
	getPeopleWillHosted: (filter: chainDayFilter) => Promise<void>;
	getPeopleWillBirthdate: (filter: chainDayFilter) => Promise<void>;
	getCountTypeTreatment: (filter: chainIntervalCheckFilter) => Promise<void>;
};

export const useChainStore = create<state>((set) => ({
	peopleHosted: { count: 0 } as peopleHosted,
	peopleWillHosted: [],
	peopleWillBirthdate: [],
	typeTreatmentCount: [],

	getCountPeopleHosted: async (filter: chainCountPeopleHostedFilter): Promise<void> => {
		const res = await Axios.get<number>("/Chain/count-people-hosted", { params: filter });
		set((state) => ({ peopleHosted: { ...state.peopleHosted, count: res.data } }));
	},

	getPeopleWillHosted: async (filter: chainDayFilter): Promise<void> => {
		const res = await Axios.get<queryResult<people>>("/Chain/people-will-hosted", { params: filter });
		set({ peopleWillHosted: res.data.items });
	},

	getPeopleWillBirthdate: async (filter: chainDayFilter): Promise<void> => {
		const res = await Axios.get<peopleRoomHosting[]>("/Chain/people-will-birthday", {
			params: filter,
		});
		set({ peopleWillBirthdate: res.data });
	},

	getCountTypeTreatment: async (filter: chainIntervalCheckFilter): Promise<void> => {
		const res = await Axios.get<typeTreatmentCount[]>("/Chain/count-type-treatment", { params: filter });
		set({ typeTreatmentCount: res.data });
	},
}));
