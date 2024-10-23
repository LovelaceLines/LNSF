import { MRT_ColumnDef } from "material-react-table";
import { useMemo } from "react";

import { useMaterialReactTable } from "@/tables";
import { MRTInputNumber } from "@/tables/components";
import { formatTypeTreatment, treatment } from "@/types";
import { useTreatmentTablePage } from "./useTreatmentTablePage";

export const TreatmentTablePage = () => {
	const {
		treatments,
		deleteTreatment: handleDelete,
		rowCount,
		state: { columnFilters, sorting, pagination, globalFilter, rowSelection },
		setColumnFilters,
		setGlobalFilter,
		setRowSelection,
		setPagination,
		setSorting,
		onSubmit,
	} = useTreatmentTablePage();

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
				Cell: ({ row }) => formatTypeTreatment(row.original.type),
			},
		],
		[]
	);

	return (
		<>
			{useMaterialReactTable({
				columns,
				data: treatments,
				title: "Tratamentos",

				setGlobalFilter,
				setColumnFilters,
				setSorting,
				setPagination,
				setRowSelection,

				rowCount,

				state: {
					globalFilter,
					columnFilters,
					sorting,
					rowSelection,
					pagination,
				},

				onSubmit,

				enableRowSelection: true,

				toCreate: true,
				toEdit: true,
				handleDelete,
			})}
		</>
	);
};
