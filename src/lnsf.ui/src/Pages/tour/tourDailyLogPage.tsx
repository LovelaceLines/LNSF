import { Divider, Grid2 as Grid, Typography } from "@mui/material";
import { useEffect, useState } from "react";

import { SelectField } from "@/components";
import { TourFormPage } from "./tourFormPage";
import { people, tour } from "@/types";
import { usePeopleStore, useTourStore } from "@/zustand";

// TODO - Fix - Datas não estão sendo exibidas corretamente mas estão sendo salvas corretamente

export const TourDailyLogPage = () => {
	const { getOpenTours, openTours } = useTourStore();
	const [people, setPeople] = useState<people>();
	const { getPeoples, peoples } = usePeopleStore();

	useEffect(() => {
		getOpenTours();
		getPeoples({ isGuest: true, page: 1, pageSize: 999 });
	}, []);

	const handleSelect = (id: number) => setPeople(peoples.find((p) => p.id === id));

	return (
		<Grid container spacing={2}>
			<Grid size={{ xs: 12 }}>
				<SelectField
					label="Buscar Pessoa"
					labelId="id"
					labelKey="name"
					onClick={handleSelect}
					options={peoples}
					valueKey="id"
					key="select-peoples"
				/>
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
							{tour.people.name} - {tour.people.note}
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
