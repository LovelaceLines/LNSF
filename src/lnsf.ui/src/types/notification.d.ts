import { baseFilter } from "./baseFilter";

export type notificationFilter = baseFilter & {
	id?: number;
	title?: string;
	content?: string;
	validFrom?: string;
	expiredAt?: string;
};

export type notification = {
	id?: number;
	title: string;
	content: string;
	validFrom: string;
	expiredAt: string;
};

export type notificationUser = {
	readAt: string;
	notificationId: number;
	notification?: notification;
	userId: number;
	user?: user;
};
