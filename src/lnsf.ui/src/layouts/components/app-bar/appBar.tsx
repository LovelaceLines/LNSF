import { AppBar as AppBarMUI, Container, Grid2 as Grid, Toolbar } from "@mui/material";

import { Avatar } from "./avatar";
import { colors, useThemeContext } from "@/theme";
import { Notification } from "./notification";
import { ToggleSideBar } from "./toggleSideBar";

export const AppBar = () => {
  const { themeName } = useThemeContext();

  return (
    <AppBarMUI
      position="sticky"
      color="transparent"
      elevation={0}
      sx={{ bgcolor: `${themeName === "light" ? colors.white : colors.black}` }}
    >
      <Toolbar>
        <Container disableGutters maxWidth="xl">
          <Grid container display="flex">
            <Grid display="flex" size={{ xs: 9, sm: 5 }}>
              <ToggleSideBar />
            </Grid>
            <Grid size={{ xs: 2, sm: 4 }} />
            <Grid
              display="flex"
              justifyContent="flex-end"
              alignContent="center"
              size={{ xs: 1, sm: 3 }}
              gap={1}
            >
              <Notification />
              <Avatar />
            </Grid>
          </Grid>
        </Container>
      </Toolbar>
    </AppBarMUI>
  );
};
