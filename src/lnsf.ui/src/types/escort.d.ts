import { people } from "./people";

export type escort = {
	id?: number;
	peopleId: number;
	people?: people;
};
