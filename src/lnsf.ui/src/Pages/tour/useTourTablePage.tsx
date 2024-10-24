import { getFilteredObject, useTable } from "@/tables";
import { tourFilter } from "@/types";
import { useTourStore } from "@/zustand";
import { useForm } from "react-hook-form";

export const useTourTablePage = () => {
	const { getTours, tours, queryResult } = useTourStore();
	const { state, ...restTablePros } = useTable();

	const { register, getValues, watch, setValue } = useForm<tourFilter>();

	const onSubmit = () => getTours({ ...getFilteredObject({ state }), ...getValues() });

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
