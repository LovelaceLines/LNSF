import { getFilteredObject, useTable } from "@/tables";
import { usePeopleStore } from "@/zustand";

export const usePeopleTablePage = () => {
	const { getPeoples, peoples, queryResult } = usePeopleStore();
	const { state, ...restTablePros } = useTable();

	const onSubmit = () => getPeoples(getFilteredObject({ state }));

	const rowCount = queryResult.totalCount;

	return {
		peoples,
		state,
		rowCount,
		...restTablePros,
		onSubmit,
	};
};
