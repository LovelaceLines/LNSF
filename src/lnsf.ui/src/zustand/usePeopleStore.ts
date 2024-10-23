import { create } from "zustand";

import { people, peopleFilter, peopleRoomHosting, queryResult } from "@/types";
import { Axios } from "@/http";

type state = {
	peoples: people[];
	queryResult: queryResult<people>;

	getPeople: (id: string) => Promise<people>;
	getPeoples: (filters?: peopleFilter) => Promise<void>;
	postPeople: (people: people) => Promise<people>;
	putPeople: (people: people) => Promise<people>;

	addPeopleToRoom: (peopleId: number, roomId: number, hostingId: number) => Promise<peopleRoomHosting>;
	removePeopleFromRoom: (peopleId: number, roomId: number, hostingId: number) => Promise<peopleRoomHosting>;
};

export const usePeopleStore = create<state>((set) => ({
	peoples: [] as people[],
	queryResult: {} as queryResult<people>,

	getPeople: async (id) => {
		const res = await Axios.get<queryResult<people>>("/People", { params: { id: id } });
		return res.data.items[0];
	},

	getPeoples: async (filters?: peopleFilter) => {
		const res = await Axios.get<queryResult<people>>("/People", { params: filters });
		set({ peoples: res.data.items });
		set({ queryResult: res.data });
	},

	postPeople: async (people) => {
		const res = await Axios.post<people>("/People", people);
		set((state) => ({ peoples: [...state.peoples, res.data] }));
		set((state) => ({ queryResult: { ...state.queryResult, totalCount: state.queryResult.totalCount + 1 } }));
		return res.data;
	},

	putPeople: async (people) => {
		const res = await Axios.put<people>("/People", people);
		set((state) => ({ peoples: state.peoples.map((p) => (p.id === people.id ? res.data : p)) }));
		return res.data;
	},

	addPeopleToRoom: async (peopleId: number, roomId: number, hostingId: number): Promise<peopleRoomHosting> => {
		const res = await Axios.post<peopleRoomHosting>("/People/add-people-to-room", {
			peopleId: peopleId,
			roomId: roomId,
			hostingId: hostingId,
		} as peopleRoomHosting);
		return res.data;
	},

	removePeopleFromRoom: async (peopleId: number, roomId: number, hostingId: number): Promise<peopleRoomHosting> => {
		const res = await Axios.delete<peopleRoomHosting>("/People/remove-people-from-room", {
			data: { peopleId: peopleId, roomId: roomId, hostingId: hostingId } as peopleRoomHosting,
		});
		return res.data;
	},
}));
