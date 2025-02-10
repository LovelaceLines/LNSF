import { Button, Divider, Grid2 as Grid, TextField } from "@mui/material";
import { Controller } from "react-hook-form";

import { DateField, SelectField } from "@/components";
import { EmergencyContactFormPage } from "@/pages/emergencyContact";
import { TourFormPage } from "@/pages/tour";
import { getGender, getMaritalStatus, getRaceColor, tour } from "@/types";
import { usePeopleFormPage } from "./usePeopleFormPage";
import { dateOnlyToStr } from "@/utils";

export const PeopleFormPage = () => {
  const { handleSave, errors, control, handleSubmit, register, setValue, watch } = usePeopleFormPage();

  return (
    <>
      <Grid container spacing={2} component="form" onSubmit={handleSubmit(handleSave)}>
        <Grid size={{ xs: 12 }}>
          <Divider>Dados Pessoais</Divider>
        </Grid>
        <Grid size={{ xs: 12, sm: 1.5, md: 1 }}>
          <Controller
            name="id"
            control={control}
            render={({ field }) => (
              <TextField
                {...field}
                label="Id"
                type="number"
                disabled
                error={!!errors.id}
                helperText={errors.id?.message}
                fullWidth
              />
            )}
          />
        </Grid>
        <Grid size={{ xs: 12, sm: 4.5, md: 5 }}>
          <Controller
            name="name"
            control={control}
            render={({ field }) => (
              <TextField
                {...field}
                label="Nome"
                error={!!errors.name}
                helperText={errors.name?.message}
                fullWidth
              />
            )}
          />
        </Grid>
        <Grid size={{ xs: 12, sm: 4 }}>
          <Controller
            name="rg"
            control={control}
            render={({ field }) => (
              <TextField
                {...field}
                label="RG"
                error={!!errors.rg}
                helperText={errors.rg?.message}
                fullWidth
              />
            )}
          />
        </Grid>
        <Grid size={{ xs: 12, sm: 2 }}>
          <Controller
            name="issuingBody"
            control={control}
            render={({ field }) => (
              <TextField
                {...field}
                label="Orgão Emissor"
                error={!!errors.issuingBody}
                helperText={errors.issuingBody?.message}
                fullWidth
              />
            )}
          />
        </Grid>
        <Grid size={{ xs: 12, sm: 3 }}>
          <Controller
            name="cpf"
            control={control}
            render={({ field }) => (
              <TextField
                {...field}
                label="CPF"
                error={!!errors.cpf}
                helperText={errors.cpf?.message}
                fullWidth
              />
            )}
          />
        </Grid>
        <Grid size={{ xs: 12, sm: 5.5 }}>
          <Controller
            name="email"
            control={control}
            render={({ field }) => (
              <TextField
                {...field}
                label="Email"
                error={!!errors.email}
                helperText={errors.email?.message}
                fullWidth
              />
            )}
          />
        </Grid>
        <Grid size={{ xs: 12, sm: 3.5 }}>
          <Controller
            name="phone"
            control={control}
            render={({ field }) => (
              <TextField
                {...field}
                label="Telefone"
                error={!!errors.phone}
                helperText={errors.phone?.message}
                fullWidth
              />
            )}
          />
        </Grid>
        <Grid size={{ xs: 12, sm: 3 }}>
          <DateField
            label="Data de Nascimento"
            value={dateOnlyToStr(watch("birthDate"))}
            register={register("birthDate")}
            error={!!errors.birthDate}
            helperText={errors.birthDate?.message}
          />
        </Grid>
        <Grid size={{ xs: 12, sm: 3 }}>
          <SelectField
            label="Sexo"
            labelId="id"
            labelKey="value"
            options={getGender()}
            valueKey="id"
            defaultValue={String(watch("gender"))}
            onClick={(value) => setValue("gender", Number(value))}
          />
        </Grid>
        <Grid size={{ xs: 12, sm: 3 }}>
          <SelectField
            label="Estado Civil"
            labelId="id"
            labelKey="value"
            options={getMaritalStatus()}
            valueKey="id"
            defaultValue={String(watch("maritalStatus"))}
            onClick={(value) => setValue("maritalStatus", Number(value))}
          />
        </Grid>
        <Grid size={{ xs: 12, sm: 3 }}>
          <SelectField
            label="Raça/Cor"
            labelId="id"
            labelKey="value"
            options={getRaceColor()}
            valueKey="id"
            defaultValue={String(watch("raceColor"))}
            onClick={(value) => setValue("raceColor", Number(value))}
          />
        </Grid>
        <Grid size={{ xs: 12 }}>
          <Divider>Endereço</Divider>
        </Grid>
        <Grid size={{ xs: 12, sm: 10 }}>
          <Controller
            name="street"
            control={control}
            render={({ field }) => (
              <TextField
                {...field}
                label="Rua"
                error={!!errors.street}
                helperText={errors.street?.message}
                fullWidth
              />
            )}
          />
        </Grid>
        <Grid size={{ xs: 12, sm: 2 }}>
          <Controller
            name="houseNumber"
            control={control}
            render={({ field }) => (
              <TextField
                {...field}
                label="Número"
                error={!!errors.houseNumber}
                helperText={errors.houseNumber?.message}
                fullWidth
              />
            )}
          />
        </Grid>
        <Grid size={{ xs: 12, sm: 4 }}>
          <Controller
            name="state"
            control={control}
            render={({ field }) => (
              <TextField
                {...field}
                label="Estado"
                error={!!errors.state}
                helperText={errors.state?.message}
                fullWidth
              />
            )}
          />
        </Grid>
        <Grid size={{ xs: 12, sm: 4 }}>
          <Controller
            name="city"
            control={control}
            render={({ field }) => (
              <TextField
                {...field}
                label="Cidade"
                error={!!errors.city}
                helperText={errors.city?.message}
                fullWidth
              />
            )}
          />
        </Grid>
        <Grid size={{ xs: 12, sm: 4 }}>
          <Controller
            name="neighborhood"
            control={control}
            render={({ field }) => (
              <TextField
                {...field}
                label="Bairro"
                error={!!errors.neighborhood}
                helperText={errors.neighborhood?.message}
                fullWidth
              />
            )}
          />
        </Grid>
        <Grid size={{ xs: 12 }}>
          <Divider>Observações</Divider>
        </Grid>
        <Grid size={{ xs: 12, sm: 12 }}>
          <Controller
            name="note"
            control={control}
            render={({ field }) => (
              <TextField
                {...field}
                label="Observação"
                multiline
                maxRows={4}
                error={!!errors.note}
                helperText={errors.note?.message}
                fullWidth
              />
            )}
          />
        </Grid>
        <Grid size={{ xs: 12 }}>
          <Button type="submit" variant="contained" fullWidth>
            {watch("id") ? "Atualizar" : "Cadastrar"}
          </Button>
        </Grid>
      </Grid>
      <Grid container spacing={2} size={{ xs: 12 }} mt={2}>
        <Grid size={{ xs: 12 }}>
          <Divider>Informações de Contato de Emergência</Divider>
        </Grid>
        {watch("emergencyContacts", []).map((emergencyContact, index: number) => (
          <>
            <Grid size={{ xs: 12 }} key={index}>
              <EmergencyContactFormPage emergencyContact={emergencyContact} />
            </Grid>
            <Grid size={{ xs: 12 }} display={{ xs: "block", md: "none" }}>
              <Divider />
            </Grid>
          </>
        ))}
        <Grid size={{ xs: 12 }}>
          <EmergencyContactFormPage emergencyContact={{ peopleId: watch("id")!, name: "", phone: "" }} />
        </Grid>
      </Grid>
      <Grid container spacing={2} size={{ xs: 12 }} mt={2}>
        <Grid size={{ xs: 12 }}>
          <Divider>Registro de Entradas/Saídas</Divider>
        </Grid>
        {watch("tours", []).map((tour, index: number) => (
          <>
            <Grid size={{ xs: 12 }} key={index}>
              <TourFormPage tour={tour} />
            </Grid>
            <Grid size={{ xs: 12 }} display={{ xs: "block", md: "none" }}>
              <Divider />
            </Grid>
          </>
        ))}
        <Grid size={{ xs: 12 }}>
          <TourFormPage tour={{ peopleId: Number(watch("id")) } as tour} />
        </Grid>
      </Grid>
    </>
  );
};
