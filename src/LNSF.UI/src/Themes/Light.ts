import { createTheme } from "@mui/material";
import { grey, lightBlue, yellow } from "@mui/material/colors";

export const LightTheme = createTheme({
    palette: {
        primary:{
            main: lightBlue[500],
            dark: lightBlue[600],
            light: lightBlue[300],
            contrastText: lightBlue[50],
        },
        secondary:{
            main: yellow[400],
            dark: yellow[500],
            light: yellow[300],
            contrastText: yellow[50],
        },
        background: {
            paper: grey[50],
            default: grey[200],
        }
    },
    typography: {
        allVariants:{
            color: 'black',
        },
        subtitle1: {
            lineHeight:'normal'
        },
    },
});