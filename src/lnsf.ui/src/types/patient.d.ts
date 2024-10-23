import { baseFilter } from "./baseFilter";
import { hospital } from "./hospital";
import { people } from "./people";
import { treatment } from "./treatment";

export type patient = {
	id?: number;
	socioeconomicRecord: boolean;
	term: boolean;
	peopleId: number;
	people?: people;
	hospitalId: number;
	hospital?: hospital;
	treatments: treatment[];
};

export type patientFilter = baseFilter & {
	id?: number;
	peopleId?: number;
	hospitalId?: number;
};

export type patientTreatment = {
	patientId: number;
	patient?: patient;
	treatmentId: number;
	treatment?: treatment;
};
