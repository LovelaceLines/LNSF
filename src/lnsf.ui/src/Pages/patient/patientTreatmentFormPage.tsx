import { Grid2 as Grid, IconButton, TextField } from "@mui/material";
import { Add, Delete } from "@mui/icons-material";

import { SelectField } from "@/components";
import { patientTreatment } from "@/types";
import { usePatientTreatmentFormPage } from "./usePatientTreatmentFormPage";

export const PatientTreatmentFormPage = ({ patientTreatment }: { patientTreatment: patientTreatment }) => {
	const { treatments, addTreatmentToPatient, removeTreatmentFromPatient, watch, setValue } = usePatientTreatmentFormPage({
		patientTreatment,
	});

	return (
		<Grid container spacing={2} alignItems="center">
			<Grid size={{ xs: 12, sm: 2, md: 1.5 }}>
				<TextField label="Id Tramento" value={watch("treatmentId")} fullWidth />
			</Grid>
			<Grid size={{ xs: 12, sm: 6, lg: 5 }}>
				<SelectField
					label="Tratamento"
					labelId="id"
					labelKey="name"
					options={treatments}
					valueKey="id"
					defaultValue={String(patientTreatment.treatmentId)}
					onClick={(value) => setValue("treatmentId", Number(value))}
				/>
			</Grid>
			<Grid>
				<IconButton onClick={() => addTreatmentToPatient(watch("patientId"), watch("treatmentId"))} color="primary" size="large">
					<Add />
				</IconButton>
				<IconButton
					onClick={() => removeTreatmentFromPatient(watch("patientId"), watch("treatmentId"))}
					color="warning"
					size="large"
				>
					<Delete />
				</IconButton>
			</Grid>
		</Grid>
	);
};
