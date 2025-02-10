import { MRT_ColumnDef } from "material-react-table";
import { useMemo } from "react";

import { useMaterialReactTable } from "@/tables";
import { MRTInputNumber } from "@/tables/components";
import { hospital } from "@/types";
import { useHospitalTablePage } from "./useHospitalTablePage";

export const HospitalTablePage = () => {
  const { hospitals, rowCount, onSubmit } = useHospitalTablePage();

  const columns = useMemo<MRT_ColumnDef<hospital>[]>(
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
        accessorKey: "acronym",
        header: "Sigla",
      },
    ],
    []
  );

  return (
    <>
      {useMaterialReactTable({
        id: "hospital",
        columns,
        data: hospitals,
        title: "Hospitais",

        rowCount,

        onSubmit,

        enableRowSelection: true,

        toCreate: true,
        toEdit: true,
      })}
    </>
  );
};
