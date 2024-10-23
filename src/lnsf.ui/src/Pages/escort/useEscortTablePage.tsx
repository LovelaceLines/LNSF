import { getFilteredObject, useTable } from "@/tables";
import { useEscortStore } from "@/zustand";

export const useEscortTablePage = () => {
	const { getEscorts, escorts, queryResult } = useEscortStore();
	const { state, ...restTablePros } = useTable();

	const onSubmit = () => getEscorts(getFilteredObject({ state }));

	const rowCount = queryResult.totalCount;

	return {
		escorts,
		state,
		rowCount,
		...restTablePros,
		onSubmit,
	};
};
