import { OpenInFull } from "@mui/icons-material";
import { Container, IconButton } from "@mui/material";

import { Modal } from "@/components";
import { useModal } from "@/contexts";

export const ValuesChangesModal = ({ id, valuesChanges }: { id: number; valuesChanges: string }) => {
	const { handleModalOpen } = useModal(String(id));

	return (
		<>
			<IconButton size="small" sx={{ height: 18 }} onClick={handleModalOpen}>
				<OpenInFull />
			</IconButton>
			{valuesChanges}
			<Modal id={String(id)}>
				<Container disableGutters maxWidth="md">
					<pre>{valuesChanges}</pre>
				</Container>
			</Modal>
		</>
	);
};
