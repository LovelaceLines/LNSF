import { ThemeProvider } from "@mui/material";

import { DarkTheme } from "./darkTheme";

interface DarkThemeProviderProps {
  children: React.ReactNode;
}

export const DarkThemeProvider: React.FC<DarkThemeProviderProps> = ({ children }) => {
  return <ThemeProvider theme={DarkTheme}>{children}</ThemeProvider>;
};
