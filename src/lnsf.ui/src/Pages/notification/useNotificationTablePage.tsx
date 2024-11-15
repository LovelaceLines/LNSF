import { getFilteredObject, useTableState } from "@/tables";
import { useNotificationStore } from "@/store";

export const useNotificationTablePage = () => {
	const { getNotifications, queryResult } = useNotificationStore();
	const { state } = useTableState();

	const onSubmit = () => getNotifications(getFilteredObject({ state: state.notification }));

	return {
		queryResult,
		onSubmit,
	};
};
