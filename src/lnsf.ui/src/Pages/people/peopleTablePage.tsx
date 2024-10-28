import { MRT_ColumnDef } from "material-react-table";
import { Check, InfoOutlined, Launch } from "@mui/icons-material";
import { Button, IconButton, Switch, ToggleButton, Tooltip } from "@mui/material";
import { useMemo } from "react";
import { Link } from "react-router-dom";

import { Checkbox } from "@/components";
import { useMaterialReactTable } from "@/tables";
import { MRTInputDateOnly, MRTInputNumber } from "@/tables/components";
import { CopyButton } from "@/tables/util";
import {
	formatGender,
	formatMaritalStatus,
	formatRaceColor,
	getGender,
	getMaritalStatus,
	getRaceColor,
	people,
} from "@/types";
import { usePeopleTablePage } from "./usePeopleTablePage";
import { dateOnlyToStr } from "@/utils";

export const PeopleTablePage = () => {
	const {
		peoples,
		register,
		watch,
		rowCount,
		setValue,
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
				enableSorting: false,
				enableColumnFilter: false,
				size: 100,
			},
			{
				accessorKey: "status",
				header: "Status",
				enableSorting: false,
				enableColumnFilter: false,
				size: 100,
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
					`${row.original.street}, ${row.original.houseNumber} - ${row.original.neighborhood}, ${row.original.city} - ${row.original.state}`,
			},
			{
				accessorKey: "birthDate",
				header: "Data de Nascimento",
				filterVariant: "date",
				Filter: ({ column }) => <MRTInputDateOnly column={column} />,
				Cell: ({ row }) => dateOnlyToStr(row.original.birthDate, "ptBr"),
			},
			{
				accessorKey: "gender",
				header: "Sexo",
				size: 100,
				filterVariant: "select",
				filterSelectOptions: getGender().map((g) => ({ value: g.id, label: g.value })),
				Cell: ({ row }) => formatGender(row.original.gender),
			},
			{
				accessorKey: "maritalStatus",
				header: "Estado Civil",
				size: 100,
				filterVariant: "select",
				filterSelectOptions: getMaritalStatus().map((m) => ({ value: m.id, label: m.value })),
				Cell: ({ row }) => formatMaritalStatus(row.original.maritalStatus),
			},
			{
				accessorKey: "raceColor",
				header: "Raça",
				size: 100,
				filterVariant: "select",
				filterSelectOptions: getRaceColor().map((r) => ({ value: r.id, label: r.value })),
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

				renderTopToolbarFilterActions: () => (
					<>
						<Checkbox
							label="Ativos"
							checked={watch("isActive")}
							register={register("isActive")}
						/>
						<Checkbox
							label="Somente Pacientes"
							checked={watch("isPatient")}
							register={register("isPatient")}
						/>
						<Checkbox
							label="Somente Acompanhantes"
							checked={watch("isEscort")}
							register={register("isEscort")}
						/>
						<Checkbox
							label="Para Chegar"
							checked={watch("willHosted")}
							register={register("willHosted")}
						/>
					</>
				),

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

				renderToolbarExtraInternalActions: () => (
					<>
						<Tooltip title="◦ Status 'Paciente' pode sobrepor o status 'Acompanhante'">
							<IconButton size="medium" aria-label="teste">
								<InfoOutlined />
							</IconButton>
						</Tooltip>
					</>
				),
			})}
		</>
	);
};
