import { ThemeOptions, createTheme } from "@mui/material";
import { lightBlue } from "@mui/material/colors";

export const GlobalTheme = createTheme({
  palette: {
    primary: {
      main: lightBlue[500],
      contrastText: lightBlue[50],
    },
  },
} as ThemeOptions);
