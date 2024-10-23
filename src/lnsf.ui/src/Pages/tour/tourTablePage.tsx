import { MRT_ColumnDef } from "material-react-table";
import { Launch } from "@mui/icons-material";
import { Box, IconButton } from "@mui/material";
import { useMemo } from "react";
import { Link } from "react-router-dom";

import { useMaterialReactTable, dateTimeToStr } from "@/tables";
import { MRTInputNumber } from "@/tables/components";
import { CopyButton } from "@/tables/util";
import { tour } from "@/types";
import { useTourTablePage } from "./useTourTablePage";

export const TourTablePage = () => {
	const {
		tours,
		rowCount,
		state: { columnFilters, sorting, pagination, globalFilter, rowSelection },
		setColumnFilters,
		setGlobalFilter,
		setRowSelection,
		setPagination,
		setSorting,
		onSubmit,
	} = useTourTablePage();

	const columns = useMemo<MRT_ColumnDef<tour>[]>(
		() => [
			{
				accessorKey: "id",
				header: "Id",
				size: 75,
				Filter: ({ column }) => <MRTInputNumber column={column} />,
			},
			{
				accessorKey: "output",
				header: "Data de Saída",
				size: 150,
				Cell: ({ row }) => dateTimeToStr(row.original.output),
			},
			{
				accessorKey: "input",
				header: "Data de Entrada",
				size: 150,
				Cell: ({ row }) => dateTimeToStr(row.original.input),
			},
			{
				accessorKey: "people.id",
				header: "Id Pessoa",
				size: 75,
				Filter: ({ column }) => <MRTInputNumber column={column} />,
				Cell: ({ row }) => (
					<Box display="flex" alignItems="center" gap={1}>
						{row.original.people.id}
						<Link to={`/app/pessoas/${row.original.people.id}`}>
							<IconButton>
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
				size: 150,
				enableClickToCopy: true,
				muiCopyButtonProps: CopyButton,
			},
			{
				accessorKey: "people.cpf",
				header: "CPF",
				size: 150,
				enableClickToCopy: true,
				muiCopyButtonProps: CopyButton,
			},
		],
		[]
	);

	return (
		<>
			{useMaterialReactTable({
				columns,
				data: tours,
				title: "Registro Diário",

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
			})}
		</>
	);
};
