import { Divider, Grid2 as Grid, TextField, Typography } from "@mui/material";

import { SelectField } from "@/components";
import { TourFormPage } from "./tourFormPage";
import { tour } from "@/types";
import { useTourDailyLogPage } from "./useTourDailyLogPage";

export const TourDailyLogPage = () => {
  const { people, setPeople, peoples, openTours, handleSelect } = useTourDailyLogPage();

  return (
    <Grid container direction="column" spacing={2}>
      <Grid size={{ xs: 12 }}>
        <Divider>Registro Diário</Divider>
      </Grid>
      <Grid container size={{ xs: 12, md: 6 }}>
        <Grid size={{ xs: 4, md: 2 }}>
          <TextField
            label="Id Pessoa"
            type="number"
            defaultValue={0}
            value={people?.id}
            fullWidth
            onChange={(e) => setPeople(peoples.find((p) => p.id === +e.target.value))}
          />
        </Grid>
        <Grid size="grow" wrap="nowrap">
          <SelectField
            label="Buscar Pessoa"
            options={peoples}
            valueKey="id"
            labelId="id"
            labelKey="name"
            defaultValue={String(people?.id)}
            onClick={handleSelect}
          />
        </Grid>
      </Grid>
      {people && (
        <>
          <Grid>
            <Typography variant="subtitle2" color="warning">
              {people.name} - {people.note}
            </Typography>
          </Grid>
          <Grid size={{ xs: 12 }}>
            <TourFormPage tour={{ peopleId: people.id! } as tour} />
          </Grid>
          <Grid size={{ xs: 12 }}>
            <Divider />
          </Grid>
        </>
      )}
      {openTours.map((tour) => (
        <>
          <Grid>
            <Typography variant="subtitle2">
              {tour.people?.name} - {tour.people?.note}
            </Typography>
          </Grid>
          <Grid size={{ xs: 12 }}>
            <TourFormPage tour={tour} />
          </Grid>
          <Grid size={{ xs: 12 }}>
            <Divider />
          </Grid>
        </>
      ))}
    </Grid>
  );
};
