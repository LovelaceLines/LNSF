import { Box, CircularProgress, Container, SxProps, Theme } from "@mui/material";

export interface LoadingProps {
  height?: "100vh" | "auto";
  sx?: SxProps<Theme> | undefined;
}

export const Loading = ({ height = "100vh", sx }: LoadingProps) => {
  return (
    <Container>
      <Box display="flex" justifyContent="center" alignItems="center" height={height} sx={sx}>
        <CircularProgress />
      </Box>
    </Container>
  );
};
