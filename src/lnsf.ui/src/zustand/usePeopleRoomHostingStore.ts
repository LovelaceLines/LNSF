import { create } from "zustand";

import { queryResult, peopleRoomHosting } from "@/types";
import { Axios } from "@/http";

type state = {
	getPeopleRoomHostingByHostingId: (hostingId: number) => Promise<peopleRoomHosting[]>;
};

export const usePeopleRoomHostingStore = create<state>((set) => ({
	getPeopleRoomHostingByHostingId: async (hostingId: number): Promise<peopleRoomHosting[]> => {
		const res = await Axios.get<queryResult<peopleRoomHosting>>("/PeopleRoomHosting", { params: { hostingId: hostingId } });
		return res.data.items;
	},
}));
