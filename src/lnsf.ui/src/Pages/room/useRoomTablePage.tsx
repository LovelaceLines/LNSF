import { getFilteredObject, useTable } from "@/tables";
import { useRoomStore } from "@/zustand";

export const useRoomTablePage = () => {
	const { getRooms, rooms, queryResult } = useRoomStore();
	const { state, ...restTablePros } = useTable();

	const onSubmit = () => getRooms(getFilteredObject({ state }));

	const rowCount = queryResult.totalCount;

	return {
		rooms,
		state,
		rowCount,
		...restTablePros,
		onSubmit,
	};
};
