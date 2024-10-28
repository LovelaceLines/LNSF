import { getFilteredObject, useTable } from "@/tables";
import { usePeopleRoomHostingStore } from "@/store";

export const usePeopleRoomHostingTablePage = () => {
	const { getPeopleRoomHosting, prh, queryResult } = usePeopleRoomHostingStore();
	const { state, ...restTablePros } = useTable();

	const rowCount = queryResult.totalCount;

	const onSubmit = () => getPeopleRoomHosting(getFilteredObject({ state }));

	return {
		prh,
		state,
		rowCount,
		...restTablePros,
		onSubmit,
	};
};
