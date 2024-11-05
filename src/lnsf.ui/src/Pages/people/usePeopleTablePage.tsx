import { useForm } from "react-hook-form";

import { getFilteredObject, useTableState } from "@/tables";
import { peopleFilter } from "@/types";
import { usePeopleStore } from "@/store";

export const usePeopleTablePage = () => {
	const { getPeoples, peoples, queryResult } = usePeopleStore();
	const { state } = useTableState();

	const { register, getValues, watch, setValue } = useForm<peopleFilter>({
		values: { isActive: undefined, isEscort: undefined, isPatient: undefined, isVeteran: undefined },
	});

	const onSubmit = () =>
		getPeoples({
			...getValues(),
			...getFilteredObject({ state: state.people }),
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
		rowCount,
		onSubmit,
	};
};
