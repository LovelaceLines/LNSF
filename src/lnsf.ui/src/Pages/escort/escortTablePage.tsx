import { MRT_ColumnDef } from "material-react-table";
import { useMemo } from "react";

import { useMaterialReactTable } from "@/tables";
import { MRTInputNumber, MRTLaunchLink } from "@/tables/components";
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
				Cell: ({ row }) => <MRTLaunchLink label={row.original.peopleId} to={`/app/pessoas/${row.original.peopleId}`} />,
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
