import { Box, CircularProgress, Container } from "@mui/material";

export interface LoadingProps {
  height?: "100vh" | "auto";
}

export const Loading = ({ height = "100vh" }: LoadingProps) => {
  return (
    <Container>
      <Box display="flex" justifyContent="center" alignItems="center" height={height}>
        <CircularProgress />
      </Box>
    </Container>
  );
};
