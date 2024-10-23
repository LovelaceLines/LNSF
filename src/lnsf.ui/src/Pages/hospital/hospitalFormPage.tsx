import { Button, Divider, Grid2 as Grid, TextField } from "@mui/material";

import { useHospitalFormPage } from "./useHospitalFormPage";

export const HospitalFormPage = () => {
	const { id, register, handleSubmit, errors, handleSave } = useHospitalFormPage();

	return (
		<Grid container spacing={2} component="form" onSubmit={handleSubmit(handleSave)}>
			<Grid size={{ xs: 12 }}>
				<Divider>Dados do Hospital</Divider>
			</Grid>
			<Grid size={{ xs: 4, sm: 1.5 }}>
				<TextField label="Id" disabled={!id} {...register("id")} error={!!errors.id} helperText={errors.id?.message} fullWidth />
			</Grid>
			<Grid size={{ xs: 8, sm: 7.5, md: 8.5 }}>
				<TextField label="Nome" {...register("name")} error={!!errors.name} helperText={errors.name?.message} fullWidth />
			</Grid>
			<Grid size={{ xs: 6, sm: 3, md: 2 }}>
				<TextField label="Sigla" {...register("acronym")} error={!!errors.acronym} helperText={errors.acronym?.message} fullWidth />
			</Grid>
			<Grid size={{ xs: 12 }}>
				<Button type="submit" variant="contained" color="primary" fullWidth>
					Salvar
				</Button>
			</Grid>
		</Grid>
	);
};
