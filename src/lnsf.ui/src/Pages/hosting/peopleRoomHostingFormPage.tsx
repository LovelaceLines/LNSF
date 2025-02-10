import { peopleRoomHosting } from "@/types";
import { usePeopleRoomHostingFormPage } from "./usePeopleRoomHostingFormPage";
import { Grid2 as Grid, IconButton, TextField } from "@mui/material";
import { Add, Delete } from "@mui/icons-material";

interface PeopleRoomHostingFormPageProps {
  prh: peopleRoomHosting;
  mode: "add" | "delete";
}

export const PeopleRoomHostingFormPage = ({ prh, mode }: PeopleRoomHostingFormPageProps) => {
  const { addPeopleToRoom, removePeopleFromRoom, register, getValues } = usePeopleRoomHostingFormPage({
    prh,
  });

  return (
    <Grid container spacing={2} alignItems="center">
      <Grid size={{ xs: 2, sm: 2, md: 1 }}>
        <TextField
          label="Id Pessoa"
          {...register("peopleId")}
          fullWidth
          slotProps={{ input: { readOnly: true } }}
        />
      </Grid>
      <Grid size={{ xs: 8, sm: 10, md: 4 }}>
        <TextField
          label="Nome"
          {...register("people.name")}
          fullWidth
          slotProps={{ input: { readOnly: true } }}
        />
      </Grid>
      <Grid size={{ xs: 2, sm: 2, md: 1 }}>
        <TextField
          label="Id Apartamento"
          {...register("roomId")}
          fullWidth
          slotProps={{ input: { readOnly: true } }}
        />
      </Grid>
      <Grid size={{ xs: 8, sm: 2, md: 1 }}>
        <TextField
          label="Número Apartamento"
          {...register("room.number")}
          fullWidth
          InputProps={{ readOnly: true }}
        />
      </Grid>
      <Grid size={{ xs: 2, sm: 2, md: 1 }}>
        <TextField
          label="Id Hospedagem"
          {...register("hostingId")}
          fullWidth
          InputProps={{ readOnly: true }}
        />
      </Grid>
      <Grid>
        {mode === "add" ? (
          <IconButton
            type="submit"
            color="info"
            size="large"
            onClick={() =>
              addPeopleToRoom({
                peopleId: getValues("peopleId"),
                roomId: getValues("roomId"),
                hostingId: getValues("hostingId"),
              })
            }
          >
            <Add />
          </IconButton>
        ) : (
          <IconButton
            color="error"
            size="large"
            onClick={() =>
              removePeopleFromRoom({
                peopleId: getValues("peopleId"),
                roomId: getValues("roomId"),
                hostingId: getValues("hostingId"),
              })
            }
          >
            <Delete />
          </IconButton>
        )}
      </Grid>
    </Grid>
  );
};
