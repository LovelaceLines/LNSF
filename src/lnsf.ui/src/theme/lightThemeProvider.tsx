import { ThemeProvider } from "@mui/material";

import { LightTheme } from "./lightTheme";

interface LightThemeProviderProps {
  children: React.ReactNode;
}

export const LightThemeProvider: React.FC<LightThemeProviderProps> = ({ children }) => {
  return <ThemeProvider theme={LightTheme}>{children}</ThemeProvider>;
};
