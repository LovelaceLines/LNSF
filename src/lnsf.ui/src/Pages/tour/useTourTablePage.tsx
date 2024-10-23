import { getFilteredObject, useTable } from "@/tables";
import { useTourStore } from "@/zustand";

export const useTourTablePage = () => {
	const { getTours, tours, queryResult } = useTourStore();
	const { state, ...restTablePros } = useTable();

	const onSubmit = () => getTours(getFilteredObject({ state }));

	const rowCount = queryResult.totalCount;

	return {
		tours,
		state,
		rowCount,
		...restTablePros,
		onSubmit,
	};
};
