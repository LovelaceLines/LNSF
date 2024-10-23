import { jwtDecode } from "jwt-decode";
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";

import { getAllRoles } from "@/globalSettings";
import { defaultRole, jwtPayload } from "@/types";
import { includes } from "@/utils";
import { useAuthStore } from "./zustand/useAuthStore";

export const AuthWrapper = ({
	children,
	authorizedRoles = getAllRoles(),
}: Readonly<{ children: React.ReactNode; authorizedRoles?: defaultRole[] }>) => {
	const { authToken, user, logoutUser, refreshToken } = useAuthStore();

	const navigate = useNavigate();

	useEffect(() => {
		if (authToken?.accessToken) {
			const jwt: jwtPayload = jwtDecode(authToken?.accessToken || "");

			if (typeof jwt.role === "string") jwt.role = [jwt.role];

			const authorized =
				user?.id?.toString() == jwt.nameid &&
				user?.userName === jwt.unique_name &&
				includes(authorizedRoles, jwt.role) &&
				includes(
					jwt.role,
					user?.roles.map((role) => role.name)
				) &&
				jwt.exp > Date.now() / 1000;

			if (authorized) return;
			else {
				refreshToken();
				return;
			}
		}

		navigate("/login");
		logoutUser();
	}, [authToken]);

	return user?.id ? children : null;
};
