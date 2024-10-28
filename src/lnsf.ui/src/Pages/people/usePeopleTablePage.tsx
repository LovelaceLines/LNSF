import { useForm } from "react-hook-form";

import { getFilteredObject, useTable } from "@/tables";
import { peopleFilter } from "@/types";
import { usePeopleStore } from "@/store";

export const usePeopleTablePage = () => {
	const { getPeoples, peoples, queryResult } = usePeopleStore();
	const { state, ...restTablePros } = useTable();

	const { register, getValues, watch, setValue } = useForm<peopleFilter>({
		values: { isActive: undefined, isEscort: undefined, isPatient: undefined, isVeteran: undefined },
	});

	const onSubmit = () =>
		getPeoples({
			...getValues(),
			...getFilteredObject({ state }),
			isActive: getValues("isActive") || undefined,
			isEscort: getValues("isEscort") || undefined,
			isPatient: getValues("isPatient") || undefined,
			willHosted: getValues("willHosted") || undefined,
		});

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
