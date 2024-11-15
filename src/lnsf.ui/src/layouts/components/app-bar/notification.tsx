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
	Skeleton,
	Typography,
} from "@mui/material";
import { useState } from "react";

import { useNotificationStore } from "@/store";
import { dateOnlyToStr } from "@/utils";
import { useThemeContext } from "@/theme";

export const Notification = () => {
	const { count, notifications, getNotificationsByUser, postMarkAsRead } = useNotificationStore();
	const { isMobile } = useThemeContext();

	const [open, setOpen] = useState<boolean>(false);
	const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);

	const toggleOpen = (event: React.MouseEvent<HTMLButtonElement>) => {
		setAnchorEl(event.currentTarget);
		if (count > 0) {
			setOpen(!open);
			getNotificationsByUser();
		}
	};

	return (
		<>
			<IconButton color="inherit" onClick={toggleOpen}>
				<Badge badgeContent={count} color="primary">
					<Notifications />
				</Badge>
			</IconButton>
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
					<Box width={350} maxHeight={isMobile ? "60vh" : "80vh"} overflow="auto">
						{count > 0 && notifications.length === 0 && (
							<Card variant="outlined">
								<CardHeader
									title={
										<Skeleton
											animation="wave"
											height={30}
											width="100%"
											style={{ marginBottom: 6 }}
										/>
									}
									subheader={<Skeleton animation="wave" height={10} width="20%" />}
								/>
								<CardContent sx={{ py: 0 }}>
									<>
										<Skeleton animation="wave" height={15} style={{ marginBottom: 6 }} />
										<Skeleton animation="wave" height={15} style={{ marginBottom: 6 }} />
										<Skeleton animation="wave" height={15} style={{ marginBottom: 6 }} />
										<Skeleton animation="wave" height={15} width="80%" />
									</>
								</CardContent>
							</Card>
						)}
						{notifications.map((notification) => (
							<>
								<Card key={notification.id} variant="outlined">
									<CardHeader
										title={notification.title}
										titleTypographyProps={{ variant: "h6" }}
										subheader={dateOnlyToStr(notification.validFrom, "ptBr")}
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
		</>
	);
};
