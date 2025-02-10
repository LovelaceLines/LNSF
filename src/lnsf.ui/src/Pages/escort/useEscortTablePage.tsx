import { getFilteredObject, useTableState } from "@/tables";
import { useEscortStore } from "@/store";

export const useEscortTablePage = () => {
  const { getEscorts, escorts, queryResult } = useEscortStore();
  const { state } = useTableState();

  const onSubmit = () => getEscorts(getFilteredObject({ state: state.escort }));

  const rowCount = queryResult.totalCount;

  return {
    escorts,
    rowCount,
    onSubmit,
  };
};
