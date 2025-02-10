import { Box, Container, Typography } from "@mui/material";

export const UnauthorizedPage = () => {
  return (
    <Container maxWidth="sm">
      <Box display="flex" textAlign="center">
        <img
          src="https://i.giphy.com/wr7oA0rSjnWuiLJOY5.webp"
          width={1080}
          height={1080}
          style={{ width: "100%", height: "auto" }}
        />
      </Box>
      <Box>
        <Typography variant="h1" align="center">
          403
        </Typography>
        <Typography variant="body1" align="center">
          Você não tem permissão para acessar esta página.
        </Typography>
      </Box>
    </Container>
  );
};
