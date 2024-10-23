import { MRT_ColumnDef } from "material-react-table";
import { Launch } from "@mui/icons-material";
import { Button } from "@mui/material";
import { useMemo } from "react";
import { Link } from "react-router-dom";

import { useMaterialReactTable, dateOnlyToStr } from "@/tables";
import { MRTInputNumber } from "@/tables/components";
import { CopyButton } from "@/tables/util";
import { formatGender, formatMaritalStatus, formatRaceColor, people } from "@/types";
import { usePeopleTablePage } from "./usePeopleTablePage";

export const PeopleTablePage = () => {
	const {
		peoples,
		rowCount,
		state: { columnFilters, sorting, pagination, globalFilter, rowSelection },
		setColumnFilters,
		setGlobalFilter,
		setRowSelection,
		setPagination,
		setSorting,
		onSubmit,
	} = usePeopleTablePage();

	const columns = useMemo<MRT_ColumnDef<people>[]>(
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
				accessorKey: "rg",
				header: "RG",
				enableClickToCopy: true,
				muiCopyButtonProps: CopyButton,
			},
			{
				accessorKey: "cpf",
				header: "CPF",
				enableClickToCopy: true,
				muiCopyButtonProps: CopyButton,
			},
			{
				accessorKey: "email",
				header: "E-mail",
			},
			{
				accessorKey: "phone",
				header: "Telefone",
				enableClickToCopy: true,
				muiCopyButtonProps: CopyButton,
			},
			{
				accessorKey: "experience",
				header: "Experiência",
			},
			{
				accessorKey: "note",
				header: "Observação",
			},
			{
				accessorKey: "street",
				header: "Endereço",
				enableClickToCopy: true,
				muiCopyButtonProps: CopyButton,
				Cell: ({ row }) =>
					`${row.original.street}, ${row.original.houseNumber}, ${row.original.neighborhood}, ${row.original.city} - ${row.original.state}`,
			},
			{
				accessorKey: "birthDate",
				header: "Data de Nascimento",
				size: 100,
				Cell: ({ row }) => dateOnlyToStr(row.original.birthDate),
			},
			{
				accessorKey: "gender",
				header: "Sexo",
				size: 100,
				Cell: ({ row }) => formatGender(row.original.gender),
			},
			{
				accessorKey: "maritalStatus",
				header: "Estado Civil",
				size: 100,
				Cell: ({ row }) => formatMaritalStatus(row.original.maritalStatus),
			},
			{
				accessorKey: "raceColor",
				header: "Raça",
				size: 100,
				Cell: ({ row }) => formatRaceColor(row.original.raceColor),
			},
		],
		[]
	);

	return (
		<>
			{useMaterialReactTable({
				columns,
				data: peoples,
				title: "Pessoas Cadastradas",

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

				renderTopToolbarExtraCustomActions: () => (
					<>
						<Link to={`/app/pessoas/pacientes`}>
							<Button variant="outlined" size="small" endIcon={<Launch />}>
								Pacientes
							</Button>
						</Link>
						<Link to={`/app/pessoas/acompanhantes`}>
							<Button variant="outlined" size="small" endIcon={<Launch />}>
								Acompanhantes
							</Button>
						</Link>
					</>
				),
			})}
		</>
	);
};
