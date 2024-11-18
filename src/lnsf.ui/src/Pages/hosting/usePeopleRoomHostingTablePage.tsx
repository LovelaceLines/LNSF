import { getFilteredObject, useTableState } from "@/tables";
import { usePeopleRoomHostingStore } from "@/store";
import { useEffect } from "react";
import { isInRoles } from "@/services";

export const usePeopleRoomHostingTablePage = () => {
	const { getPeopleRoomHosting, prh, queryResult } = usePeopleRoomHostingStore();
	const { state, setColumnVisibility } = useTableState();

	const rowCount = queryResult.totalCount;

	const onSubmit = () => getPeopleRoomHosting(getFilteredObject({ state: state.peopleRoomHosting }));

	useEffect(() => {
		setColumnVisibility("peopleRoomHosting", {
			...state.peopleRoomHosting.columnVisibility,
			"people.cpf": isInRoles(["Voluntário"])
				? false
				: state.peopleRoomHosting.columnVisibility?.cpf ?? true,
			"people.rg": isInRoles(["Voluntário"])
				? false
				: state.peopleRoomHosting.columnVisibility?.rg ?? true,
		});
	}, []);

	return {
		prh,
		rowCount,
		onSubmit,
	};
};
