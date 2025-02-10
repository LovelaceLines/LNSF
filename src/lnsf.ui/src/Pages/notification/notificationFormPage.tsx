import { Button, Divider, Grid2 as Grid, TextField } from "@mui/material";

import { useNotificationFormPage } from "./useNotificationFormPage";
import { DateTimeField } from "@/components";
import { Controller } from "react-hook-form";
import { dateTimeToStr } from "@/utils";
import { useThemeContext } from "@/theme";

export const NotificationFormPage = () => {
  const { handleSave, control, errors, handleSubmit, register, watch } = useNotificationFormPage();
  const { isMobile } = useThemeContext();

  console.debug("NotificationFormPage", watch());

  return (
    <Grid container spacing={2} component="form" onSubmit={handleSubmit(handleSave)}>
      <Grid size={{ xs: 12 }}>
        <Divider>Dados da Notificação</Divider>
      </Grid>
      <Grid size={{ xs: 12, sm: 1.5, md: 1 }}>
        <Controller
          name="id"
          control={control}
          render={({ field }) => (
            <TextField
              label="Id"
              disabled
              {...field}
              error={!!errors.id}
              helperText={errors.id?.message}
              fullWidth
            />
          )}
        />
      </Grid>
      <Grid size={{ xs: 12, sm: 10.5, md: 5 }}>
        <Controller
          name="title"
          control={control}
          render={({ field }) => (
            <TextField
              label="Título"
              multiline
              maxRows={isMobile ? 3 : 1}
              {...field}
              error={!!errors.title}
              helperText={errors.title?.message}
              fullWidth
            />
          )}
        />
      </Grid>
      <Grid size={{ xs: 6, sm: 6, md: 3 }}>
        <DateTimeField
          label="Válido de"
          value={dateTimeToStr(watch("validFrom"))}
          register={register("validFrom")}
          error={!!errors.validFrom}
          helperText={errors.validFrom?.message}
        />
      </Grid>
      <Grid size={{ xs: 6, sm: 6, md: 3 }}>
        <DateTimeField
          label="Válido até"
          value={dateTimeToStr(watch("expiredAt"))}
          register={register("expiredAt")}
          error={!!errors.expiredAt}
          helperText={errors.expiredAt?.message}
        />
      </Grid>
      <Grid size={12}>
        <Controller
          name="content"
          control={control}
          render={({ field }) => (
            <TextField
              label="Conteúdo"
              multiline
              rows={4}
              {...field}
              error={!!errors.content}
              helperText={errors.content?.message}
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
