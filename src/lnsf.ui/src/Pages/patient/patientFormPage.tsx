import { Button, Divider, Grid2 as Grid, TextField } from "@mui/material";

import { Checkbox, SelectField } from "@/components";
import { usePatientFormPage } from "./usePatientFormPage";
import { PatientTreatmentFormPage } from "./patientTreatmentFormPage";

export const PatientFormPage = () => {
	const {
		addTreatmentToPatient,
		removeTreatmentFromPatient,
		peoples,
		hospitals,
		handleSave,
		errors,
		handleSubmit,
		register,
		setValue,
		watch,
	} = usePatientFormPage();

	return (
		<>
			<Grid container spacing={2} component="form" onSubmit={handleSubmit(handleSave)}>
				<Grid size={{ xs: 12 }}>
					<Divider>Dados do Paciente</Divider>
				</Grid>
				<Grid size={{ xs: 12, sm: 1.5, md: 1 }}>
					<TextField
						label="Id"
						type="number"
						disabled={!watch("id")}
						{...register("id")}
						error={!!errors.id}
						helperText={errors.id?.message}
						fullWidth
					/>
				</Grid>
				<Grid size={{ xs: 12, sm: 2, md: 1.5 }}>
					<TextField
						label="Id Pessoa"
						type="number"
						{...register("peopleId")}
						error={!!errors.peopleId}
						helperText={errors.peopleId?.message}
						onChange={(e) => setValue("peopleId", Number(e.target.value))}
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
						defaultValue={String(watch("peopleId"))}
						onClick={(value) => setValue("peopleId", value)}
					/>
				</Grid>
				<Grid size={{ xs: 12, sm: 2, md: 1.5 }}>
					<TextField
						label="Id Hospital"
						type="number"
						{...register("hospitalId")}
						error={!!errors.hospitalId}
						helperText={errors.hospitalId?.message}
						onChange={(e) => setValue("hospitalId", Number(e.target.value))}
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
						defaultValue={String(watch("hospitalId"))}
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
						{watch("id") ? "Atualizar" : "Cadastrar"}
					</Button>
				</Grid>
			</Grid>
			<Grid container direction="column" spacing={2} mt={2}>
				<Grid size={{ xs: 12 }}>
					<Divider>Tratamentos do Paciente</Divider>
				</Grid>
				{watch("treatments", [])?.map((treatment, index: number) => (
					<Grid size={{ xs: 6 }} key={index}>
						<PatientTreatmentFormPage
							patientTreatment={{ patientId: Number(watch("id")), treatmentId: treatment.id! }}
							mode="delete"
							onSave={removeTreatmentFromPatient}
						/>
					</Grid>
				))}
				<Grid size={{ xs: 6 }}>
					<PatientTreatmentFormPage
						patientTreatment={{ patientId: Number(watch("id")), treatmentId: 0 }}
						mode="add"
						onSave={addTreatmentToPatient}
					/>
				</Grid>
			</Grid>
		</>
	);
};
