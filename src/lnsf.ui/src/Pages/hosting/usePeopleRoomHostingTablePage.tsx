import { getFilteredObject, useTableState } from "@/tables";
import { usePeopleRoomHostingStore } from "@/store";
import { useEffect, useState } from "react";
import { isInRoles } from "@/services";

export const usePeopleRoomHostingTablePage = () => {
	const { getPeopleRoomHosting, prh, queryResult } = usePeopleRoomHostingStore();
	const { state, setColumnVisibility, setGrouping } = useTableState();

	useEffect(() => {
		setColumnVisibility("peopleRoomHosting", {
			...state.peopleRoomHosting.columnVisibility,
			"people.cpf": isInRoles(["Voluntário"])
				? false
				: state.peopleRoomHosting.columnVisibility?.["people.cpf"] ?? true,
			"people.rg": isInRoles(["Voluntário"])
				? false
				: state.peopleRoomHosting.columnVisibility?.["people.cpf"] ?? true,
		});
	}, []);

	const rowCount = queryResult.totalCount;

	const onSubmit = () => getPeopleRoomHosting(getFilteredObject({ state: state.peopleRoomHosting }));

	const roomIdGrouped = state.peopleRoomHosting.grouping?.includes("roomId");
	const toggleRoomIdGrouping = () =>
		setGrouping(
			"peopleRoomHosting",
			state.peopleRoomHosting.grouping?.includes("roomId")
				? state.peopleRoomHosting.grouping.filter((g) => g !== "roomId")
				: ["roomId", ...(state.peopleRoomHosting.grouping ?? [])]
		);

	return {
		prh,
		rowCount,
		roomIdGrouped,
		toggleRoomIdGrouping,
		onSubmit,
	};
};
