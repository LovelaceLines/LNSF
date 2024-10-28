import { getFilteredObject, useTable } from "@/tables";
import { useUserStore } from "@/store";

export const useUserTablePage = () => {
	const { getUsers, queryResult, users } = useUserStore();
	const { state, ...restTablePros } = useTable();

	const onSubmit = () => getUsers(getFilteredObject({ state }));

	const rowCount = queryResult.totalCount;

	return {
		users,
		state,
		rowCount,
		...restTablePros,
		onSubmit,
	};
};
