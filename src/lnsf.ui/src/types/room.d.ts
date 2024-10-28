import { baseFilter } from "./baseFilter";

export type room = {
	id: number;
	number: string;
	bathroom: boolean;
	beds: number;
	storey: number;
	available: boolean;
};

export type roomFilter = baseFilter & {
	id?: number;
	number?: string;
	bathroom?: boolean;
	beds?: number;
	storey?: number;
	available?: boolean;
	isAvailable?: boolean;
};
