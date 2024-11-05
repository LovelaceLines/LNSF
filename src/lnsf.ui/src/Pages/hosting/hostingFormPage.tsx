import { Button, Divider, Grid2 as Grid, IconButton, TextField } from "@mui/material";
import { Add, Delete } from "@mui/icons-material";

import { useHostingFormPage } from "./useHostingFormPage";
import { DateField, SelectField } from "@/components";
import { dateOnlyToStr } from "@/utils";
import { PeopleRoomHostingFormPage } from "./peopleRoomHostingFormPage";

export const HostingFormPage = () => {
	const {
		prh,
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
		<>
			<Grid container spacing={2} component="form" onSubmit={handleSubmit(handleSave)}>
				<Grid size={{ xs: 12 }}>
					<Divider>Dados da Reserva</Divider>
				</Grid>
				<Grid size={{ xs: 2, sm: 1.5, md: 1 }}>
					<TextField
						label="Id"
						disabled
						{...register("id")}
						error={!!errors.id}
						helperText={errors.id?.message}
						fullWidth
					/>
				</Grid>
				<Grid size={{ xs: 2, sm: 2, md: 1 }}>
					<TextField
						label="Id Paciente"
						type="number"
						{...register("patientId")}
						error={!!errors.patientId}
						helperText={errors.patientId?.message}
						onChange={(e) => setValue("patientId", +e.target.value)}
						fullWidth
					/>
				</Grid>
				<Grid size={{ xs: 8, sm: 8.5, md: 4 }}>
					<SelectField
						label="Nome Paciente"
						options={patients}
						valueKey="id"
						labelId="id"
						labelKey="people.name"
						defaultValue={String(watch("patientId"))}
						onClick={(value) => setValue("patientId", value)}
					/>
				</Grid>
				<Grid size={{ xs: 6, sm: 6, md: 3 }}>
					<DateField
						label="Check-in"
						value={dateOnlyToStr(watch("checkIn"))}
						register={register("checkIn")}
						error={!!errors.checkIn}
						helperText={errors.checkIn?.message}
					/>
				</Grid>
				<Grid size={{ xs: 6, sm: 6, md: 3 }}>
					<DateField
						label="Check-out"
						value={dateOnlyToStr(watch("checkOut"))}
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
			</Grid>
			<Grid container direction="column" spacing={2} mt={2}>
				<Grid size={{ xs: 12 }}>
					<Divider>Acompanhantes da Reserva</Divider>
				</Grid>
				{watch("escorts", []).map((escort, index) => (
					<>
						<Grid container spacing={2} size={{ xs: 12, md: 6 }} key={index}>
							<Grid size={{ xs: 2, sm: 2 }}>
								<TextField label="Id Acompanhante" value={escort.id} fullWidth />
							</Grid>
							<Grid size="grow">
								<TextField label="Nome Acompanhante" value={escort.people?.name} fullWidth />
							</Grid>
							<Grid wrap="nowrap">
								<IconButton
									color="error"
									size="large"
									onClick={() =>
										removeEscortFromHosting({
											hostingId: +getValues("id")!,
											escortId: escort.id!,
										})
									}
								>
									<Delete />
								</IconButton>
							</Grid>
						</Grid>
					</>
				))}
				<Grid container spacing={2} size={{ xs: 12, md: 6 }}>
					<Grid size={{ xs: 2, sm: 2 }}>
						<TextField
							label="Id Acompanhante"
							type="number"
							value={escort?.id ?? ""}
							fullWidth
							onChange={(ev) => setEscort(escorts.find((es) => es.id === +ev.target.value))}
						/>
					</Grid>
					<Grid size="grow">
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
					<Grid wrap="nowrap">
						<IconButton
							type="submit"
							color="info"
							size="large"
							onClick={() =>
								addEscortToHosting({ hostingId: +getValues("id")!, escortId: escort!.id! })
							}
						>
							<Add />
						</IconButton>
					</Grid>
				</Grid>
			</Grid>
			<Grid container spacing={2} mt={2}>
				<Grid size={{ xs: 12 }}>
					<Divider>Apartamento/Hospedagem</Divider>
				</Grid>
				{prh.length > 0 && (
					<>
						<Grid container spacing={2} mb={2}>
							{prh.map((prh, index) => (
								<>
									<Grid size={{ xs: 12 }} key={index}>
										<PeopleRoomHostingFormPage prh={prh} mode="delete" />
									</Grid>
								</>
							))}
						</Grid>
					</>
				)}
				<Grid size={{ xs: 2, sm: 2, md: 1 }}>
					<TextField
						label="Id Apartamento"
						type="number"
						value={room?.id ?? ""}
						fullWidth
						onChange={(e) => setRoom(rooms.find((r) => r.id === +e.target.value))}
					/>
				</Grid>
				<Grid size={{ xs: 10, sm: 10, md: 5 }}>
					<SelectField
						label="Apartamento"
						options={rooms.map((r) => ({
							...r,
							title: `Nº ${r.number} - ${r.beds} camas - ${r.storey}º andar`,
						}))}
						valueKey="id"
						labelId="id"
						labelKey="title"
						defaultValue={String(room?.id)}
						onClick={(value) => setRoom(rooms.find((r) => r.id === value))}
					/>
				</Grid>
				<Grid size={{ xs: 12 }}></Grid>
				{room &&
					watch("escorts", [])
						.map((e) => e.people) // Pessoas que são acompanhantes da reserva
						.concat(watch("patient.people")) // Pessoa que é paciente da reserva
						.filter((p) => !prh.map((p) => p.peopleId).includes(p?.id ?? 0)) // Pessoas que não estão hospedadas
						.map((p) => (
							<>
								<Grid size={{ xs: 12 }}>
									<PeopleRoomHostingFormPage
										mode="add"
										prh={{
											peopleId: p?.id ?? 0,
											people: p,
											roomId: room.id,
											room: room,
											hostingId: +watch("id")!,
										}}
									/>
								</Grid>
							</>
						))}
			</Grid>
		</>
	);
};
