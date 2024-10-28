import { Divider, List, ListItem, ListItemButton, ListItemIcon, ListItemText, Tooltip } from "@mui/material";
import { Logout } from "@mui/icons-material";
import React from "react";
import { Link } from "react-router-dom";

import { useSideBar } from "@/contexts";
import { useAuthStore } from "@/store/useAuthStore";

export interface ISideBarProps {
	text: string;
	to: string;
	icon: React.ReactElement;
	allowRoles?: string[];
}

const logoutButton: ISideBarProps = { text: "Sair", to: "/login", icon: <Logout /> };

export const ButtonList = ({ buttonList }: Readonly<{ buttonList: ISideBarProps[][] }>) => {
	const { logoutUser } = useAuthStore();
	const { open } = useSideBar();

	return (
		<List>
			{buttonList.map((subList, index) => (
				<React.Fragment key={index}>
					{subList.map(({ text, to, icon }, index) => (
						<ListItem key={text + index} disablePadding>
							<Link to={to} style={{ width: "100%", color: "inherit", textDecoration: "none" }}>
								<ListItemButton>
									<Tooltip title={text} placement="right" arrow disableHoverListener={open}>
										<ListItemIcon sx={{ color: "inherit" }}>{icon}</ListItemIcon>
									</Tooltip>
									<ListItemText primary={text} sx={{ opacity: 1, textWrap: "nowrap" }} />
								</ListItemButton>
							</Link>
						</ListItem>
					))}
					<Divider key={"Divider" + index} />
				</React.Fragment>
			))}

			<ListItem key={logoutButton.text} disablePadding sx={{ display: "block" }}>
				<ListItemButton onClick={() => logoutUser()}>
					<Tooltip title={logoutButton.text} placement="right" disableHoverListener={open} arrow>
						<ListItemIcon sx={{ color: "inherit" }}>{logoutButton.icon}</ListItemIcon>
					</Tooltip>
					<ListItemText primary={logoutButton.text} sx={{ opacity: 1 }} />
				</ListItemButton>
			</ListItem>
		</List>
	);
};
