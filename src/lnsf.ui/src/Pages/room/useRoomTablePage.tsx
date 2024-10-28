import { getFilteredObject, useTable } from "@/tables";
import { useRoomStore } from "@/store";
import { roomFilter } from "@/types";
import { useForm } from "react-hook-form";

export const useRoomTablePage = () => {
	const { register, getValues, watch } = useForm<roomFilter>({ values: {} });
	const { getRooms, rooms, queryResult } = useRoomStore();
	const { state, ...restTablePros } = useTable();

	const onSubmit = () =>
		getRooms({
			...getFilteredObject({ state }),
			...getValues(),
			isAvailable: getValues("isAvailable") || undefined,
		});

	const rowCount = queryResult.totalCount;

	return {
		rooms,
		state,
		rowCount,
		...restTablePros,
		onSubmit,
		getValues,
		register,
		watch,
	};
};
