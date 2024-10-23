import { Edit, Input } from "@mui/icons-material";
import { Grid2 as Grid, IconButton, TextField, Tooltip } from "@mui/material";

import { useTourFormPage } from "./useTourFormPage";
import { tour } from "@/types";
import { DateTimeField } from "@/components";
import { formatDateTime } from "@/utils";

export const TourFormPage = ({ tour }: { tour?: tour }) => {
	const { errors, getValues, handlePutAll, handleSave, handleSubmit, register, watch } = useTourFormPage({ tour });

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
			<Grid size={{ xs: 12, sm: 5.25, md: 2 }}>
				<DateTimeField
					label="Saída"
					value={formatDateTime(watch("output"))}
					register={register("output")}
					error={!!errors.output}
					helperText={errors.output?.message}
				/>
			</Grid>
			<Grid size={{ xs: 12, sm: 5.25, md: 2 }}>
				<DateTimeField
					label="Entrada"
					value={formatDateTime(watch("input"))}
					register={register("input")}
					error={!!errors.input}
					helperText={errors.input?.message}
				/>
			</Grid>
			<Grid size={{ xs: 12, sm: 11, md: 6 }}>
				<TextField label="Observação" {...register("note")} error={!!errors.note} helperText={errors.note?.message} fullWidth />
			</Grid>
			<Grid direction="row" spacing={2} size={{ xs: 12, sm: 1 }}>
				<Tooltip title={getValues("id") ? "Registrar entrada" : "Registrar saída"}>
					<IconButton type="submit" color="primary" size="large">
						<Input />
					</IconButton>
				</Tooltip>
				<Tooltip title="Editar todos os valores">
					<IconButton onClick={() => handlePutAll(getValues())} color="warning" size="large">
						<Edit />
					</IconButton>
				</Tooltip>
			</Grid>
		</Grid>
	);
};
