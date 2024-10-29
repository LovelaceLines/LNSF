import { getFilteredObject, useTable } from "@/tables";
import { tourFilter } from "@/types";
import { useTourStore } from "@/store";
import { useForm } from "react-hook-form";

export const useTourTablePage = () => {
	const { getTours, tours, queryResult } = useTourStore();
	const { state, ...restTablePros } = useTable();

	const { register, getValues, watch, setValue } = useForm<tourFilter>();

	const onSubmit = () =>
		getTours({
			...getFilteredObject({ state }),
			...getValues(),
			isClose: getValues("isClose") || undefined,
			isOpen: getValues("isOpen") || undefined,
		});

	const rowCount = queryResult.totalCount;

	return {
		tours,
		state,
		rowCount,
		...restTablePros,
		register,
		getValues,
		watch,
		setValue,
		onSubmit,
	};
};
