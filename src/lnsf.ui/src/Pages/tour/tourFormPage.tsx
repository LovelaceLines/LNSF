import { Edit, Input } from "@mui/icons-material";
import { Grid2 as Grid, IconButton, TextField, Tooltip } from "@mui/material";

import { useTourFormPage } from "./useTourFormPage";
import { tour } from "@/types";
import { DateTimeField } from "@/components";
import { dateTimeToStr } from "@/utils";

export const TourFormPage = ({ tour }: { tour?: tour }) => {
	const { handleSave, handlePutAll, errors, handleSubmit, register, watch } = useTourFormPage({
		tour,
	});

	return (
		<Grid container spacing={2} alignItems="center" component="form" onSubmit={handleSubmit(handleSave)}>
			<Grid size={{ xs: 6, sm: 1.5, md: 1 }}>
				<TextField
					label="Id Pessoa"
					disabled
					{...register("peopleId")}
					error={!!errors.peopleId}
					helperText={errors.peopleId?.message}
					fullWidth
				/>
			</Grid>
			<Grid size={{ xs: 6, sm: 1.5, md: 1 }}>
				<TextField
					label="Id"
					disabled
					{...register("id")}
					error={!!errors.id}
					helperText={errors.id?.message}
					fullWidth
				/>
			</Grid>
			<Grid size={{ xs: 12, sm: 4.5, md: 2 }}>
				<DateTimeField
					label="Saída"
					value={dateTimeToStr(watch("output"))}
					register={register("output")}
					error={!!errors.output}
					helperText={errors.output?.message}
				/>
			</Grid>
			<Grid size={{ xs: 12, sm: 4.5, md: 2 }}>
				<DateTimeField
					label="Entrada"
					value={dateTimeToStr(watch("input"))}
					register={register("input")}
					error={!!errors.input}
					helperText={errors.input?.message}
				/>
			</Grid>
			<Grid size="grow">
				<TextField
					label="Observação"
					{...register("note")}
					error={!!errors.note}
					helperText={errors.note?.message}
					fullWidth
				/>
			</Grid>
			<Grid direction="row" spacing={2} wrap="nowrap">
				{!watch("input") && (
					<Tooltip title={watch("id") ? "Registrar entrada" : "Registrar saída"}>
						<IconButton type="submit" color="primary" size="large">
							<Input />
						</IconButton>
					</Tooltip>
				)}
				{watch("id") ? (
					<Tooltip title="Editar todos os valores">
						<IconButton onClick={() => handlePutAll(watch())} color="warning" size="large">
							<Edit />
						</IconButton>
					</Tooltip>
				) : null}
			</Grid>
		</Grid>
	);
};
