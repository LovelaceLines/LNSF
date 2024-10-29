import { Button, Divider, Grid2 as Grid, TextField } from "@mui/material";

import { useTreatmentFormPage } from "./useTreatmentFormPage";
import { getTypeTreatment } from "@/types";
import { SelectField } from "@/components";
import { Controller } from "react-hook-form";

export const TreatmentFormPage = () => {
	const { handleSave, control, errors, handleSubmit, setValue, register, watch } = useTreatmentFormPage();

	return (
		<Grid container spacing={2} component="form" onSubmit={handleSubmit(handleSave)}>
			<Grid size={{ xs: 12 }}>
				<Divider>Dados do Tratamento</Divider>
			</Grid>
			<Grid size={{ xs: 4, sm: 1.5 }}>
				<TextField
					label="Id"
					disabled
					{...register("id")}
					error={!!errors.id}
					helperText={errors.id?.message}
					fullWidth
				/>
			</Grid>
			<Grid size={{ xs: 8, sm: 7.5, md: 8.5 }}>
				<Controller
					name="name"
					control={control}
					render={({ field }) => (
						<TextField
							label="Nome"
							{...field}
							error={!!errors.name}
							helperText={errors.name?.message}
							fullWidth
						/>
					)}
				/>
			</Grid>
			<Grid size={{ xs: 12, sm: 3, md: 2 }}>
				<SelectField
					label="Tipo"
					labelId="id"
					labelKey="value"
					options={getTypeTreatment()}
					valueKey="id"
					defaultValue={String(watch("type"))}
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
