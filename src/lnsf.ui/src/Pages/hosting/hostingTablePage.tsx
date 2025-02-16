import { MRT_ColumnDef } from "material-react-table";
import { Box } from "@mui/material";
import { useMemo } from "react";

import { useMaterialReactTable } from "@/tables";
import { MRTInputDateTime, MRTInputNumber, MRTLaunchLink } from "@/tables/components";
import { hosting } from "@/types";
import { useHostingTablePage } from "./useHostingTablePage";
import { dateTimeToStr } from "@/utils";

export const HostingTablePage = () => {
  const { handleDelete, hostings, rowCount, onSubmit } = useHostingTablePage();

  const columns = useMemo<MRT_ColumnDef<hosting>[]>(
    () => [
      {
        accessorKey: "id",
        header: "Id",
        size: 75,
        Filter: ({ column }) => <MRTInputNumber column={column} />,
      },
      {
        accessorKey: "patientId",
        header: "Id Paciente",
        Filter: ({ column }) => <MRTInputNumber column={column} />,
        Cell: ({ row }) => (
          <MRTLaunchLink
            label={row.original.patientId}
            to={`/app/pessoas/pacientes/${row.original.patientId}`}
          />
        ),
      },
      {
        accessorKey: "patient.people.name",
        header: "Nome Paciente",
      },
      {
        accessorKey: "escorts.id",
        header: "Id Acompanhante",
        Filter: ({ column }) => <MRTInputNumber column={column} />,
        Cell: ({ row }) => (
          <Box display="flex" gap={1}>
            {row.original.escorts?.map((e) => (
              <>
                <MRTLaunchLink label={e.id} to={`/app/pessoas/acompanhantes/${e.id}`} />
              </>
            ))}
          </Box>
        ),
      },
      {
        accessorKey: "escorts.people.name",
        header: "Nome Acompanhante",
        Cell: ({ row }) => row.original.escorts?.map((e) => e.people?.name).join(", "),
      },
      {
        accessorKey: "checkIn",
        header: "Check In",
        size: 300,
        filterVariant: "datetime-range",
        Filter: ({ column, rangeFilterIndex }) => (
          <MRTInputDateTime column={column} rangeFilterIndex={rangeFilterIndex} />
        ),
        Cell: ({ row }) => dateTimeToStr(row.original.checkIn, "ptBr"),
      },
      {
        accessorKey: "checkOut",
        header: "Check Out",
        size: 300,
        filterVariant: "datetime-range",
        Filter: ({ column, rangeFilterIndex }) => (
          <MRTInputDateTime column={column} rangeFilterIndex={rangeFilterIndex} />
        ),
        Cell: ({ row }) => dateTimeToStr(row.original.checkOut, "ptBr"),
      },
    ],
    []
  );

  return (
    <>
      {useMaterialReactTable({
        id: "hosting",
        columns,
        data: hostings,
        title: "Reservas",

        rowCount,

        onSubmit,

        enableRowSelection: true,

        toCreate: true,
        toEdit: true,
        handleDelete,
      })}
    </>
  );
};
