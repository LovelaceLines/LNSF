import { useContext, useEffect } from 'react';
import { Navigate, Outlet, useNavigate} from 'react-router-dom'
import { AuthContext } from '../Contexts';
import { toast } from 'react-toastify';

export const ProtectedRoutes = () => {

    const { getUser, setUser } = useContext(AuthContext);
    const accessToken = localStorage.getItem("@lnsf:accessToken") || '';
    const userName = localStorage.getItem('@lnsf:userName') || '';

    const navigate = useNavigate();
    
    useEffect(() => {
        if (accessToken) {
            getUser(userName)
                .then((response) => {
                    if (response instanceof Error) {
                        localStorage.clear();
                        toast.error(response.message);
                        navigate("/")
                    } else {
                        setUser(response);
                    }
                })
                .catch((error) => {
                    console.error('Detalhes do erro:', error);
                });
        }
    }, []);

    return (
        <>
            {
                accessToken ? (
                    <Outlet /> 
                ) : (
                    <Navigate to="/" />
                )
            }
        </>
    )
}
