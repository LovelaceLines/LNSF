import { create } from "zustand";

import { queryResult, peopleRoomHosting, peopleRoomHostingFilter } from "@/types";
import { Axios } from "@/http";

type state = {
	prh: peopleRoomHosting[];
	queryResult: queryResult<peopleRoomHosting>;

	getPeopleRoomHostingByHostingId: (hostingId: number) => Promise<peopleRoomHosting[]>;
	getPeopleRoomHosting: (filter?: peopleRoomHostingFilter) => Promise<peopleRoomHosting[]>;
};

export const usePeopleRoomHostingStore = create<state>((set) => ({
	prh: [],
	queryResult: {} as queryResult<peopleRoomHosting>,

	getPeopleRoomHostingByHostingId: async (hostingId: number): Promise<peopleRoomHosting[]> => {
		const res = await Axios.get<queryResult<peopleRoomHosting>>("/PeopleRoomHosting", {
			params: { hostingId: hostingId },
		});
		set({ prh: res.data.items });
		set({ queryResult: res.data });
		return res.data.items;
	},

	getPeopleRoomHosting: async (filter?: peopleRoomHostingFilter): Promise<peopleRoomHosting[]> => {
		const res = await Axios.get<queryResult<peopleRoomHosting>>("/PeopleRoomHosting", { params: filter });
		set({ prh: res.data.items });
		set({ queryResult: res.data });
		return res.data.items;
	},
}));
