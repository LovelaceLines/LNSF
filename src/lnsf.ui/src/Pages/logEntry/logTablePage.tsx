import { MRT_ColumnDef } from "material-react-table";
import { useMemo } from "react";

import { useMaterialReactTable } from "@/tables";
import { MRTInputDateTime, MRTInputNumber, MRTLaunchLink } from "@/tables/components";
import { formatAction, formatEntityName, getAction, getEntityName, log } from "@/types";
import { useLogTablePage } from "./useLogTablePage";
import { dateTimeToStr } from "@/utils";
import { ValuesChangesModal } from "./valuesChangesModal";

export const LogTablePage = () => {
	const { logs, rowCount, onSubmit } = useLogTablePage();

	const columns = useMemo<MRT_ColumnDef<log>[]>(
		() => [
			{
				accessorKey: "id",
				header: "Id",
				size: 50,
				Filter: ({ column }) => <MRTInputNumber column={column} />,
			},
			{
				accessorKey: "userId",
				header: "Usuário",
				size: 50,
				Cell: ({ row }) => (
					<MRTLaunchLink label={row.original.userId} to={`/app/usuarios/${row.original.userId}`} />
				),
			},
			{
				accessorKey: "user.name",
				header: "Nome do Usuário",
				size: 75,
			},
			{
				accessorKey: "entityName",
				header: "Entidade",
				filterVariant: "select",
				filterSelectOptions: getEntityName().map((t) => ({ value: t.id, label: t.value })),
				Cell: ({ row }) => formatEntityName(row.original.entityName),
				size: 75,
			},
			{
				accessorKey: "entityId",
				header: "Id Entidade",
				size: 50,
			},
			{
				accessorKey: "action",
				header: "Ação",
				filterVariant: "select",
				filterSelectOptions: getAction().map((t) => ({ value: t.id, label: t.value })),
				Cell: ({ row }) => formatAction(row.original.action),
				size: 50,
			},
			{
				accessorKey: "logDateTime",
				header: "Data/Hora",
				size: 50,
				filterVariant: "datetime-range",
				Filter: ({ column, rangeFilterIndex }) => (
					<MRTInputDateTime column={column} rangeFilterIndex={rangeFilterIndex} />
				),
				Cell: ({ row }) => dateTimeToStr(row.original.logDateTime, "ptBr"),
			},
			{
				accessorKey: "valuesChanges",
				header: "Valores",
				Cell: ({ row }) => (
					<ValuesChangesModal id={row.original.id} valuesChanges={row.original.valuesChanges} />
				),
			},
		],
		[]
	);

	return (
		<>
			{useMaterialReactTable({
				columns,
				data: logs,
				title: "Logs do Sistema",
				id: "log",

				rowCount,

				onSubmit,
			})}
		</>
	);
};
