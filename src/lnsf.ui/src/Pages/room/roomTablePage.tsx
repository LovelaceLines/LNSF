import { MRT_ColumnDef } from "material-react-table";
import { useMemo } from "react";

import { useMaterialReactTable } from "@/tables";
import { MRTInputNumber } from "@/tables/components";
import { room } from "@/types";
import { useRoomTablePage } from "./useRoomTablePage";

export const RoomTablePage = () => {
	const {
		rooms,
		rowCount,
		state: { columnFilters, sorting, pagination, globalFilter, rowSelection },
		setColumnFilters,
		setGlobalFilter,
		setRowSelection,
		setPagination,
		setSorting,
		onSubmit,
	} = useRoomTablePage();

	const columns = useMemo<MRT_ColumnDef<room>[]>(
		() => [
			{
				accessorKey: "id",
				header: "Id",
				size: 75,
				Filter: ({ column }) => <MRTInputNumber column={column} />,
			},
			{
				accessorKey: "number",
				header: "Número",
			},
			{
				accessorKey: "bathroom",
				header: "Banheiro",
				Cell: ({ row }) => (row.original.bathroom ? "Sim" : "Não"),
			},
			{
				accessorKey: "beds",
				header: "Camas",
			},
			{
				accessorKey: "storey",
				header: "Andar",
			},
			{
				accessorKey: "available",
				header: "Disponibilidade",
				Cell: ({ row }) => (row.original.available ? "Sim" : "Não"),
			},
		],
		[]
	);

	return (
		<>
			{useMaterialReactTable({
				columns,
				data: rooms,
				title: "Apartamentos",

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
			})}
		</>
	);
};
