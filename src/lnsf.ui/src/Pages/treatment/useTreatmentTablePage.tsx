import { getFilteredObject, useTable } from "@/tables";
import { useTreatmentStore } from "@/store";

export const useTreatmentTablePage = () => {
	const { getTreatments, queryResult, treatments, deleteTreatment } = useTreatmentStore();
	const { state, ...restTablePros } = useTable();

	const onSubmit = () => getTreatments(getFilteredObject({ state }));

	const rowCount = queryResult.totalCount;

	return {
		treatments,
		state,
		rowCount,
		deleteTreatment,
		...restTablePros,
		onSubmit,
	};
};
