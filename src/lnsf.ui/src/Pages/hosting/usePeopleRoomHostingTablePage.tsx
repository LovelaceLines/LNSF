import { getFilteredObject, useTableState } from "@/tables";
import { usePeopleRoomHostingStore } from "@/store";

export const usePeopleRoomHostingTablePage = () => {
	const { getPeopleRoomHosting, prh, queryResult } = usePeopleRoomHostingStore();
	const { state } = useTableState();

	const rowCount = queryResult.totalCount;

	const onSubmit = () => getPeopleRoomHosting(getFilteredObject({ state: state.peopleRoomHosting }));

	return {
		prh,
		rowCount,
		onSubmit,
	};
};
