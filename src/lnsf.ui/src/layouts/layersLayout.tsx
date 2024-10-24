import { Container } from "@mui/material";
import { Outlet } from "react-router-dom";

export const LayersLayout = () => {
	return (
		<Container disableGutters maxWidth={false} sx={{ py: 1 }}>
			<Container disableGutters maxWidth="xl" sx={{ px: { xs: 1, sm: 2, xl: 0 }, py: 2 }}>
				<Outlet />
			</Container>
		</Container>
	);
};
