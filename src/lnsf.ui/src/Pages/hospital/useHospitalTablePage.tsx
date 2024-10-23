import { getFilteredObject, useTable } from "@/tables";
import { useHospitalStore } from "@/zustand";

export const useHospitalTablePage = () => {
	const { getHospitals, hospitals, queryResult } = useHospitalStore();
	const { state, ...restTablePros } = useTable();

	const onSubmit = () => getHospitals(getFilteredObject({ state }));

	const rowCount = queryResult.totalCount;

	return {
		hospitals,
		state,
		rowCount,
		...restTablePros,
		onSubmit,
	};
};
