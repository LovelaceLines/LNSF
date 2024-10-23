import { MRT_ColumnDef } from "material-react-table";
import { Launch } from "@mui/icons-material";
import { Box, IconButton } from "@mui/material";
import { useMemo } from "react";
import { Link } from "react-router-dom";

import { useMaterialReactTable } from "@/tables";
import { MRTInputNumber } from "@/tables/components";
import { CopyButton } from "@/tables/util";
import { patient } from "@/types";
import { usePatientTablePage } from "./usePatientTablePage";

export const PatientTablePage = () => {
	const {
		patients,
		rowCount,
		state: { columnFilters, sorting, pagination, globalFilter, rowSelection },
		setColumnFilters,
		setGlobalFilter,
		setRowSelection,
		setPagination,
		setSorting,
		onSubmit,
	} = usePatientTablePage();

	const columns = useMemo<MRT_ColumnDef<patient>[]>(
		() => [
			{
				accessorKey: "id",
				header: "Id",
				size: 75,
				Filter: ({ column }) => <MRTInputNumber column={column} />,
			},
			{
				accessorKey: "peopleId",
				header: "Id Pessoa",
				Cell: ({ row }) => (
					<Box display="flex" alignItems="center" gap={1}>
						{row.original.peopleId}
						<Link to={`/app/pessoas/${row.original.peopleId}`}>
							<IconButton size="small">
								<Launch />
							</IconButton>
						</Link>
					</Box>
				),
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
				accessorKey: "term",
				header: "Termo",
				Cell: ({ row }) => (row.original.term ? "Sim" : "Não"),
			},
			{
				accessorKey: "socioeconomicRecord",
				header: "Cad. Socioeconomico",
				Cell: ({ row }) => (row.original.socioeconomicRecord ? "Sim" : "Não"),
			},
			{
				accessorKey: "hospitalId",
				header: "Id Hospital",
				Cell: ({ row }) => (
					<Box display="flex" alignItems="center" gap={1}>
						{row.original.hospitalId}
						<Link to={`/app/hospitais/${row.original.hospitalId}`}>
							<IconButton size="small">
								<Launch />
							</IconButton>
						</Link>
					</Box>
				),
			},
			{
				accessorKey: "hospital.name",
				header: "Hospital",
				Cell: ({ row }) => `${row.original.hospital?.acronym} - ${row.original.hospital?.name}`,
			},
			{
				accessorKey: "treatments",
				header: "Tratamentos",
				Cell: ({ row }) => row.original.treatments.map((t) => t.name).join(", "),
			},
		],
		[]
	);

	return (
		<>
			{useMaterialReactTable({
				columns,
				data: patients,
				title: "Pacientes Cadastrados",

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
