import { jwtDecode } from "jwt-decode";
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";

import { getAllRoles } from "@/globalSettings";
import { defaultRole, jwtPayload } from "@/types";
import { includes } from "@/utils";
import { useAuthStore } from "./store/useAuthStore";

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

      const authenticated =
        user?.id?.toString() == jwt.nameid &&
        user?.userName === jwt.unique_name &&
        includes(
          jwt.role,
          user?.roles?.map((role) => role.name)
        ) &&
        jwt.exp > Date.now() / 1000;

      const authorized = includes(authorizedRoles, jwt.role);

      if (authenticated && authorized) return;
      else if (authenticated && !authorized) {
        navigate("/app/unauthorized");
        return;
      } else {
        refreshToken();
        return;
      }
    }

    navigate("/login");
    logoutUser();
  }, [authToken]);

  return user?.id ? children : null;
};
