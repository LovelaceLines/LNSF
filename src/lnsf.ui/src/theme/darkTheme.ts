import { ThemeOptions, createTheme } from "@mui/material";

import { colors } from "./colors";
import { GlobalTheme } from "./globalTheme";

export const DarkTheme = createTheme(GlobalTheme, {
	palette: {
		mode: "dark",
		background: {
			paper: colors.eerieBlack,
			default: colors.night,
		},
		text: {
			primary: colors.white,
			secondary: colors.cadetGray,
			disabled: colors.raisinBlack,
		},
		action: {
			hover: "#111111",
			disabled: colors.raisinBlack,
		},
		divider: colors.jet,
	},
	typography: {
		h1: {
			color: colors.honeydew,
		},
		h2: {
			color: colors.honeydew,
		},
		h3: {
			color: colors.honeydew,
		},
		h4: {
			color: colors.honeydew,
		},
		h5: {
			color: colors.honeydew,
		},
		h6: {
			color: colors.honeydew,
		},
		subtitle1: {
			color: colors.honeydew,
		},
		subtitle2: {
			color: colors.honeydew,
		},
		body1: {
			color: colors.white,
		},
		body2: {
			color: colors.white,
		},
	},
} as ThemeOptions);
