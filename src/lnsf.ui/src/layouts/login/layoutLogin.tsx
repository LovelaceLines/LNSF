import { Avatar, Box, Grid2 as Grid, Link, Paper, SxProps, Theme, Typography } from "@mui/material";
import { Outlet } from "react-router-dom";

import env from "@/env";
import { LightThemeProvider } from "@/theme";

const backgroundStyle: SxProps<Theme> = {
  backgroundImage: "url(/backgrounds/login.jpg)",
  backgroundRepeat: "no-repeat",
  backgroundSize: "cover",
  backgroundPosition: "center",
};

const Logo = () => (
  <Box display="flex" flexDirection="row" alignItems="center" gap={2}>
    <Avatar src="logos/lnsf.svg" variant="square" sx={{ width: 256, height: "auto" }} />
  </Box>
);

const Copyright = () => (
  <Typography variant="body2" color="text.secondary" align="center">
    {"Copyright © "}
    <Link color="inherit" href={env.APP_URL}>
      LNSF
    </Link>{" "}
    {new Date().getFullYear()}
    {"."}
  </Typography>
);

export const LoginLayout = () => {
  return (
    <LightThemeProvider>
      <Grid container component="main">
        <Grid size={{ xs: 0, sm: 4, md: 7 }} sx={backgroundStyle} />
        <Grid size={{ xs: 12, sm: 8, md: 5 }} component={Paper} square>
          <Box
            display="flex"
            flexDirection="column"
            justifyContent="center"
            alignItems="center"
            gap={2}
            px={4}
            height="100vh"
          >
            <Logo />
            <Outlet />
            <Copyright />
          </Box>
        </Grid>
      </Grid>
    </LightThemeProvider>
  );
};
