import { MRT_ColumnDef } from "material-react-table";
import { useMemo } from "react";

import { useMaterialReactTable } from "@/tables";
import { MRTInputDateOnly, MRTInputNumber } from "@/tables/components";
import { peopleRoomHosting } from "@/types";
import { usePeopleRoomHostingTablePage } from "./usePeopleRoomHostingTablePage";
import { CopyButton } from "@/tables/util";
import { dateTimeToStr } from "@/utils";

export const PeopleRoomHostingTablePage = () => {
	const { prh, rowCount, onSubmit } = usePeopleRoomHostingTablePage();

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
				id: "peopleRoomHosting",
				columns,
				data: prh,
				title: "Reservas",

				getRowId: (originalRow) =>
					`${originalRow.peopleId}.${originalRow.roomId}.${originalRow.hostingId}`,

				rowCount,

				onSubmit,
			})}
		</>
	);
};
