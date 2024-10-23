import { Container } from "@mui/material";
import { Outlet } from "react-router-dom";

export const LayersLayout = () => {
	return (
		<Container disableGutters maxWidth={false} sx={{ py: 1 }}>
			<Container maxWidth="xl" sx={{ py: { xs: 0, md: 2 } }}>
				<Outlet />
			</Container>
		</Container>
	);
};
