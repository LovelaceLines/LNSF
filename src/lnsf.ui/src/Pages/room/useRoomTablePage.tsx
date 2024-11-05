import { useForm } from "react-hook-form";

import { getFilteredObject, useTableState } from "@/tables";
import { useRoomStore } from "@/store";
import { roomFilter } from "@/types";

export const useRoomTablePage = () => {
	const { register, getValues, watch } = useForm<roomFilter>({ values: {} });
	const { getRooms, rooms, queryResult } = useRoomStore();
	const { state } = useTableState();

	const onSubmit = () =>
		getRooms({
			...getFilteredObject({ state: state.room }),
			...getValues(),
			isAvailable: getValues("isAvailable") || undefined,
		});

	const rowCount = queryResult.totalCount;

	return {
		rooms,
		rowCount,
		onSubmit,
		getValues,
		register,
		watch,
	};
};
