import { Close } from "@mui/icons-material";
import { Box, IconButton, Modal as ModalMUI } from "@mui/material";
import { ReactElement } from "react";

import { colors, useThemeContext } from "@/theme";
import { ModalKey, useModal } from "@/contexts";

export interface ModalProps {
	id: ModalKey;
	children: ReactElement;
}

export const Modal = ({ id, children }: ModalProps) => {
	const { handleModalClose, isOpen } = useModal(id);
	const { themeName } = useThemeContext();

	return (
		<ModalMUI open={isOpen} onClose={handleModalClose} closeAfterTransition>
			<Box
				display="flex"
				flexDirection="column"
				gap={1}
				position="absolute"
				top="50%"
				left="50%"
				bgcolor="background.paper"
				border="2px solid"
				borderColor={themeName === "light" ? colors.black : colors.white}
				boxShadow={24}
				p={4}
				sx={{
					transform: "translate(-50%, -50%)",
				}}
			>
				<IconButton onClick={handleModalClose} sx={{ position: "absolute", top: 0, right: 0 }}>
					<Close sx={{ color: themeName === "light" ? colors.black : colors.white }} />
				</IconButton>
				{children}
			</Box>
		</ModalMUI>
	);
};
