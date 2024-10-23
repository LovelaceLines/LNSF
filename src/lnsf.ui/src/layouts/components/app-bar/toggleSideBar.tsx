import { Menu } from "@mui/icons-material";
import { IconButton } from "@mui/material";

import { useSideBar } from "@/contexts";

export const ToggleSideBar = () => {
  const { open, toggleSideBar } = useSideBar();

  return (
    <IconButton color="inherit" onClick={() => toggleSideBar()} sx={{ display: { xs: "flex", sm: open ? "none" : "flex" } }}>
      <Menu />
    </IconButton>
  );
};
