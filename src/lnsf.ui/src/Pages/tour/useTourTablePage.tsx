import { getFilteredObject, useTableState } from "@/tables";
import { tourFilter } from "@/types";
import { useTourStore } from "@/store";
import { useForm } from "react-hook-form";

export const useTourTablePage = () => {
	const { getTours, tours, queryResult } = useTourStore();
	const { state } = useTableState();

	const { register, getValues, watch, setValue } = useForm<tourFilter>();

	const onSubmit = () =>
		getTours({
			...getFilteredObject({ state: state.tour }),
			...getValues(),
			isClose: getValues("isClose") || undefined,
			isOpen: getValues("isOpen") || undefined,
		});

	const rowCount = queryResult.totalCount;

	return {
		tours,
		rowCount,
		register,
		getValues,
		watch,
		setValue,
		onSubmit,
	};
};
