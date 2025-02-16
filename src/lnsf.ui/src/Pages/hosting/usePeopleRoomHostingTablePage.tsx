import { useEffect } from "react";
import { useForm } from "react-hook-form";

import { getFilteredObject, useTableState } from "@/tables";
import { isInRoles, usePeopleRoomHostingStore } from "@/store";
import { peopleRoomHostingFilter } from "@/types";

export const usePeopleRoomHostingTablePage = () => {
  const { getPeopleRoomHosting, prh, queryResult } = usePeopleRoomHostingStore();
  const { state, setColumnVisibility, setGrouping } = useTableState();

  const { getValues, register, watch } = useForm<peopleRoomHostingFilter>({
    values: { isActive: undefined },
  });

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

  const onSubmit = () =>
    getPeopleRoomHosting({
      ...getFilteredObject({ state: state.peopleRoomHosting }),
      ...getValues(),
      isActive: getValues("isActive") || undefined,
    });

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
    watch,
    register,
  };
};
