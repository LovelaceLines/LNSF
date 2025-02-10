import { getFilteredObject, useTableState } from "@/tables";
import { useLogStore } from "@/store";

export const useLogTablePage = () => {
  const { getLogs, logs, queryResult } = useLogStore();
  const { state } = useTableState();

  const onSubmit = () =>
    getLogs({
      ...getFilteredObject({ state: state.log }),
    });

  const rowCount = queryResult.totalCount;

  return {
    logs,
    rowCount,
    onSubmit,
  };
};
