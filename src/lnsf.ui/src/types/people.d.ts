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
	birthDate?: Date;
	street?: string;
	houseNumber?: string;
	neighborhood?: string;
	city?: string;
	state?: string;
	note?: string;
};

export type peopleRoomHosting = {
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

export const getGender = (): { id: string; value: string }[] => [
	{ id: gender.male.toString(), value: "Masculino" },
	{ id: gender.female.toString(), value: "Feminino" },
	{ id: gender.other.toString(), value: "Outro" },
];

export enum raceColor {
	white,
	black,
	brown,
	yellow,
	indigenous,
	ignored,
}

export const getRaceColor = (): { id: string; value: string }[] => [
	{ id: raceColor.white.toString(), value: "Branco" },
	{ id: raceColor.black.toString(), value: "Preto" },
	{ id: raceColor.brown.toString(), value: "Pardo" },
	{ id: raceColor.yellow.toString(), value: "Amarelo" },
	{ id: raceColor.indigenous.toString(), value: "Indígena" },
	{ id: raceColor.ignored.toString(), value: "Não declarado" },
];

export enum maritalStatus {
	single,
	married,
	separate,
	divorced,
	stableUnion,
	widower,
}

export const getMaritalStatus = (): { id: string; value: string }[] => [
	{ id: maritalStatus.single.toString(), value: "Solteiro(a)" },
	{ id: maritalStatus.married.toString(), value: "Casado(a)" },
	{ id: maritalStatus.separate.toString(), value: "Separado(a)" },
	{ id: maritalStatus.divorced.toString(), value: "Divorciado(a)" },
	{ id: maritalStatus.stableUnion.toString(), value: "União Estável" },
	{ id: maritalStatus.widower.toString(), value: "Viúvo(a)" },
];
