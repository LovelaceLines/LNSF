import { Button, Divider, Grid2 as Grid, TextField } from "@mui/material";

import { useTreatmentFormPage } from "./useTreatmentFormPage";
import { getTypeTreatment } from "@/types";
import { SelectField } from "@/components";

export const TreatmentFormPage = () => {
	const { id, register, handleSubmit, errors, getValues, handleSave, setValue } = useTreatmentFormPage();

	return (
		<Grid container spacing={2} component="form" onSubmit={handleSubmit(handleSave)}>
			<Grid size={{ xs: 12 }}>
				<Divider>Dados do Tratamento</Divider>
			</Grid>
			<Grid size={{ xs: 4, sm: 1.5 }}>
				<TextField label="Id" disabled={!id} {...register("id")} error={!!errors.id} helperText={errors.id?.message} fullWidth />
			</Grid>
			<Grid size={{ xs: 8, sm: 7.5, md: 8.5 }}>
				<TextField label="Nome" {...register("name")} error={!!errors.name} helperText={errors.name?.message} fullWidth />
			</Grid>
			<Grid size={{ xs: 6, sm: 3, md: 2 }}>
				<SelectField
					label="Tipo"
					labelId="id"
					labelKey="value"
					options={getTypeTreatment()}
					valueKey="id"
					defaultValue={id && `${getValues("type")}`}
					onClick={(value) => setValue("type", Number(value))}
				/>
			</Grid>
			<Grid size={{ xs: 12 }}>
				<Button type="submit" variant="contained" color="primary" fullWidth>
					Salvar
				</Button>
			</Grid>
		</Grid>
	);
};
