import { MRT_ColumnDef } from "material-react-table";
import { useMemo } from "react";

import { dateOnlyToStr, useMaterialReactTable } from "@/tables";
import { MRTInputNumber } from "@/tables/components";
import { hosting } from "@/types";
import { useHostingTablePage } from "./useHostingTablePage";
import { Box, IconButton } from "@mui/material";
import { Link } from "react-router-dom";
import { Launch } from "@mui/icons-material";

export const HostingTablePage = () => {
	const {
		hostings,
		rowCount,
		state: { columnFilters, sorting, pagination, globalFilter, rowSelection },
		setColumnFilters,
		setGlobalFilter,
		setRowSelection,
		setPagination,
		setSorting,
		onSubmit,
	} = useHostingTablePage();

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
					<Box display="flex" alignItems="center" gap={1}>
						{row.original.patientId}
						<Link to={`/app/pessoas/pacientes/${row.original.patientId}`}>
							<IconButton size="small">
								<Launch />
							</IconButton>
						</Link>
					</Box>
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
				Cell: ({ row }) => row.original.escorts.map((e) => e.id).join(", "),
			},
			{
				accessorKey: "escorts.people.name",
				header: "Nome Acompanhante",
				Cell: ({ row }) => row.original.escorts.map((e) => e.people?.name).join(", "),
			},
			{
				accessorKey: "checkIn",
				header: "Check In",
				Cell: ({ row }) => dateOnlyToStr(row.original.checkIn),
			},
			{
				accessorKey: "checkOut",
				header: "Check Out",
				Cell: ({ row }) => dateOnlyToStr(row.original.checkOut),
			},
		],
		[]
	);

	return (
		<>
			{useMaterialReactTable({
				columns,
				data: hostings,
				title: "Hospedagens",

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
