import { useForm } from "react-hook-form";

import { getFilteredObject, useTableState } from "@/tables";
import { peopleFilter } from "@/types";
import { isInRoles, usePeopleStore } from "@/store";
import { useEffect } from "react";

export const usePeopleTablePage = () => {
  const { getPeoples, peoples, queryResult } = usePeopleStore();

  const { register, getValues, watch, setValue } = useForm<peopleFilter>({
    values: { isActive: undefined, isEscort: undefined, isPatient: undefined, isVeteran: undefined },
  });

  const { state, setColumnVisibility } = useTableState();

  useEffect(() => {
    setColumnVisibility("people", {
      ...state.people.columnVisibility,
      cpf: isInRoles(["Voluntário"]) ? false : state.people.columnVisibility?.cpf ?? true,
      rg: isInRoles(["Voluntário"]) ? false : state.people.columnVisibility?.rg ?? true,
      phone: isInRoles(["Voluntário"]) ? false : state.people.columnVisibility?.phone ?? true,
    });
  }, []);

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
