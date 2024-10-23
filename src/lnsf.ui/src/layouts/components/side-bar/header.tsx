import { Avatar, Box, IconButton, Link } from "@mui/material";
import { MenuOpen } from "@mui/icons-material";
import logo from "/icons/lnsf.svg";
import logo_name from "/icons/lnsf-txt.svg";

import { useSideBar } from "@/contexts";
import { useThemeContext } from "@/theme";

export const Header = () => {
	const { isMobile } = useThemeContext();
	const { open, toggleSideBar } = useSideBar();

	return (
		<Box display="flex" flexDirection="row" alignItems="center" gap={1} height={64} p={2}>
			<Link
				href="/app"
				style={{
					display: "flex",
					flexDirection: "row",
					alignItems: "center",
					gap: 16,
				}}
			>
				<Avatar
					src={logo_name}
					alt="Logo"
					sx={{
						width: 72,
						height: "auto",
						display: isMobile || open ? "block" : "none",
						borderRadius: 0,
					}}
				/>
				{/* TODO - Fix - Logo cortada */}
				<Avatar src={logo} variant="square" sx={{ width: 32, height: 32, marginLeft: -0.5 }} />
			</Link>
			<IconButton color="inherit" onClick={() => toggleSideBar()} sx={{ ml: "auto" }}>
				<MenuOpen />
			</IconButton>
		</Box>
	);
};
