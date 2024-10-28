import { create } from "zustand";

import { Axios } from "@/http";
import { password, queryResult, user, userFilter, userRole } from "@/types";

type state = {
	users: user[];
	queryResult: queryResult<user>;

	getUser: (id: number) => Promise<user>;
	getUsers: (filter?: userFilter) => Promise<void>;
	postUser: (user: user) => Promise<user>;
	putUser: (user: user) => Promise<user>;
	putPassword: (password: password) => Promise<user>;

	addUserToRole: (userId: number, roleId: number) => Promise<userRole>;
	removeUserFromRole: (userId: number, roleId: number) => Promise<userRole>;
};

export const useUserStore = create<state>((set) => ({
	users: [],
	queryResult: {} as queryResult<user>,

	getUser: async (id: number): Promise<user> => {
		const res = await Axios.get<queryResult<user>>("/User", { params: { id: id } });
		return res.data.items[0];
	},

	getUsers: async (filter?: userFilter): Promise<void> => {
		const res = await Axios.get<queryResult<user>>("/User", { params: filter });
		set({ users: res.data.items });
		set({ queryResult: res.data });
	},

	postUser: async (user: user): Promise<user> => {
		const res = await Axios.post<user>("/User", user);
		set((state) => ({ users: [...state.users, res.data] }));
		set((state) => ({ queryResult: { ...state.queryResult, totalCount: state.queryResult.totalCount + 1 } }));
		return res.data;
	},

	putUser: async (user: user): Promise<user> => {
		const res = await Axios.put<user>("/User", user);
		set((state) => ({ users: state.users.map((u) => (u.id === user.id ? res.data : u)) }));
		return res.data;
	},

	putPassword: async (password: password): Promise<user> => {
		const res = await Axios.put<user>("/User/password", password);
		return res.data;
	},

	addUserToRole: async (userId: number, roleId: number): Promise<userRole> => {
		const res = await Axios.post<userRole>("/User/add-user-to-role", { userId: userId, roleId: roleId });
		return res.data;
	},

	removeUserFromRole: async (userId: number, roleId: number): Promise<userRole> => {
		const res = await Axios.delete<userRole>("/User/remove-user-from-role", { data: { userId: userId, roleId: roleId } });
		return res.data;
	},
}));
