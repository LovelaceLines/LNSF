import { getFilteredObject, useTable } from "@/tables";
import { usePatientStore } from "@/store";

export const usePatientTablePage = () => {
	const { getPatients, patients, queryResult } = usePatientStore();
	const { state, ...restTablePros } = useTable();

	const onSubmit = () => getPatients(getFilteredObject({ state }));

	const rowCount = queryResult.totalCount;

	return {
		patients,
		state,
		rowCount,
		...restTablePros,
		onSubmit,
	};
};
