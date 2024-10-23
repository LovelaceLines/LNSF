import { Avatar as AvatarMUI, IconButton, Link } from "@mui/material";
import { useSelector } from "react-redux";

import { selectUser } from "@/redux/features/auth/slice";
import { colors, useThemeContext } from "@/theme";

export const Avatar = () => {
  const user = useSelector(selectUser);
  const { themeName } = useThemeContext();

  return (
    <Link href="/app/my-account" style={{ textDecoration: "none" }}>
      <IconButton color="inherit" sx={{ display: { xs: "none", sm: "flex" } }}>
        <AvatarMUI sx={{ width: 24, height: 24, bgcolor: `${themeName === "light" ? colors.black : colors.white}` }}>{user?.name?.charAt(0) || "A"}</AvatarMUI>
      </IconButton>
    </Link>
  );
};
