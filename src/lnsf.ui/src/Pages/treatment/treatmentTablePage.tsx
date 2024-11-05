import { MRT_ColumnDef } from "material-react-table";
import { useMemo } from "react";

import { useMaterialReactTable } from "@/tables";
import { MRTInputNumber } from "@/tables/components";
import { formatTypeTreatment, getTypeTreatment, treatment } from "@/types";
import { useTreatmentTablePage } from "./useTreatmentTablePage";

export const TreatmentTablePage = () => {
	const { treatments, deleteTreatment: handleDelete, rowCount, onSubmit } = useTreatmentTablePage();

	const columns = useMemo<MRT_ColumnDef<treatment>[]>(
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
				accessorKey: "type",
				header: "Tipo",
				filterVariant: "select",
				filterSelectOptions: getTypeTreatment().map((t) => ({ value: t.id, label: t.value })),
				Cell: ({ row }) => formatTypeTreatment(row.original.type),
			},
		],
		[]
	);

	return (
		<>
			{useMaterialReactTable({
				id: "treatment",
				columns,
				data: treatments,
				title: "Tratamentos",

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
