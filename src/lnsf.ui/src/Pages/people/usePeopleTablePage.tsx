import { useForm } from "react-hook-form";

import { getFilteredObject, useTable } from "@/tables";
import { peopleFilter } from "@/types";
import { usePeopleStore } from "@/zustand";

export const usePeopleTablePage = () => {
	const { getPeoples, peoples, queryResult } = usePeopleStore();
	const { state, ...restTablePros } = useTable();

	const { register, getValues, watch, setValue } = useForm<peopleFilter>({});

	const onSubmit = () => {
		console.log("aquii", getFilteredObject({ state }));
		getPeoples({ ...getValues(), ...getFilteredObject({ state }) });
	};

	const rowCount = queryResult.totalCount;

	return {
		peoples,
		register,
		getValues,
		watch,
		setValue,
		state,
		rowCount,
		...restTablePros,
		onSubmit,
	};
};
