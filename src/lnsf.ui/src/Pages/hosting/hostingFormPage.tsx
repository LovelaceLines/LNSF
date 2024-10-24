import { Button, Divider, Grid2 as Grid, IconButton, TextField } from "@mui/material";

import { useHostingFormPage } from "./useHostingFormPage";
import { DateField, SelectField } from "@/components";
import { dateOnlyToStr } from "@/utils";
import { Add, Delete } from "@mui/icons-material";
import { PeopleRoomHostingFormPage } from "./peopleRoomHostingFormPage";

export const HostingFormPage = () => {
	const {
		id,
		patients,
		escorts,
		escort,
		setEscort,
		room,
		setRoom,
		rooms,
		addEscortToHosting,
		removeEscortFromHosting,
		register,
		handleSubmit,
		errors,
		handleSave,
		getValues,
		setValue,
		watch,
	} = useHostingFormPage();

	return (
		<Grid container spacing={2} component="form" onSubmit={handleSubmit(handleSave)}>
			<Grid size={{ xs: 12 }}>
				<Divider>Dados da Hospedagem</Divider>
			</Grid>
			<Grid size={{ xs: 4, sm: 1.5, md: 1 }}>
				<TextField label="Id" disabled={!id} {...register("id")} error={!!errors.id} helperText={errors.id?.message} fullWidth />
			</Grid>
			<Grid size={{ xs: 8, sm: 2, md: 1 }}>
				<TextField
					label="Id Paciente"
					{...register("patientId")}
					error={!!errors.patientId}
					helperText={errors.patientId?.message}
					fullWidth
				/>
			</Grid>
			<Grid size={{ xs: 12, sm: 8.5, md: 4 }}>
				<SelectField
					label="Nome Paciente"
					options={patients}
					valueKey="id"
					labelId="id"
					labelKey="people.name"
					defaultValue={String(getValues("patientId"))}
					onClick={(value) => setValue("patientId", value)}
				/>
			</Grid>
			<Grid size={{ xs: 6, sm: 6, md: 3 }}>
				<DateField
					label="Check-in"
					value={dateOnlyToStr(getValues("checkIn"))}
					register={register("checkIn")}
					error={!!errors.checkIn}
					helperText={errors.checkIn?.message}
				/>
			</Grid>
			<Grid size={{ xs: 6, sm: 6, md: 3 }}>
				<DateField
					label="Check-out"
					value={dateOnlyToStr(getValues("checkOut"))}
					register={register("checkOut")}
					error={!!errors.checkOut}
					helperText={errors.checkOut?.message}
				/>
			</Grid>
			<Grid size={{ xs: 12 }}>
				<Button type="submit" variant="contained" color="primary" fullWidth>
					Salvar
				</Button>
			</Grid>
			<Grid size={{ xs: 12 }}>
				<Divider>Acompanhantes</Divider>
			</Grid>
			{watch("escorts", []).map((escort, index) => (
				<>
					<Grid container spacing={2} size={{ xs: 12 }} key={index}>
						<Grid size={{ xs: 4, sm: 2, md: 1 }}>
							<TextField label="Id Acompanhante" value={escort.id} fullWidth />
						</Grid>
						<Grid size={{ xs: 8, sm: 7, md: 4 }}>
							<TextField label="Nome Acompanhante" value={escort.people?.name} fullWidth />
						</Grid>
						<Grid size={{ xs: 12, sm: 3, md: 1 }}>
							<IconButton
								type="submit"
								color="primary"
								size="large"
								onClick={() => addEscortToHosting(Number(id), escort.id!)}
							>
								<Add />
							</IconButton>
							<IconButton color="primary" size="large" onClick={() => removeEscortFromHosting(Number(id), escort.id!)}>
								<Delete />
							</IconButton>
						</Grid>
					</Grid>
				</>
			))}
			<Grid container spacing={2} size={{ xs: 12 }}>
				<Grid size={{ xs: 4, sm: 2, md: 1 }}>
					<TextField label="Id Acompanhante" value={escort?.id ?? ""} fullWidth />
				</Grid>
				<Grid size={{ xs: 8, sm: 7, md: 4 }}>
					<SelectField
						label="Nome Acompanhante"
						options={escorts}
						valueKey="id"
						labelId="id"
						labelKey="people.name"
						defaultValue={String(escort?.id)}
						onClick={(value) => setEscort(escorts.find((e) => e.id === value))}
					/>
				</Grid>
				<Grid size={{ xs: 12, sm: 3, md: 1 }}>
					<IconButton type="submit" color="primary" size="large" onClick={() => addEscortToHosting(Number(id), escort!.id!)}>
						<Add />
					</IconButton>
					<IconButton color="primary" size="large" onClick={() => removeEscortFromHosting(Number(id), escort!.id!)}>
						<Delete />
					</IconButton>
				</Grid>
			</Grid>
			<Grid size={{ xs: 12 }}>
				<Divider>Apartamento</Divider>
			</Grid>
			<Grid size={{ xs: 4, sm: 2, md: 1 }}>
				<TextField
					label="Id Apartamento"
					value={room?.id ?? ""}
					fullWidth
					onChange={(e) => setRoom(rooms.find((r) => r.id === Number(e.target.value)))}
				/>
			</Grid>
			<Grid size={{ xs: 8, sm: 7, md: 4 }}>
				<SelectField
					label="Apartamento"
					options={rooms}
					valueKey="id"
					labelId="id"
					labelKey="number"
					defaultValue={String(room?.id)}
					onClick={(value) => setRoom(rooms.find((r) => r.id === value))}
				/>
			</Grid>
			<Grid size={{ xs: 12 }}></Grid>
			{room && (
				<>
					<Grid size={{ xs: 12 }}>
						<PeopleRoomHostingFormPage
							prh={{
								peopleId: getValues("patient.people.id") ?? 0,
								people: getValues("patient.people"),
								roomId: room.id,
								room: room,
								hostingId: Number(id),
							}}
						/>
					</Grid>
					{getValues("escorts").map((escort, index) => (
						<Grid size={{ xs: 12 }} key={index}>
							<PeopleRoomHostingFormPage
								prh={{
									peopleId: escort.people?.id ?? 0,
									people: escort.people!,
									roomId: room.id,
									room: room,
									hostingId: Number(id),
								}}
							/>
						</Grid>
					))}
				</>
			)}
		</Grid>
	);
};
