import { SwipeableDrawer } from "@mui/material";

import { ButtonList, ISideBarProps } from "./buttomList";
import { useSideBar } from "@/contexts";
import { colors, useThemeContext } from "@/theme";
import { Header } from "./header";

export interface SideBarProps {
  buttonList: ISideBarProps[][];
  drawerWidth?: number;
  minDrawerWidth?: number;
}

export const SideBar = ({ buttonList, drawerWidth = 240, minDrawerWidth = 56 }: SideBarProps) => {
  const { isMobile, themeName } = useThemeContext();
  const { open, toggleSideBar } = useSideBar();

  return (
    <SwipeableDrawer
      variant={isMobile ? "temporary" : "permanent"}
      open={isMobile ? open : true}
      onOpen={() => toggleSideBar()}
      onClose={() => toggleSideBar()}
      sx={{
        "& .MuiDrawer-paper": {
          width: open ? drawerWidth : minDrawerWidth,
          borderRight: "none",
          backgroundImage: "none",
          overflowX: "hidden",
          overflowY: "scroll",
          bgcolor: `${themeName === "light" ? colors.white : colors.black}`,
        },
      }}
    >
      <Header />
      <ButtonList buttonList={buttonList} />
    </SwipeableDrawer>
  );
};
