import { createContext, useContext, useEffect, useState } from "react";
import { iRoleTypes, iRoleProvider } from "./type";
import { AuthContext } from "../authcontext/AuthContext_";

export const RoleContext = createContext<iRoleTypes>({} as iRoleTypes);

export const RoleProvider = ({ children }: iRoleProvider) => {
  const [roles, setRoles] = useState<iRoleTypes>({
    isDesenvolvedor: false,
    isAdministrador: false,
    isAssistenteSocial: false,
    isSecretario: false,
    isVoluntario: false,
  });

  const { getUser } = useContext(AuthContext);

  useEffect(() => {
    const fetchUserRoles = async () => {
      const user = await getUser();

      setRoles(() => ({
        isDesenvolvedor: user.roles.includes("Desenvolvedor"),
        isAdministrador: user.roles.includes("Administrador"),
        isAssistenteSocial: user.roles.includes("Assistente Social"),
        isSecretario: user.roles.includes("Secretário"),
        isVoluntario: user.roles.includes("Voluntário"),
      }));
    };

    fetchUserRoles();
  }, []);
  /*
  return (
    <RoleContext.Provider value={roles}>
      {children}
    </RoleContext.Provider>
  );
  */
};
