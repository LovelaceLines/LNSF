import { useContext } from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import { AuthContext } from '../Contexts';

export const ProtectedRoutes = () => {
  const { isAuthenticated } = useContext(AuthContext);

  return (
    <>
      { isAuthenticated ? <Outlet /> : <Navigate to="/login" /> }
    </>
  )
}
