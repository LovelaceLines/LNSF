import { Button, Divider, Grid2 as Grid, TextField } from "@mui/material";

import { SelectField } from "@/components";
import { useEscortFormPage } from "./useEscortFormPage";

export const EscortFormPage = () => {
	const { id, peoples, register, handleSubmit, errors, getValues, setValue, handleSave } = useEscortFormPage();

	return (
		<Grid container spacing={2} component="form" onSubmit={handleSubmit(handleSave)}>
			<Grid size={{ xs: 12 }}>
				<Divider>Dados do Acompanhante</Divider>
			</Grid>
			<Grid size={{ xs: 12, sm: 1.5, md: 1 }}>
				<TextField label="Id" disabled={!id} {...register("id")} error={!!errors.id} helperText={errors.id?.message} fullWidth />
			</Grid>
			<Grid size={{ xs: 12, sm: 2, md: 1.5 }}>
				<TextField
					label="Id Pessoa"
					{...register("peopleId")}
					error={!!errors.peopleId}
					helperText={errors.peopleId?.message}
					fullWidth
				/>
			</Grid>
			<Grid size={{ xs: 12, sm: 3, lg: 4 }}>
				<SelectField
					label="Pessoa"
					labelId="id"
					labelKey="name"
					options={peoples}
					valueKey="id"
					defaultValue={String(getValues("peopleId"))}
					onClick={(value) => setValue("peopleId", value)}
				/>
			</Grid>
			<Grid size={{ xs: 12 }}>
				<Button type="submit" variant="contained" fullWidth>
					{id ? "Atualizar" : "Cadastrar"}
				</Button>
			</Grid>
		</Grid>
	);
};
