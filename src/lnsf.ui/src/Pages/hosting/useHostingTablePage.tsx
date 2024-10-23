import { getFilteredObject, useTable } from "@/tables";
import { useHostingStore } from "@/zustand";

export const useHostingTablePage = () => {
	const { getHostings, hostings, queryResult } = useHostingStore();
	const { state, ...restTablePros } = useTable();

	const onSubmit = () => getHostings(getFilteredObject({ state }));

	const rowCount = queryResult.totalCount;

	return {
		hostings,
		state,
		rowCount,
		...restTablePros,
		onSubmit,
	};
};
