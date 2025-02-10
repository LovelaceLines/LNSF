import { Add, Delete, Edit } from "@mui/icons-material";
import { Grid2 as Grid, IconButton, TextField } from "@mui/material";
import { Controller } from "react-hook-form";

import { emergencyContact } from "@/types";
import { useEmergencyContactFormPage } from "./useEmergencyContactFormPage";

export const EmergencyContactFormPage = ({ emergencyContact }: { emergencyContact?: emergencyContact }) => {
  const { handleSave, deleteEmergencyContact, control, errors, getValues, handleSubmit, register, watch } =
    useEmergencyContactFormPage({
      emergencyContact,
    });

  return (
    <Grid container spacing={2} alignItems="center" component="form" onSubmit={handleSubmit(handleSave)}>
      <Grid size={{ xs: 6, sm: 1.5, md: 1 }}>
        <TextField
          label="Id Pessoa"
          disabled
          fullWidth
          {...register("peopleId")}
          error={!!errors.peopleId}
          helperText={errors.peopleId?.message}
        />
      </Grid>
      <Grid size={{ xs: 6, sm: 1.5, md: 1 }}>
        <Controller
          name="id"
          control={control}
          render={({ field }) => (
            <TextField
              {...field}
              label="Id"
              disabled
              error={!!errors.id}
              helperText={errors.id?.message}
              fullWidth
            />
          )}
        />
      </Grid>
      <Grid size={{ xs: 12, sm: 4, md: 6 }}>
        <TextField
          label="Nome do Contato"
          {...register("name")}
          error={!!errors.name}
          helperText={errors.name?.message}
          fullWidth
        />
      </Grid>
      <Grid size="grow">
        <TextField
          label="Telefone do Contato"
          {...register("phone")}
          error={!!errors.phone}
          helperText={errors.phone?.message}
          fullWidth
        />
      </Grid>
      <Grid direction="row" spacing={2} wrap="nowrap">
        <IconButton type="submit" color={watch("id") ? "warning" : "info"} size="large">
          {watch("id") ? <Edit /> : <Add />}
        </IconButton>
        {watch("id") ? (
          <IconButton color="error" size="large" onClick={() => deleteEmergencyContact(getValues("id")!)}>
            <Delete />
          </IconButton>
        ) : null}
      </Grid>
    </Grid>
  );
};
