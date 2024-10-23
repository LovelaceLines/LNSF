import { Avatar, Box, Paper, SxProps, Theme, Typography } from "@mui/material";

import { Loading } from "@/components";

export interface ContainerProps {
  title?: string;
  subtitle?: string;
  src?: string;
  children?: React.ReactNode;
  loading?: boolean;
  sxPaper?: SxProps<Theme>;
}

export const Container = ({ title, subtitle, src, children, loading, sxPaper }: ContainerProps) => {
  return (
    <Paper
      component={Box}
      display="flex"
      alignItems="center"
      justifyContent="center"
      width="100%"
      p={4}
      sx={{ borderRadius: "16px", ...sxPaper }}
    >
      {src && (
        <Avatar
          src={src}
          sx={{
            width: "96px",
            height: "auto",
            paddingRight: "28px",
          }}
        />
      )}
      {title && subtitle && (
        <Box>
          <Typography variant="h5">{title}</Typography>
          <Typography variant="subtitle1">{subtitle}</Typography>
        </Box>
      )}
      {loading ? <Loading height="auto" /> : children != undefined ? children : null}
    </Paper>
  );
};
