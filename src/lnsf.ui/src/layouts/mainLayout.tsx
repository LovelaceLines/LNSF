import { Box } from "@mui/material";
import {
	AccountCircle,
	Apartment,
	Bed,
	Home,
	LocalHospital,
	People,
	Quiz,
	Settings,
	Spa,
	Today,
	Tour,
} from "@mui/icons-material";

import { AppBar } from "./components/app-bar";
import { ISideBarProps, SideBar } from "./components/side-bar";
import { useSideBar } from "@/contexts";
import { useThemeContext } from "@/theme";
import { LayersLayout } from "./layersLayout";

const buttonList: ISideBarProps[][] = [
	[{ text: "Inicio", to: "/app", icon: <Home /> }],
	[
		{ text: "Reg. Diário", to: "/app/registro-diario", icon: <Tour /> },
		{ text: "Reg. Diário - Histor.", to: "/app/registro-diario/historico", icon: <Tour /> },
	],
	[
		{ text: "Pessoas", to: "/app/pessoas", icon: <People /> },
		{ text: "Reservas", to: "/app/reservas", icon: <Today /> },
		{ text: "Hospedagens", to: "/app/hospedagens", icon: <Apartment /> },
		{ text: "Apartamentos", to: "/app/apartamentos", icon: <Bed /> },
	],
	[
		{ text: "hospitais", to: "/app/hospitais", icon: <LocalHospital /> },
		{ text: "Tratamentos", to: "/app/tratamentos", icon: <Spa /> },
	],
	[{ text: "Usuários", to: "/app/usuarios", icon: <People /> }],
	[
		{ text: "Minha Conta", to: "/app/minha-conta", icon: <AccountCircle /> },
		{ text: "Logs", to: "/app/logs", icon: <Quiz /> },
		{ text: "Configurações", to: "/app/configuracoes", icon: <Settings /> },
	],
];

const drawerWidth = 240;
const minDrawerWidth = 56;

export const MainLayout = () => {
	const { open } = useSideBar();
	const { isDesktop } = useThemeContext();

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
