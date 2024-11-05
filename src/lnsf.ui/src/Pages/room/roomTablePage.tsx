import { MRT_ColumnDef } from "material-react-table";
import { IconButton, Tooltip } from "@mui/material";
import { InfoOutlined } from "@mui/icons-material";
import { useMemo } from "react";

import { useMaterialReactTable } from "@/tables";
import { MRTInputNumber } from "@/tables/components";
import { room } from "@/types";
import { useRoomTablePage } from "./useRoomTablePage";
import { Checkbox } from "@/components";

export const RoomTablePage = () => {
	const { rooms, rowCount, onSubmit, register, watch } = useRoomTablePage();

	const columns = useMemo<MRT_ColumnDef<room>[]>(
		() => [
			{
				accessorKey: "id",
				header: "Id",
				size: 75,
				Filter: ({ column }) => <MRTInputNumber column={column} />,
			},
			{
				accessorKey: "number",
				header: "Número",
			},
			{
				accessorKey: "bathroom",
				header: "Banheiro",
				filterVariant: "select",
				filterSelectOptions: [
					{ value: "true", label: "Sim" },
					{ value: "false", label: "Não" },
				],
				Cell: ({ row }) => (row.original.bathroom ? "Sim" : "Não"),
			},
			{
				accessorKey: "beds",
				header: "Camas",
				Filter: ({ column }) => <MRTInputNumber column={column} />,
			},
			{
				accessorKey: "storey",
				header: "Andar",
				Filter: ({ column }) => <MRTInputNumber column={column} />,
			},
			{
				accessorKey: "available",
				header: "Disponibilidade",
				filterVariant: "select",
				filterSelectOptions: [
					{ value: "true", label: "Sim" },
					{ value: "false", label: "Não" },
				],
				Cell: ({ row }) => (row.original.available ? "Sim" : "Não"),
			},
		],
		[]
	);

	return (
		<>
			{useMaterialReactTable({
				id: "room",
				columns,
				data: rooms,
				title: "Apartamentos",

				rowCount,

				onSubmit,

				enableRowSelection: true,

				toCreate: true,
				toEdit: true,

				renderTopToolbarFilterActions: () => (
					<>
						<Checkbox
							label="Disponíveis agora"
							checked={watch("isAvailable")}
							register={register("isAvailable")}
						/>
					</>
				),

				renderToolbarExtraInternalActions: () => (
					<>
						<Tooltip title="◦ Disponibilidade refere-se as condições do apartamento para ser alugado.">
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
