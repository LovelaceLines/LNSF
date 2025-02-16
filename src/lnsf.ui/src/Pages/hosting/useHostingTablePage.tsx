import { getFilteredObject, useTableState } from "@/tables";
import { useHostingStore } from "@/store";

export const useHostingTablePage = () => {
  const { getHostings, hostings, queryResult, deleteHosting } = useHostingStore();
  const { state } = useTableState();

  const onSubmit = () => getHostings(getFilteredObject({ state: state.hosting }));

  const rowCount = queryResult.totalCount;

  return {
    handleDelete: deleteHosting,
    hostings,
    rowCount,
    onSubmit,
  };
};
