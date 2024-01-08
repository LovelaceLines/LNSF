import { useContext } from "react";
import { AuthContext } from "../Contexts";
import { Navigate, Outlet } from "react-router-dom";

export const ProtectedRoutes = () => {
    const { isAuthenticated } = useContext(AuthContext);
    return isAuthenticated  ? <Outlet /> : <Navigate to="/login" />;
  }