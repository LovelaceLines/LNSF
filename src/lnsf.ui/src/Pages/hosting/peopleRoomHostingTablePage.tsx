import { MRT_ColumnDef } from "material-react-table";
import { useMemo } from "react";

import { useMaterialReactTable } from "@/tables";
import { MRTInputNumber } from "@/tables/components";
import { peopleRoomHosting } from "@/types";
import { usePeopleRoomHostingTablePage } from "./usePeopleRoomHostingTablePage";
import { CopyButton } from "@/tables/util";
import { dateOnlyToStr } from "@/utils";

export const PeopleRoomHostingTablePage = () => {
	const {
		prh,
		rowCount,
		state: { columnFilters, sorting, pagination, globalFilter, rowSelection },
		setColumnFilters,
		setGlobalFilter,
		setRowSelection,
		setPagination,
		setSorting,
		onSubmit,
	} = usePeopleRoomHostingTablePage();

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
				Cell: ({ row }) => dateOnlyToStr(row.original.hosting?.checkIn, "ptBr"),
			},
			{
				accessorKey: "hosting.checkOut",
				header: "Check Out",
				Cell: ({ row }) => dateOnlyToStr(row.original.hosting?.checkOut, "ptBr"),
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
			},
			{
				accessorKey: "people.cpf",
				header: "CPF",
				enableClickToCopy: true,
				muiCopyButtonProps: CopyButton,
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
			{
				accessorKey: "room.available",
				header: "Disponibilidade",
				size: 75,
				filterVariant: "select",
				filterSelectOptions: [
					{ value: "true", label: "Sim" },
					{ value: "false", label: "Não" },
				],
				Cell: ({ row }) => (row.original.room?.available ? "Sim" : "Não"),
			},
		],
		[]
	);

	return (
		<>
			{useMaterialReactTable({
				columns,
				data: prh,
				title: "Reservas",

				setGlobalFilter,
				setColumnFilters,
				setSorting,
				setPagination,
				setRowSelection,

				getRowId: (originalRow) => `${originalRow.peopleId}.${originalRow.roomId}.${originalRow.hostingId}`,

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
			})}
		</>
	);
};
