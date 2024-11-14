import { Delete, Notifications } from "@mui/icons-material";
import {
	Badge,
	Box,
	Card,
	CardContent,
	CardHeader,
	Container,
	Divider,
	IconButton,
	Popover,
	Typography,
} from "@mui/material";
import { useNotificationStore } from "@/store";
import { useEffect, useState } from "react";
import { dateOnlyToStr } from "@/utils";
import { Loading } from "@/components";
import { useThemeContext } from "@/theme";

export const Notification = () => {
	const { count, notifications, getNotifications, getUnreadCount, postMarkAsRead } = useNotificationStore();
	const { isMobile } = useThemeContext();

	useEffect(() => {
		getUnreadCount();
		getNotifications();
	}, []);

	const [open, setOpen] = useState<boolean>(false);
	const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);

	const toggleOpen = (event: React.MouseEvent<HTMLButtonElement>) => {
		setOpen(!open && notifications.length > 0);
		setAnchorEl(event.currentTarget);
	};

	return (
		<IconButton color="inherit" onClick={toggleOpen}>
			<Badge badgeContent={count} color="primary">
				<Notifications />
			</Badge>
			<Popover
				open={open}
				anchorEl={anchorEl}
				onClose={toggleOpen}
				anchorOrigin={{
					vertical: "bottom",
					horizontal: "right",
				}}
				transformOrigin={{
					vertical: "top",
					horizontal: "right",
				}}
			>
				<Container disableGutters maxWidth="xs">
					<Box maxHeight={isMobile ? "60vh" : "80vh"} overflow="auto">
						<Loading height="auto" sx={{ m: "auto" }} />
						{notifications.map((notification) => (
							<>
								<Card key={notification.id}>
									<CardHeader
										title={notification.title}
										titleTypographyProps={{ variant: "h6" }}
										subheader={dateOnlyToStr(notification.createdAt, "ptBr")}
										subheaderTypographyProps={{ variant: "caption" }}
										action={
											<IconButton
												size="small"
												color="error"
												onClick={() => postMarkAsRead(notification.id!)}
											>
												<Delete />
											</IconButton>
										}
									/>
									<CardContent sx={{ py: 0 }}>
										<Typography variant="body2">{notification.content}</Typography>
									</CardContent>
								</Card>
								<Divider />
							</>
						))}
					</Box>
				</Container>
			</Popover>
		</IconButton>
	);
};
