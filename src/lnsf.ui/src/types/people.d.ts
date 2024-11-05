import { baseFilter } from "./baseFilter";
import { emergencyContact } from "./emergencyContact";
import { hosting } from "./hosting";
import { room } from "./room";
import { tour } from "./tour";

export type people = {
	id?: number;
	name: string;
	gender: gender;
	birthDate: string;
	maritalStatus: maritalStatus;
	raceColor: raceColor;
	email?: string;
	rg: string;
	issuingBody: string;
	cpf: string;
	street: string;
	houseNumber: string;
	neighborhood: string;
	city: string;
	state: string;
	phone?: string;
	note?: string;
	experience?: string;
	status?: string;
	tours: tour[];
	emergencyContacts: emergencyContact[];
};

export type peopleFilter = baseFilter & {
	id?: number;
	name?: string;
	rg?: string;
	issuingBody?: string;
	cpf?: string;
	phone?: string;
	gender?: gender;
	birthDate?: string;
	street?: string;
	houseNumber?: string;
	neighborhood?: string;
	city?: string;
	state?: string;
	note?: string;
	isActive?: boolean;
	isEscort?: boolean;
	isPatient?: boolean;
	isVeteran?: boolean;
	willHosted?: boolean;
};

export type peopleRoomHosting = {
	id?: number;
	hostingId: number;
	hosting?: hosting;
	peopleId: number;
	people?: people;
	roomId: number;
	room?: room;
};

export type peopleRoomHostingFilter = baseFilter & {
	hostingId?: number;
	peopleId?: number;
	roomId?: number;
	checkIn?: Date;
	checkOut?: Date;
	active?: boolean;
};

export enum gender {
	male = 0,
	female = 1,
	other = 2,
}

export enum raceColor {
	white,
	black,
	brown,
	yellow,
	indigenous,
	ignored,
}

export enum maritalStatus {
	single,
	married,
	separate,
	divorced,
	stableUnion,
	widower,
}
