// import { createContext, useCallback, useContext, useEffect, useMemo, useState } from "react";
// //import { AthServer } from "../services/api/auth/AuthServer";

// interface IAuthProviderProps{
//     children: React.ReactNode;
// }

// interface IAuthContextData{
//     isAuthenticated: boolean;
//     login: (email: string, password: string) => Promise<string | void>;
//     logout: () => void;
// }

// const AuthContext = createContext({} as IAuthContextData);
// export const useAuthContext = () => useContext(AuthContext);


// export const AuthProvider: React.FC<IAuthProviderProps> = ({children}) => {
//     const [accessToken, setAcessToken] = useState<string>()

//     useEffect(() => {
//         const accessToken = localStorage.getItem('APP_ACCESS_TOKEN')
        
//         if(accessToken){
//             setAcessToken( JSON.stringify(accessToken));
//         }else{
//             setAcessToken(undefined);
//         }
//     }, [])

    // const handleLogin = useCallback(async (email: string, password: string) => {
    //     const result = await AthServer.auth(email, password);
       
    //     if(result instanceof Error){
    //         return result.message;
    //     }else{
    //         console.log(result)
    //         localStorage.setItem('APP_ACCESS_TOKEN', JSON.stringify(result.AccessToken));
    //         setAcessToken(result.AccessToken)
    //     }

    // }, []);

//     const handleLogout = useCallback(() => {
//         localStorage.removeItem('APP_ACCESS_TOKEN');
//         setAcessToken(undefined);
//     }, []);

//     const isAuthenticated = useMemo(() => accessToken !== undefined, [accessToken]);
//                                             //  !!acessToken                            
//     return(
//         <AuthContext.Provider value={{isAuthenticated, login: handleLogin, logout: handleLogout}}>
//             {children}
//         </AuthContext.Provider>
//     )
// }