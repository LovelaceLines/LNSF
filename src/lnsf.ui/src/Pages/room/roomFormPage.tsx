import { Button, Divider, Grid2 as Grid, TextField } from "@mui/material";

import { useRoomFormPage } from "./useRoomFormPage";
import { Checkbox } from "@/components";

export const RoomFormPage = () => {
	const { id, register, handleSubmit, errors, handleSave, watch } = useRoomFormPage();

	return (
		<Grid container spacing={2} component="form" onSubmit={handleSubmit(handleSave)}>
			<Grid size={{ xs: 12 }}>
				<Divider>Dados do Apartamento</Divider>
			</Grid>
			<Grid size={{ xs: 4, sm: 1.5, md: 1 }}>
				<TextField label="Id" disabled={!id} {...register("id")} error={!!errors.id} helperText={errors.id?.message} fullWidth />
			</Grid>
			<Grid size={{ xs: 8, sm: 4.5, md: 2 }}>
				<TextField label="Número" {...register("number")} error={!!errors.number} helperText={errors.number?.message} fullWidth />
			</Grid>
			<Grid size={{ xs: 6, sm: 3, md: 1 }}>
				<TextField
					type="number"
					label="Camas"
					{...register("beds")}
					error={!!errors.beds}
					helperText={errors.beds?.message}
					fullWidth
				/>
			</Grid>
			<Grid size={{ xs: 6, sm: 3, md: 1 }}>
				<TextField
					type="number"
					label="Andar"
					{...register("storey")}
					error={!!errors.storey}
					helperText={errors.storey?.message}
					fullWidth
				/>
			</Grid>
			<Grid my="auto">
				<Checkbox label="Possui banheiro" checked={watch("bathroom")} register={register("bathroom")} />
				<Checkbox label="Disponível" checked={watch("available")} register={register("available")} />
			</Grid>
			<Grid size={{ xs: 12 }}>
				<Button type="submit" variant="contained" color="primary" fullWidth>
					Salvar
				</Button>
			</Grid>
		</Grid>
	);
};
