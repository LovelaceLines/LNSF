import { ThemeOptions, createTheme } from "@mui/material";
import { grey } from "@mui/material/colors";

import { colors } from "./colors";
import { GlobalTheme } from "./globalTheme";

export const LightTheme = createTheme(GlobalTheme, {
  palette: {
    mode: "light",
    background: {
      paper: colors.white,
      default: grey[200],
    },
    text: {
      primary: colors.black,
      secondary: grey[800],
      disabled: grey[500],
    },
    action: {
      hover: grey[300],
      disabled: grey[500],
    },
    divider: colors.cadetGray,
  },
  typography: {
    h1: {
      color: colors.black,
    },
    h2: {
      color: colors.black,
    },
    h3: {
      color: colors.black,
    },
    h4: {
      color: colors.black,
    },
    h5: {
      color: colors.black,
    },
    h6: {
      color: colors.black,
    },
    subtitle1: {
      color: colors.black,
    },
    subtitle2: {
      color: colors.black,
    },
    body1: {
      color: colors.raisinBlack,
    },
    body2: {
      color: colors.raisinBlack,
    },
  },
} as ThemeOptions);
