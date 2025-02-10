import { Button, Divider, Grid2 as Grid, TextField } from "@mui/material";

import { useHospitalFormPage } from "./useHospitalFormPage";
import { Controller } from "react-hook-form";

export const HospitalFormPage = () => {
  const { handleSave, control, errors, handleSubmit } = useHospitalFormPage();

  return (
    <Grid container spacing={2} component="form" onSubmit={handleSubmit(handleSave)}>
      <Grid size={{ xs: 12 }}>
        <Divider>Dados do Hospital</Divider>
      </Grid>
      <Grid size={{ xs: 4, sm: 1.5 }}>
        <Controller
          name="id"
          control={control}
          render={({ field }) => (
            <TextField
              label="Id"
              type="number"
              disabled
              {...field}
              error={!!errors.id}
              helperText={errors.id?.message}
              fullWidth
            />
          )}
        />
      </Grid>
      <Grid size={{ xs: 8, sm: 7.5, md: 8.5 }}>
        <Controller
          name="name"
          control={control}
          render={({ field }) => (
            <TextField
              label="Nome"
              {...field}
              error={!!errors.name}
              helperText={errors.name?.message}
              fullWidth
            />
          )}
        />
      </Grid>
      <Grid size={{ xs: 6, sm: 3, md: 2 }}>
        <Controller
          name="acronym"
          control={control}
          render={({ field }) => (
            <TextField
              label="Sigla"
              {...field}
              error={!!errors.acronym}
              helperText={errors.acronym?.message}
              fullWidth
            />
          )}
        />
      </Grid>
      <Grid size={{ xs: 12 }}>
        <Button type="submit" variant="contained" color="primary" fullWidth>
          Salvar
        </Button>
      </Grid>
    </Grid>
  );
};
