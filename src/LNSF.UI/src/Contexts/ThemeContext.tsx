import { createContext, useCallback, useMemo, useState, useContext } from "react";
import { ThemeProvider } from "@emotion/react";
import { LightTheme, DarkTheme } from "../Themes";
import { Box } from "@mui/material";


interface IAppThemeProviderProps {
    children: React.ReactNode;
}

interface IthemeContextData {
    themeName: 'light' | 'dark';
    toggleTheme: () => void;
}

export const ThemeContext = createContext({} as IthemeContextData);

export const useAppThemeContext = () => {
    return useContext(ThemeContext);
}

export const AppThemeProvider: React.FC<IAppThemeProviderProps> = ({ children }) => {

    const [themeName, setThemeName] = useState<'light' | 'dark'>('light');

    const toggleTheme = useCallback(() => {
        setThemeName(oldThemeName => oldThemeName === 'light' ? 'dark' : 'light');
    }, []);

    const theme = useMemo(() => {
        if (themeName === 'light') return LightTheme;
        return DarkTheme;
    }, [themeName]);

    return (
        <ThemeContext.Provider value={{ themeName, toggleTheme }}>
            <ThemeProvider theme={theme}>
                <Box width="100vw" height="100vh" bgcolor={theme.palette.background.default}>
                    {children}
                </Box>

            </ThemeProvider>
        </ThemeContext.Provider>
    );
};