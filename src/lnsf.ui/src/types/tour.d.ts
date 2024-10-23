import { baseFilter } from "./baseFilter";
import { people } from "./people";

export type tour = {
	id?: number;
	output?: string;
	input?: string;
	note: string;
	peopleId: number;
	people: people;
};

export type tourFilter = baseFilter & {
	id?: number;
	output?: Date;
	input?: Date;
	open?: boolean;
	close?: boolean;
	note?: string;
	peopleId?: number;
};
