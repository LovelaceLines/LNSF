import { peopleRoomHosting } from "@/types";
import { usePeopleRoomHostingFormPage } from "./usePeopleRoomHostingFormPage";
import { Grid2 as Grid, IconButton, TextField } from "@mui/material";
import { Add, Delete } from "@mui/icons-material";

export const PeopleRoomHostingFormPage = ({ prh }: { prh: peopleRoomHosting }) => {
	const { addPeopleToRoom, removePeopleFromRoom, register, getValues } = usePeopleRoomHostingFormPage({ prh });

	return (
		<Grid container spacing={2} alignItems="center">
			<Grid size={{ xs: 4, sm: 1.5, md: 1 }}>
				<TextField label="Id Pessoa" {...register("peopleId")} fullWidth />
			</Grid>
			<Grid size={{ xs: 8, sm: 8.5, md: 4 }}>
				<TextField label="Nome" {...register("people.name")} fullWidth />
			</Grid>
			<Grid size={{ xs: 4, sm: 1.5, md: 1 }}>
				<TextField label="Id Apartamento" {...register("roomId")} fullWidth />
			</Grid>
			<Grid size={{ xs: 8, sm: 1.5, md: 1 }}>
				<TextField label="Número Apartamento" {...register("room.number")} fullWidth />
			</Grid>
			<Grid size={{ xs: 4, sm: 1.5, md: 1 }}>
				<TextField label="Id Hospedagem" {...register("hostingId")} fullWidth />
			</Grid>
			<Grid>
				<IconButton
					type="submit"
					color="primary"
					size="large"
					onClick={() => addPeopleToRoom(getValues("peopleId"), getValues("roomId"), getValues("hostingId"))}
				>
					<Add />
				</IconButton>
				<IconButton
					color="primary"
					size="large"
					onClick={() => removePeopleFromRoom(getValues("peopleId"), getValues("roomId"), getValues("hostingId"))}
				>
					<Delete />
				</IconButton>
			</Grid>
		</Grid>
	);
};
