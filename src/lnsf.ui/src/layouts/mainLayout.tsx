import {
  AccountCircle,
  Apartment,
  Bed,
  EditNotifications,
  Home,
  LocalHospital,
  People,
  Quiz,
  Settings,
  Spa,
  Today,
  Tour,
} from "@mui/icons-material";
import { Box } from "@mui/material";
import { useEffect } from "react";

import { AppBar } from "./components/app-bar";
import { ISideBarProps, SideBar } from "./components/side-bar";
import { useSideBar } from "@/contexts";
import { useThemeContext } from "@/theme";
import { LayersLayout } from "./layersLayout";
import { isInRoles, useNotificationStore } from "@/store";

const buttonList: ISideBarProps[][] = [
  [{ text: "Inicio", to: "/app", icon: <Home />, display: !isInRoles(["Voluntário"]) }],
  [
    { text: "Reg. Diário", to: "/app/registro-diario", icon: <Tour />, display: true },
    {
      text: "Reg. Diário - Histor.",
      to: "/app/registro-diario/historico",
      icon: <Tour />,
      display: true,
    },
  ],
  [
    { text: "Pessoas", to: "/app/pessoas", icon: <People />, display: !isInRoles(["Voluntário"]) },
    { text: "Reservas", to: "/app/reservas", icon: <Today />, display: !isInRoles(["Voluntário"]) },
    {
      text: "Hospedagens",
      to: "/app/hospedagens",
      icon: <Apartment />,

      display: true,
    },
    { text: "Apartamentos", to: "/app/apartamentos", icon: <Bed />, display: !isInRoles(["Voluntário"]) },
  ],
  [
    {
      text: "hospitais",
      to: "/app/hospitais",
      icon: <LocalHospital />,
      display: !isInRoles(["Voluntário"]),
    },
    { text: "Tratamentos", to: "/app/tratamentos", icon: <Spa />, display: !isInRoles(["Voluntário"]) },
  ],
  [{ text: "Usuários", to: "/app/usuarios", icon: <People />, display: !isInRoles(["Voluntário"]) }],
  [
    { text: "Minha Conta", to: "/app/minha-conta", icon: <AccountCircle />, display: true },
    {
      text: "Logs",
      to: "/app/logs",
      icon: <Quiz />,
      display: isInRoles(["Desenvolvedor", "Administrador"]),
    },
    {
      text: "Notificações",
      to: "/app/notificacoes",
      icon: <EditNotifications />,
      display: isInRoles(["Desenvolvedor", "Administrador"]),
    },
    { text: "Configurações", to: "/app/configuracoes", icon: <Settings />, display: true },
  ],
];

const drawerWidth = 240;
const minDrawerWidth = 56;

export const MainLayout = () => {
  const { open } = useSideBar();
  const { isDesktop } = useThemeContext();
  const { getUnreadCount } = useNotificationStore();

  useEffect(() => {
    getUnreadCount();
  }, []);

  return (
    <Box display="flex">
      <SideBar buttonList={buttonList} drawerWidth={drawerWidth} minDrawerWidth={minDrawerWidth} />
      <Box
        display="flex"
        flexDirection="column"
        sx={{
          height: isDesktop ? "100vh" : "auto",
          width: { xs: "100%", sm: `calc(100% - ${open ? drawerWidth : minDrawerWidth}px)` },
          ml: { sm: `${open ? drawerWidth : minDrawerWidth}px` },
        }}
      >
        <AppBar />
        <LayersLayout />
      </Box>
    </Box>
  );
};
