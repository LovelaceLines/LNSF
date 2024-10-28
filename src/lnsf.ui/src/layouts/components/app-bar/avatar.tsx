import { Avatar as AvatarMUI, IconButton, Link } from "@mui/material";

import { colors, useThemeContext } from "@/theme";
import { useAuthStore } from "@/store/useAuthStore";

export const Avatar = () => {
	const { user } = useAuthStore();
	const { themeName } = useThemeContext();

	return (
		<Link href="/app/my-account" style={{ textDecoration: "none" }}>
			<IconButton color="inherit" sx={{ display: { xs: "none", sm: "flex" } }}>
				<AvatarMUI sx={{ width: 24, height: 24, bgcolor: `${themeName === "light" ? colors.black : colors.white}` }}>
					{user?.name?.charAt(0) || "A"}
				</AvatarMUI>
			</IconButton>
		</Link>
	);
};
