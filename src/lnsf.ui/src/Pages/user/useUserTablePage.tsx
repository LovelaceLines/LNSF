import { getFilteredObject, useTableState } from "@/tables";
import { useUserStore } from "@/store";

export const useUserTablePage = () => {
  const { getUsers, queryResult, users } = useUserStore();
  const { state } = useTableState();

  const onSubmit = () => getUsers(getFilteredObject({ state: state.user }));

  const rowCount = queryResult.totalCount;

  return {
    users,
    rowCount,
    onSubmit,
  };
};
