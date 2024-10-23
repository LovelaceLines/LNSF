import { baseFilter } from "./baseFilter";

export type user = {
	id?: number;
	name: string;
	userName: string;
	email: string;
	phoneNumber: string;
	password?: string;
	roles: role[];
};

export type userFilter = baseFilter & {
	id?: number;
	name?: string;
	userName?: string;
	email?: string;
	phoneNumber?: string;
};

export type defaultRole = "Desenvolvedor" | "Administrador" | "Assistente Social" | "Secretário" | "Voluntário";

export type role = {
	id?: number;
	name: defaultRole | string;
};

export type roleFilter = baseFilter & {
	id?: number;
	name?: string;
};

export type userRole = {
	userId: number;
	user?: user;
	roleId: number;
	role?: role;
};
