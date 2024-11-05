import { getFilteredObject, useTableState } from "@/tables";
import { useTreatmentStore } from "@/store";

export const useTreatmentTablePage = () => {
	const { getTreatments, queryResult, treatments, deleteTreatment } = useTreatmentStore();
	const { state } = useTableState();

	const onSubmit = () => getTreatments(getFilteredObject({ state: state.treatment }));

	const rowCount = queryResult.totalCount;

	return {
		treatments,
		state,
		rowCount,
		deleteTreatment,
		onSubmit,
	};
};
