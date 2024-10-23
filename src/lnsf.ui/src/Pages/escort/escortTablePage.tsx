import { MRT_ColumnDef } from "material-react-table";
import { Launch } from "@mui/icons-material";
import { Box, IconButton } from "@mui/material";
import { useMemo } from "react";
import { Link } from "react-router-dom";

import { useMaterialReactTable } from "@/tables";
import { MRTInputNumber } from "@/tables/components";
import { CopyButton } from "@/tables/util";
import { escort } from "@/types";
import { useEscortTablePage } from "./useEscortTablePage";

export const EscortTablePage = () => {
	const {
		escorts,
		rowCount,
		state: { columnFilters, sorting, pagination, globalFilter, rowSelection },
		setColumnFilters,
		setGlobalFilter,
		setRowSelection,
		setPagination,
		setSorting,
		onSubmit,
	} = useEscortTablePage();

	const columns = useMemo<MRT_ColumnDef<escort>[]>(
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
		],
		[]
	);

	return (
		<>
			{useMaterialReactTable({
				columns,
				data: escorts,
				title: "Acompanhantes Cadastrados",

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
