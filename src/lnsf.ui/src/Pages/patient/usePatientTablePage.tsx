import { getFilteredObject, useTableState } from "@/tables";
import { usePatientStore } from "@/store";

export const usePatientTablePage = () => {
	const { getPatients, patients, queryResult } = usePatientStore();
	const { state } = useTableState();

	const onSubmit = () => getPatients(getFilteredObject({ state: state.patient }));

	const rowCount = queryResult.totalCount;

	return {
		patients,
		state,
		rowCount,
		onSubmit,
	};
};
