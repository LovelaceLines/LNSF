import { Button, Divider, Grid2 as Grid, TextField } from "@mui/material";

import { Checkbox, SelectField } from "@/components";
import { usePatientFormPage } from "./usePatientFormPage";
import { PatientTreatmentFormPage } from "./patientTreatmentFormPage";
import { patientTreatment } from "@/types";

export const PatientFormPage = () => {
	const { id, peoples, hospitals, register, handleSubmit, errors, watch, getValues, setValue, handleSave } = usePatientFormPage();

	return (
		<>
			<Grid container spacing={2} component="form" onSubmit={handleSubmit(handleSave)}>
				<Grid size={{ xs: 12 }}>
					<Divider>Dados do Paciente</Divider>
				</Grid>
				<Grid size={{ xs: 12, sm: 1.5, md: 1 }}>
					<TextField
						label="Id"
						disabled={!id}
						{...register("id")}
						error={!!errors.id}
						helperText={errors.id?.message}
						fullWidth
					/>
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
				<Grid size={{ xs: 12, sm: 2, md: 1.5 }}>
					<TextField
						label="Id Hospital"
						{...register("hospitalId")}
						error={!!errors.hospitalId}
						helperText={errors.hospitalId?.message}
						fullWidth
					/>
				</Grid>
				<Grid size={{ xs: 12, sm: 3, lg: 4 }}>
					<SelectField
						label="Hospital"
						labelId="id"
						labelKey="name"
						options={hospitals}
						valueKey="id"
						defaultValue={String(getValues("hospitalId"))}
						onClick={(value) => setValue("hospitalId", value)}
					/>
				</Grid>
				<Grid my="auto">
					<Checkbox label="Termo" checked={watch("term")} register={register("term")} />
					<Checkbox
						label="Registro Socioeconômico"
						checked={watch("socioeconomicRecord")}
						register={register("socioeconomicRecord")}
					/>
				</Grid>
				<Grid size={{ xs: 12 }}>
					<Button type="submit" variant="contained" fullWidth>
						{id ? "Atualizar" : "Cadastrar"}
					</Button>
				</Grid>
			</Grid>
			<Grid container spacing={2} mt={2}>
				<Grid size={{ xs: 12 }}>
					<Divider>Tratamentos do Paciente</Divider>
				</Grid>
				{getValues("treatments")?.map((treatment, index: number) => (
					<Grid size={{ xs: 12 }} key={index}>
						<PatientTreatmentFormPage patientTreatment={{ patientId: Number(id), treatmentId: treatment.id! }} />
					</Grid>
				))}
				<Grid size={{ xs: 12 }}>
					<PatientTreatmentFormPage patientTreatment={{ patientId: Number(id), treatmentId: 0 }} />
				</Grid>
			</Grid>
		</>
	);
};
