import { MRT_ColumnDef } from "material-react-table";
import { useMemo } from "react";

import { useMaterialReactTable } from "@/tables";
import { MRTInputNumber } from "@/tables/components";
import { user } from "@/types";
import { useUserTablePage } from "./useUserTablePage";

export const UserTablePage = () => {
  const { users, rowCount, onSubmit } = useUserTablePage();

  const columns = useMemo<MRT_ColumnDef<user>[]>(
    () => [
      {
        accessorKey: "id",
        header: "Id",
        size: 75,
        Filter: ({ column }) => <MRTInputNumber column={column} />,
      },
      {
        accessorKey: "name",
        header: "Nome",
      },
      {
        accessorKey: "roles",
        header: "Funções",
        enableSorting: false,
        enableColumnFilter: false,
        Cell: ({ row }) => row.original.roles.map((role) => role.name).join(", "),
      },
      {
        accessorKey: "userName",
        header: "Usuário",
      },
      {
        accessorKey: "phoneNumber",
        header: "Telefone",
      },
      {
        accessorKey: "email",
        header: "Email",
      },
    ],
    []
  );

  return (
    <>
      {useMaterialReactTable({
        id: "user",
        columns,
        data: users,
        title: "Usuários",

        rowCount,

        onSubmit,

        enableRowSelection: true,

        toCreate: true,
        toEdit: true,
      })}
    </>
  );
};
