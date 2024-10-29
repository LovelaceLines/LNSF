import { Grid2 as Grid, IconButton, TextField } from "@mui/material";
import { Add, Delete } from "@mui/icons-material";
import { useCallback } from "react";
import { Controller } from "react-hook-form";

import { SelectField } from "@/components";
import { patientTreatment } from "@/types";
import { usePatientTreatmentFormPage } from "./usePatientTreatmentFormPage";

interface PatientTreatmentFormPageProps {
	patientTreatment: patientTreatment;
	mode: "add" | "delete";
	onSave: (patientId: number, treatmentId: number) => void;
}

export const PatientTreatmentFormPage = ({
	patientTreatment,
	mode,
	onSave,
}: PatientTreatmentFormPageProps) => {
	const { treatments, control, getValues, register, setValue, watch } = usePatientTreatmentFormPage({
		patientTreatment,
	});

	const handleSave = useCallback(() => {
		onSave(getValues("patientId"), getValues("treatmentId"));
	}, [getValues("patientId"), getValues("treatmentId"), onSave]);

	return (
		<Grid container spacing={2} alignItems="center">
			<Grid size={{ xs: 12, sm: 2 }}>
				<Controller
					name="patientId"
					control={control}
					render={({ field }) => (
						<TextField type="number" disabled label="Id Paciente" {...field} fullWidth />
					)}
				/>
			</Grid>
			<Grid size={{ xs: 12, sm: 2 }}>
				<TextField
					type="number"
					label="Id Tramento"
					value={Number(watch("treatmentId"))}
					{...register("treatmentId")}
					fullWidth
					onChange={(e) => setValue("treatmentId", Number(e.target.value))}
				/>
			</Grid>
			<Grid size="grow">
				<SelectField
					label="Tratamento"
					labelId="id"
					labelKey="name"
					options={treatments}
					valueKey="id"
					defaultValue={String(watch("treatmentId"))}
					onClick={(value) => setValue("treatmentId", Number(value))}
				/>
			</Grid>
			<Grid wrap="nowrap">
				{mode === "add" ? (
					<IconButton onClick={handleSave} color="info" size="large">
						<Add />
					</IconButton>
				) : (
					<IconButton onClick={handleSave} color="error" size="large">
						<Delete />
					</IconButton>
				)}
			</Grid>
		</Grid>
	);
};
