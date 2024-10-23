import { createContext, useContext } from "react";
import { CssBaseline, GlobalStyles, ThemeProvider as ThemeProviderMUI, useMediaQuery } from "@mui/material";

import { DarkTheme } from "./darkTheme";
import { LightTheme } from "./lightTheme";
import { useLocalStorage } from "../hooks";
import { scrollbarStyles } from "./scrollbarStyles";

interface IThemeContextProps {
	themeName: "light" | "dark";
	toggleTheme: () => void;
	isMobile: boolean;
	isTablet: boolean;
	isDesktop: boolean;
	isWide: boolean;
	isUltraWide: boolean;
}

export const ThemeContext = createContext({} as IThemeContextProps);

export const ThemeProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
	const isLightModePreferred = useMediaQuery("(prefers-color-scheme: light)");
	const [themeName, setThemeName] = useLocalStorage("theme", isLightModePreferred ? "light" : "dark");

	const toggleTheme = () => setThemeName(themeName === "light" ? "dark" : "light");

	const theme = themeName === "light" ? LightTheme : DarkTheme;

	const isMobile = useMediaQuery(theme.breakpoints.down("sm")); // 0px - 600px
	const isTablet = useMediaQuery(theme.breakpoints.between("sm", "md")); // 600px - 900px
	const isDesktop = useMediaQuery(theme.breakpoints.up("md")); // + 900px
	const isWide = useMediaQuery(theme.breakpoints.between("lg", "xl")); // 1200px - 1536px
	const isUltraWide = useMediaQuery(theme.breakpoints.up("xl")); // + 1536px

	return (
		<ThemeProviderMUI theme={theme}>
			<CssBaseline enableColorScheme />
			<GlobalStyles styles={{ ...scrollbarStyles }} />
			<ThemeContext.Provider
				value={{
					themeName,
					toggleTheme,
					isMobile,
					isTablet,
					isDesktop,
					isWide,
					isUltraWide,
				}}
			>
				{children}
			</ThemeContext.Provider>
		</ThemeProviderMUI>
	);
};

export const useThemeContext = () => {
	const { ...props } = useContext(ThemeContext);
	return { ...props };
};
