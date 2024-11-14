import { ArrowBackIos } from "@mui/icons-material";
import { Box, Button, Container, Typography } from "@mui/material";

export const NotFoundPage = () => {
	return (
		<Container
			maxWidth="sm"
			sx={{ my: 4, display: "flex", flexDirection: "column", alignItems: "center" }}
		>
			<Box display="flex" textAlign="center">
				<img
					src="https://c.tenor.com/ftaDAT-sWg4AAAAC/tenor.gif"
					width={1080}
					height={1080}
					style={{ width: "100%", height: "auto" }}
				/>
			</Box>
			<Box>
				<Typography variant="h1" align="center">
					404
				</Typography>
				<Typography variant="body1" align="center">
					Página não encontrada.
				</Typography>
			</Box>
			<Button
				variant="outlined"
				href="javascript:history.back()"
				startIcon={<ArrowBackIos />}
				sx={{ my: 2 }}
			>
				Voltar para a página anterior
			</Button>
		</Container>
	);
};
