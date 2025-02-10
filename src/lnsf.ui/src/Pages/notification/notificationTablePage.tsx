import { MRT_ColumnDef } from "material-react-table";
import { useMemo } from "react";

import { useMaterialReactTable } from "@/tables";
import { MRTInputDateTime, MRTInputNumber } from "@/tables/components";
import { notification } from "@/types";
import { useNotificationTablePage } from "./useNotificationTablePage";
import { dateTimeToStr } from "@/utils";

export const NotificationTablePage = () => {
  const { queryResult, onSubmit } = useNotificationTablePage();

  const columns = useMemo<MRT_ColumnDef<notification>[]>(
    () => [
      {
        accessorKey: "id",
        header: "Id",
        Filter: ({ column }) => <MRTInputNumber column={column} />,
      },
      {
        accessorKey: "title",
        header: "Título",
      },
      {
        accessorKey: "content",
        header: "Conteúdo",
      },
      {
        accessorKey: "validFrom",
        header: "Válido de",
        filterVariant: "datetime-range",
        Filter: ({ column, rangeFilterIndex }) => (
          <MRTInputDateTime column={column} rangeFilterIndex={rangeFilterIndex} />
        ),
        Cell: ({ row }) => dateTimeToStr(row.original.validFrom, "ptBr"),
      },
      {
        accessorKey: "expiredAt",
        header: "Válido até",
        filterVariant: "datetime-range",
        Filter: ({ column, rangeFilterIndex }) => (
          <MRTInputDateTime column={column} rangeFilterIndex={rangeFilterIndex} />
        ),
        Cell: ({ row }) => dateTimeToStr(row.original.expiredAt, "ptBr"),
      },
    ],
    []
  );

  return (
    <>
      {useMaterialReactTable({
        id: "notification",
        columns,
        data: queryResult.items,
        title: "Notificações",

        rowCount: queryResult.totalCount,

        onSubmit,

        enableRowSelection: true,

        toCreate: true,
        toEdit: true,
      })}
    </>
  );
};
