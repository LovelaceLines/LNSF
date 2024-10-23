import { create } from "zustand";

import { Axios } from "@/http";
import { getAuthToken, getUser, setAuthToken, setUser } from "@/services";
import { authToken, login, user, userToken } from "@/types";

type state = {
	authToken?: authToken;
	user?: user;

	logoutUser: () => void;
	loginUser: (loginData: login) => Promise<userToken>;
	refreshToken: () => Promise<authToken>;
	currentUser: () => Promise<user>;
};

export const useAuthStore = create<state>((set) => ({
	authToken: getAuthToken(),
	user: getUser(),

	logoutUser: () => {
		set({ authToken: undefined, user: undefined });
		setAuthToken({} as authToken);
		setUser({} as user);
	},

	loginUser: async (loginData: login): Promise<userToken> => {
		const res = await Axios.post<userToken>("/Auth/login", loginData);
		const data = res.data;

		set({ authToken: data.authToken, user: data.user });
		setAuthToken(data.authToken);
		setUser(data.user);

		return data;
	},

	refreshToken: async () => {
		const { refreshToken } = getAuthToken() || { refreshToken: "" };
		setAuthToken({ accessToken: refreshToken, refreshToken: refreshToken } as authToken);

		const res = await Axios.get<authToken>("/Auth/refresh-token");
		const data = res.data;

		set({ authToken: data });
		setAuthToken(data);

		return data;
	},

	currentUser: async () => {
		const res = await Axios.get<user>("/Auth/user");
		const data = res.data;

		set({ user: data });
		setUser(data);

		return data;
	},
}));
