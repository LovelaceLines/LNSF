import { Box, Card, CardContent, CardHeader, Paper, TextField, Typography } from "@mui/material";
import { BarChart } from "@mui/x-charts/BarChart";
import { PieChart } from "@mui/x-charts/PieChart";
import { MRT_ColumnDef, MaterialReactTable, useMaterialReactTable } from "material-react-table";
import { useMemo } from "react";
import { Masonry } from "@mui/lab";

import { DateField } from "@/components";
import { useChainDashboardPage } from "./useChainDashboardPage";
import { peopleRoomHosting } from "@/types";
import { dateOnlyToStr, dateTimeToStr } from "@/utils";
import { useThemeContext } from "@/theme";

export const ChainDashboardPage = () => {
  const {
    peopleHosted,
    peopleWillHosted,
    peopleWillBirthdate,
    genderChartCctivePeople,
    typeTreatmentCount,
    errors,
    register,
  } = useChainDashboardPage();
  const { isMobile, isTablet } = useThemeContext();

  const peopleWillBirthdateColumns = useMemo<MRT_ColumnDef<peopleRoomHosting>[]>(
    () => [
      {
        accessorKey: "people.id",
        header: "Id",
        size: 75,
      },
      {
        accessorKey: "people.name",
        header: "Nome",
      },
      {
        accessorKey: "people.birthDate",
        header: "Data de aniversário",
        Cell: ({ row }) => `${dateOnlyToStr(row.original.people?.birthDate, "ptBr")}`,
      },
      {
        accessorKey: "hosting.checkIn",
        header: "Check-in",
        Cell: ({ row }) => dateTimeToStr(row.original.hosting?.checkIn, "ptBr"),
      },
      {
        accessorKey: "hosting.checkOut",
        header: "Check-out",
        Cell: ({ row }) => dateTimeToStr(row.original.hosting?.checkOut, "ptBr"),
      },
    ],
    []
  );

  return (
    <>
      <Masonry columns={isMobile ? 1 : isTablet ? 2 : 3} spacing={isMobile ? 1 : 2} sx={{ m: 0 }}>
        {[
          <Card variant="elevation" raised key="peopleWillHosted">
            <CardHeader title="Pessoas a caminho" subheader="Quantidade de pessoas que irão hospedar" />
            <CardContent>
              <TextField
                label="Dias até o check-in"
                type="number"
                {...register("daysToCheck")}
                error={!!errors.daysToCheck}
                helperText={errors.daysToCheck?.message}
                fullWidth
              />
            </CardContent>
            <CardContent>
              <Typography variant="h6">{peopleWillHosted.length} pessoas</Typography>
            </CardContent>
          </Card>,
          <Card variant="elevation" raised key="peopleHosted">
            <CardHeader title="Pessoas hospedadas" subheader="Quantidade de pessoas hospedadas no dia" />
            <CardContent>
              <DateField
                label="Data"
                register={register("date")}
                error={!!errors.date}
                helperText={errors.date?.message}
              />
            </CardContent>
            <CardContent>
              <Typography variant="h6">{peopleHosted.count} pessoas hospedadas</Typography>
            </CardContent>
          </Card>,
          <Card variant="elevation" raised key="peopleWillBirthdate">
            <CardHeader title="Aniversariantes" subheader="Próximos aniversariantes" />
            <CardContent>
              <TextField
                label="Dias até o aniversário"
                type="number"
                {...register("daysToBirthdate")}
                error={!!errors.daysToBirthdate}
                helperText={errors.daysToBirthdate?.message}
                fullWidth
              />
            </CardContent>
            <CardContent>
              <MaterialReactTable
                table={useMaterialReactTable({
                  columns: peopleWillBirthdateColumns,
                  data: peopleWillBirthdate,
                  initialState: {
                    showColumnFilters: false,
                  },
                  renderBottomToolbar: () => <></>,
                  renderTopToolbar: () => <></>,
                })}
              />
            </CardContent>
          </Card>,
          <Card variant="elevation" raised key="typeTreatmentCount">
            <CardHeader title="Tratamentos" subheader="Quantidade de tratamentos por período" />
            <CardContent
              component={Box}
              display="flex"
              flexDirection="row"
              justifyContent="space-between"
              gap={2}
            >
              <DateField
                label="Check-in"
                register={register("checkIn")}
                error={!!errors.checkIn}
                helperText={errors.checkIn?.message}
              />
              <DateField
                label="Check-out"
                register={register("checkOut")}
                error={!!errors.checkOut}
                helperText={errors.checkOut?.message}
              />
            </CardContent>
            <CardContent sx={{ display: "flex", justifyContent: "center" }}>
              <BarChart
                xAxis={[{ data: [""], scaleType: "band" }]}
                series={typeTreatmentCount.map((item) => {
                  return {
                    data: [item.totalCount],
                    label: item.name.slice(0, 10),
                  };
                })}
                barLabel="value"
                width={400}
                height={300}
                slotProps={{
                  loadingOverlay: { message: "Carregando..." },
                  noDataOverlay: { message: "Sem dados" },
                }}
              />
            </CardContent>
          </Card>,
          <Card variant="elevation" raised key="genderChartCctivePeople">
            <CardHeader title="Gênero" subheader="Quantidade de pessoas ativas por gênero" />
            <CardContent sx={{ display: "flex", justifyContent: "center" }}>
              <PieChart
                series={[
                  {
                    data: genderChartCctivePeople,
                    arcLabel: "value",
                  },
                ]}
                width={325}
                height={150}
                slotProps={{
                  loadingOverlay: { message: "Carregando..." },
                  noDataOverlay: { message: "Sem dados" },
                }}
              />
            </CardContent>
          </Card>,
        ].map((item, index) => (
          <Paper key={index}>{item}</Paper>
        ))}
      </Masonry>
    </>
  );
};
