import { getFilteredObject, useTableState } from "@/tables";
import { useHospitalStore } from "@/store";

export const useHospitalTablePage = () => {
	const { getHospitals, hospitals, queryResult } = useHospitalStore();
	const { state } = useTableState();

	const onSubmit = () => getHospitals(getFilteredObject({ state: state.hospital }));

	const rowCount = queryResult.totalCount;

	return {
		hospitals,
		rowCount,
		onSubmit,
	};
};
