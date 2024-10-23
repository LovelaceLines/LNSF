import { Grid2 as Grid, IconButton, TextField } from "@mui/material";

import { useEmergencyContactFormPage } from "./useEmergencyContactFormPage";
import { emergencyContact } from "@/types";
import { Add, Delete, Edit } from "@mui/icons-material";

export const EmergencyContactFormPage = ({ emergencyContact }: { emergencyContact?: emergencyContact }) => {
	const { errors, getValues, handleDelete, handleSave, handleSubmit, register } = useEmergencyContactFormPage({ emergencyContact });

	return (
		<Grid container spacing={2} alignItems="center" component="form" onSubmit={handleSubmit(handleSave)}>
			<Grid size={{ xs: 12, sm: 1.5, md: 1 }}>
				<TextField
					label="Id"
					disabled={!getValues("id")}
					{...register("id")}
					error={!!errors.id}
					helperText={errors.id?.message}
					fullWidth
				/>
			</Grid>
			<Grid size={{ xs: 12, sm: 4.5, md: 6 }}>
				<TextField label="Nome" {...register("name")} error={!!errors.name} helperText={errors.name?.message} fullWidth />
			</Grid>
			<Grid size={{ xs: 12, sm: 4 }}>
				<TextField label="Telefone" {...register("phone")} error={!!errors.phone} helperText={errors.phone?.message} fullWidth />
			</Grid>
			<Grid direction="row" spacing={2} size={{ xs: 12, sm: 2, md: 1 }}>
				<IconButton type="submit" color="primary" size="large">
					{getValues("id") ? <Edit /> : <Add />}
				</IconButton>
				{getValues("id") && (
					<IconButton color="primary" size="large" onClick={() => handleDelete(getValues("id")!)}>
						<Delete />
					</IconButton>
				)}
			</Grid>
		</Grid>
	);
};
