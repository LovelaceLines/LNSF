import { create } from "zustand";

import { Axios } from "@/http";
import { queryResult, log, logFilter, sortOrder } from "@/types";

type state = {
	logs: log[];
	queryResult: queryResult<log>;

	getLogs: (filter?: logFilter) => Promise<void>;
};

export const useLogStore = create<state>((set) => ({
	logs: [],
	queryResult: {} as queryResult<log>,

	getLogs: async (filter?: logFilter): Promise<void> => {
		const res = await Axios.get<queryResult<log>>("/LogEntry", {
			params: { ...filter, sort: filter?.sort ?? "id", sortBy: filter?.sortBy ?? sortOrder.desc },
		});
		set({ logs: res.data.items });
		set({ queryResult: res.data });
	},
}));
