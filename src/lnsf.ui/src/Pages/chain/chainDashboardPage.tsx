import { Box, Card, CardContent, CardHeader, Grid2 as Grid, TextField, Typography } from "@mui/material";
import { PieChart } from "@mui/x-charts/PieChart";
import { MRT_ColumnDef } from "material-react-table";
import { useMemo } from "react";

import { DateField } from "@/components";
import { typeTreatmentCount } from "@/store";
import { useMaterialReactTable } from "@/tables";
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
	const { isMobile } = useThemeContext();

	const typeTreatmentcolumns = useMemo<MRT_ColumnDef<typeTreatmentCount>[]>(
		() => [
			{
				accessorKey: "name",
				header: "Nome",
				size: 100,
			},
			{
				accessorKey: "type",
				header: "Tipo",
				size: 50,
			},
			{
				accessorKey: "totalCount",
				header: "Total",
				size: 50,
			},
		],
		[]
	);

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
		<Grid container spacing={2}>
			<Grid size={{ xs: 12, sm: 6, md: 3 }}>
				<Card variant="elevation" raised>
					<CardHeader
						title="Pessoas hospedadas"
						subheader="Quantidade de pessoas hospedadas no dia"
					/>
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
				</Card>
			</Grid>
			<Grid size={{ xs: 12, sm: 6, md: 3 }}>
				<Card variant="elevation" raised>
					<CardHeader
						title="Pessoas a caminho"
						subheader="Quantidade de pessoas que irão hospedar"
					/>
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
				</Card>
			</Grid>
			<Grid size={{ xs: 12, md: 4 }}>
				<Card variant="elevation" raised>
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
					<CardContent>
						<>
							{useMaterialReactTable({
								columns: typeTreatmentcolumns,
								data: typeTreatmentCount,
								title: "Tratamentos",
								initialState: {
									sorting: [{ id: "people.birthDate", desc: false }],
									showColumnFilters: false,
								},
								renderBottomToolbar: () => <></>,
								renderTopToolbar: () => <></>,
							})}
						</>
					</CardContent>
				</Card>
			</Grid>
			<Grid size={{ xs: 12, md: 4 }}>
				<Card variant="elevation" raised>
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
						<>
							{useMaterialReactTable({
								columns: peopleWillBirthdateColumns,
								data: peopleWillBirthdate,
								title: "Aniversariantes",
								initialState: { showColumnFilters: false },
								renderBottomToolbar: () => <></>,
								renderTopToolbar: () => <></>,
							})}
						</>
					</CardContent>
				</Card>
			</Grid>
			<Grid size={{ xs: 12, sm: 7, md: 4 }}>
				<Card variant="elevation" raised>
					<CardHeader title="Gênero" subheader="Quantidade de pessoas ativas por gênero" />
					<CardContent>
						<PieChart
							series={[
								{
									data: genderChartCctivePeople,
									arcLabel: "value",
								},
							]}
							width={300}
							height={150}
						/>
					</CardContent>
				</Card>
			</Grid>
		</Grid>
	);
};
