import { createContext, useContext, useEffect, useState } from "react";
import { iRoleTypes } from "./type";
import { AuthContext } from "../authcontext/AuthContext_";

export const RoleContext = createContext<iRoleTypes>({} as iRoleTypes);

export const RoleProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [Roles, setRole] = useState<iRoleTypes>({
    isDesenvolvedor: false,
    isAdministrador: false,
    isAssistenteSocial: false,
    isSecretario: false,
    isVoluntario: false,
  });

  const { isAuthenticated, getUser } = useContext(AuthContext);

  useEffect(() => {
    const fetchUserRoles = async () => {
      if (!isAuthenticated) return;

      const user = await getUser();

      setRole(() => ({
        isDesenvolvedor: user.roles.includes("Desenvolvedor"),
        isAdministrador: user.roles.includes("Administrador"),
        isAssistenteSocial: user.roles.includes("Assistente Social"),
        isSecretario: user.roles.includes("Secretário"),
        isVoluntario: user.roles.includes("Voluntário"),
      }));
    };

    fetchUserRoles();
  }, [isAuthenticated]);
  
  return (
    <RoleContext.Provider value={Roles}>
      {children}
    </RoleContext.Provider>
  );
};
