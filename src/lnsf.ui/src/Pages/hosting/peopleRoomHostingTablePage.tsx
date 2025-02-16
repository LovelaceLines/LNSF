import { MRT_ColumnDef } from "material-react-table";
import { useMemo } from "react";

import { Checkbox } from "@/components";
import { useMaterialReactTable } from "@/tables";
import { MRTInputDateOnly, MRTInputNumber } from "@/tables/components";
import { peopleRoomHosting } from "@/types";
import { usePeopleRoomHostingTablePage } from "./usePeopleRoomHostingTablePage";
import { CopyButton } from "@/tables/util";
import { isInRoles } from "@/store";
import { dateTimeToStr } from "@/utils";

export const PeopleRoomHostingTablePage = () => {
  const { prh, rowCount, roomIdGrouped, toggleRoomIdGrouping, onSubmit, watch, register } =
    usePeopleRoomHostingTablePage();

  const columns = useMemo<MRT_ColumnDef<peopleRoomHosting>[]>(
    () => [
      {
        accessorKey: "hostingId",
        header: "Id Reserva",
        size: 100,
        Filter: ({ column }) => <MRTInputNumber column={column} />,
      },
      {
        accessorKey: "hosting.checkIn",
        header: "Check In",
        size: 300,
        filterVariant: "date-range",
        Filter: ({ column, rangeFilterIndex }) => (
          <MRTInputDateOnly column={column} rangeFilterIndex={rangeFilterIndex} />
        ),
        Cell: ({ row }) => dateTimeToStr(row.original.hosting?.checkIn, "ptBr"),
      },
      {
        accessorKey: "hosting.checkOut",
        header: "Check Out",
        size: 300,
        filterVariant: "date-range",
        Filter: ({ column, rangeFilterIndex }) => (
          <MRTInputDateOnly column={column} rangeFilterIndex={rangeFilterIndex} />
        ),
        Cell: ({ row }) => dateTimeToStr(row.original.hosting?.checkOut, "ptBr"),
      },
      {
        accessorKey: "peopleId",
        header: "Id Pessoa",
        size: 75,
        Filter: ({ column }) => <MRTInputNumber column={column} />,
      },
      {
        accessorKey: "people.name",
        header: "Nome",
      },
      {
        accessorKey: "people.rg",
        header: "RG",
        enableClickToCopy: true,
        muiCopyButtonProps: CopyButton,
        visibleInShowHideMenu: !isInRoles(["Voluntário"]),
      },
      {
        accessorKey: "people.cpf",
        header: "CPF",
        enableClickToCopy: true,
        muiCopyButtonProps: CopyButton,
        visibleInShowHideMenu: !isInRoles(["Voluntário"]),
      },
      {
        accessorKey: "roomId",
        header: "Id Apartamento",
        size: 75,
        Filter: ({ column }) => <MRTInputNumber column={column} />,
      },
      {
        accessorKey: "room.number",
        header: "Número Apartamento",
        size: 75,
      },
    ],
    []
  );

  return (
    <>
      {useMaterialReactTable({
        id: "peopleRoomHosting",
        columns,
        data: prh,
        title: "Reservas",

        getRowId: (originalRow) => `${originalRow.peopleId}.${originalRow.roomId}.${originalRow.hostingId}`,

        rowCount,

        onSubmit,

        renderTopToolbarFilterActions: () => (
          <>
            <Checkbox checked={watch("isActive")} register={register("isActive")} label="Ativos" />
            <Checkbox
              checked={roomIdGrouped}
              onChange={toggleRoomIdGrouping}
              label="Agrupar por Apartamento"
            />
          </>
        ),
      })}
    </>
  );
};
