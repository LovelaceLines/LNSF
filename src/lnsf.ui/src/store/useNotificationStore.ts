import { create } from "zustand";

import { Axios } from "@/http";
import { notification, notificationUser } from "@/types";

type state = {
	count: number;
	notifications: notification[];

	getNotifications: () => Promise<notification[]>;
	getUnreadCount: () => Promise<number>;
	postMarkAsRead: (notificationId: number) => Promise<notificationUser>;
	postNotification: (notification: notification) => Promise<notification>;
	putNotification: (notification: notification) => Promise<notification>;
	deleteNotification: (id: number) => Promise<notification>;
};

export const useNotificationStore = create<state>((set) => ({
	count: 0,
	notifications: [],

	getNotifications: async (): Promise<notification[]> => {
		const res = await Axios.get<notification[]>("/Notification");
		set({ notifications: res.data });
		return res.data;
	},

	getUnreadCount: async (): Promise<number> => {
		const res = await Axios.get<number>("/Notification/unread-count");
		set({ count: res.data });
		return res.data;
	},

	postMarkAsRead: async (notificationId: number): Promise<notificationUser> => {
		const res = await Axios.post<notificationUser>("/Notification/mark-as-read", {
			notificationId: notificationId,
			userId: 0,
			readAt: new Date().toISOString(),
		} as notificationUser);
		const notificationUser = res.data;
		set((state) => ({ count: state.count - 1 }));
		set((state) => ({
			notifications: state.notifications.filter((n) => n.id !== notificationUser.notificationId),
		}));
		return notificationUser;
	},

	postNotification: async (notification) => {
		const res = await Axios.post<notification>("/Notification", notification);
		set((state) => ({ count: state.count + 1 }));
		set((state) => ({ notifications: [...state.notifications, res.data] }));
		return res.data;
	},

	putNotification: async (notification) => {
		const res = await Axios.put<notification>("/Notification", notification);
		set((state) => ({
			notifications: state.notifications.map((n) => (n.id === notification.id ? res.data : n)),
		}));
		return res.data;
	},

	deleteNotification: async (id: number): Promise<notification> => {
		const res = await Axios.delete<notification>(`/Notification/${id}`);
		set((state) => ({ count: state.count - 1 }));
		set((state) => ({ notifications: state.notifications.filter((n) => n.id !== id) }));
		return res.data;
	},
}));
