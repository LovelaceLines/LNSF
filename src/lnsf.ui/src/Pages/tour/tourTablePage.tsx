import { MRT_ColumnDef } from "material-react-table";
import { useMemo } from "react";

import { Checkbox } from "@/components";
import { useMaterialReactTable } from "@/tables";
import { MRTInputDateTime, MRTInputNumber, MRTLaunchLink } from "@/tables/components";
import { CopyButton } from "@/tables/util";
import { tour } from "@/types";
import { useTourTablePage } from "./useTourTablePage";
import { dateTimeToStr } from "@/utils";

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
		register,
		watch,
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
				size: 300,
				filterVariant: "datetime-range",
				Filter: ({ column, rangeFilterIndex }) => (
					<MRTInputDateTime column={column} rangeFilterIndex={rangeFilterIndex} />
				),
				Cell: ({ row }) => dateTimeToStr(row.original.output, "ptBr"),
			},
			{
				accessorKey: "input",
				header: "Data de Entrada",
				size: 300,
				filterVariant: "datetime-range",
				Filter: ({ column, rangeFilterIndex }) => (
					<MRTInputDateTime column={column} rangeFilterIndex={rangeFilterIndex} />
				),
				Cell: ({ row }) => dateTimeToStr(row.original.input, "ptBr"),
			},
			{
				accessorKey: "people.id",
				header: "Id Pessoa",
				size: 75,
				Filter: ({ column }) => <MRTInputNumber column={column} />,
				Cell: ({ row }) => (
					<MRTLaunchLink
						label={row.original.people?.id}
						to={`/app/pessoas/${row.original.people?.id}`}
					/>
				),
			},
			{
				accessorKey: "people.name",
				header: "Nome",
				enableSorting: false,
				enableColumnFilter: false,
			},
			{
				accessorKey: "people.rg",
				header: "RG",
				size: 150,
				enableSorting: false,
				enableColumnFilter: false,
				enableClickToCopy: true,
				muiCopyButtonProps: CopyButton,
			},
			{
				accessorKey: "people.cpf",
				header: "CPF",
				size: 150,
				enableSorting: false,
				enableColumnFilter: false,
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

				enableRowSelection: true,

				toCreate: true,
				toEdit: true,

				renderTopToolbarFilterActions: () => (
					<>
						<Checkbox
							label="Saída e entrada"
							checked={watch("isClose")}
							register={register("isClose")}
						/>
						<Checkbox
							label="Apenas saída"
							checked={watch("isOpen")}
							register={register("isOpen")}
						/>
					</>
				),
			})}
		</>
	);
};
