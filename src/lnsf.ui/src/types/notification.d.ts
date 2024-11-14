export type notification = {
	id?: number;
	title: string;
	content: string;
	createdAt: string;
};

export type notificationUser = {
	readAt: string;
	notificationId: number;
	notification?: notification;
	userId: number;
	user?: user;
};
