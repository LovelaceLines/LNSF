import { Button, Divider, Grid2 as Grid, TextField } from "@mui/material";

import { DateField, SelectField } from "@/components";
import { EmergencyContactFormPage } from "@/pages/emergencyContact";
import { getGender, getMaritalStatus, getRaceColor, tour } from "@/types";
import { usePeopleFormPage } from "./usePeopleFormPage";
import { TourFormPage } from "@/pages/tour";
import { dateOnlyToStr } from "@/utils";

export const PeopleFormPage = () => {
	const { id, register, handleSubmit, errors, watch, getValues, setValue, handleSave } = usePeopleFormPage();

	return (
		<>
			<Grid container spacing={2} component="form" onSubmit={handleSubmit(handleSave)}>
				<Grid size={{ xs: 12 }}>
					<Divider>Dados Pessoais</Divider>
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
				<Grid size={{ xs: 12, sm: 4, md: 5 }}>
					<TextField label="Nome" {...register("name")} error={!!errors.name} helperText={errors.name?.message} fullWidth />
				</Grid>
				<Grid size={{ xs: 12, sm: 4 }}>
					<TextField label="RG" {...register("rg")} error={!!errors.rg} helperText={errors.rg?.message} fullWidth />
				</Grid>
				<Grid size={{ xs: 12, sm: 2 }}>
					<TextField
						label="Orgão Emissor"
						{...register("issuingBody")}
						error={!!errors.issuingBody}
						helperText={errors.issuingBody?.message}
						fullWidth
					/>
				</Grid>
				<Grid size={{ xs: 12, sm: 4 }}>
					<TextField label="CPF" {...register("cpf")} error={!!errors.cpf} helperText={errors.cpf?.message} fullWidth />
				</Grid>
				<Grid size={{ xs: 12, sm: 4 }}>
					<TextField
						label="Telefone"
						{...register("phone")}
						error={!!errors.phone}
						helperText={errors.phone?.message}
						fullWidth
					/>
				</Grid>
				<Grid size={{ xs: 12, sm: 4 }}>
					<DateField
						label="Data de Nascimento"
						value={dateOnlyToStr(watch("birthDate"))}
						register={register("birthDate")}
						error={!!errors.birthDate}
						helperText={errors.birthDate?.message}
					/>
				</Grid>
				<Grid size={{ xs: 12, sm: 4 }}>
					<SelectField
						label="Sexo"
						labelId="id"
						labelKey="value"
						options={getGender()}
						valueKey="id"
						defaultValue={id && `${getValues("gender")}`}
						onClick={(value) => setValue("gender", Number(value))}
					/>
				</Grid>
				<Grid size={{ xs: 12, sm: 4 }}>
					<SelectField
						label="Estado Civil"
						labelId="id"
						labelKey="value"
						options={getMaritalStatus()}
						valueKey="id"
						defaultValue={id && `${getValues("maritalStatus")}`}
						onClick={(value) => setValue("maritalStatus", Number(value))}
					/>
				</Grid>
				<Grid size={{ xs: 12, sm: 4 }}>
					<SelectField
						label="Raça/Cor"
						labelId="id"
						labelKey="value"
						options={getRaceColor()}
						valueKey="id"
						defaultValue={id && `${getValues("raceColor")}`}
						onClick={(value) => setValue("raceColor", Number(value))}
					/>
				</Grid>
				<Grid size={{ xs: 12 }}>
					<Divider>Endereço</Divider>
				</Grid>
				<Grid size={{ xs: 12, sm: 10 }}>
					<TextField label="Rua" {...register("street")} error={!!errors.street} helperText={errors.street?.message} fullWidth />
				</Grid>
				<Grid size={{ xs: 12, sm: 2 }}>
					<TextField
						label="Número"
						{...register("houseNumber")}
						error={!!errors.houseNumber}
						helperText={errors.houseNumber?.message}
						fullWidth
					/>
				</Grid>
				<Grid size={{ xs: 12, sm: 4 }}>
					<TextField label="Estado" {...register("state")} error={!!errors.state} helperText={errors.state?.message} fullWidth />
				</Grid>
				<Grid size={{ xs: 12, sm: 4 }}>
					<TextField label="Cidade" {...register("city")} error={!!errors.city} helperText={errors.city?.message} fullWidth />
				</Grid>
				<Grid size={{ xs: 12, sm: 4 }}>
					<TextField
						label="Bairro"
						{...register("neighborhood")}
						error={!!errors.neighborhood}
						helperText={errors.neighborhood?.message}
						fullWidth
					/>
				</Grid>
				<Grid size={{ xs: 12 }}>
					<Divider>Observações</Divider>
				</Grid>
				<Grid size={{ xs: 12, sm: 12 }}>
					<TextField
						type=""
						label="Observação"
						// multiline // TODO - Fix - Bug nos campos de texto
						// rows={4} // TODO - Fix - Bug nos campos de texto
						{...register("note")}
						error={!!errors.note}
						helperText={errors.note?.message}
						fullWidth
					/>
				</Grid>
				<Grid size={{ xs: 12 }}>
					<Button type="submit" variant="contained" fullWidth>
						{id ? "Atualizar" : "Cadastrar"}
					</Button>
				</Grid>
			</Grid>
			<Grid container spacing={2} size={{ xs: 12 }} mt={2}>
				<Grid size={{ xs: 12 }}>
					<Divider>Informações de Contato de Emergência</Divider>
				</Grid>
				{getValues("emergencyContacts")?.map((emergencyContact, index: number) => (
					<Grid size={{ xs: 12 }} key={index}>
						<EmergencyContactFormPage emergencyContact={emergencyContact} />
					</Grid>
				))}
				<Grid size={{ xs: 12 }}>
					<EmergencyContactFormPage emergencyContact={{ peopleId: Number(id), name: "", phone: "" }} />
				</Grid>
				<Grid size={{ xs: 12 }}>
					<Divider>Registro de Entradas/Saídas</Divider>
				</Grid>
				{getValues("tours")?.map((tour, index: number) => (
					<Grid size={{ xs: 12 }} key={index}>
						<TourFormPage tour={tour} />
					</Grid>
				))}
				<Grid size={{ xs: 12 }}>
					<TourFormPage tour={{ peopleId: Number(id) } as tour} />
				</Grid>
			</Grid>
		</>
	);
};
