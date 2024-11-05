import { MRT_ColumnDef } from "material-react-table";
import { useMemo } from "react";

import { useMaterialReactTable } from "@/tables";
import { MRTInputNumber, MRTLaunchLink } from "@/tables/components";
import { CopyButton } from "@/tables/util";
import { patient } from "@/types";
import { usePatientTablePage } from "./usePatientTablePage";

export const PatientTablePage = () => {
	const { patients, rowCount, onSubmit } = usePatientTablePage();

	const columns = useMemo<MRT_ColumnDef<patient>[]>(
		() => [
			{
				accessorKey: "id",
				header: "Id",
				size: 80,
				Filter: ({ column }) => <MRTInputNumber column={column} />,
			},
			{
				accessorKey: "peopleId",
				header: "Id Pessoa",
				Cell: ({ row }) => (
					<MRTLaunchLink
						label={row.original.peopleId}
						to={`/app/pessoas/${row.original.peopleId}`}
					/>
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
					<MRTLaunchLink
						label={row.original.hospitalId}
						to={`/app/hospitais/${row.original.hospitalId}`}
					/>
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
				Cell: ({ row }) => row.original.treatments?.map((t) => t.name).join(", "),
			},
		],
		[]
	);

	return (
		<>
			{useMaterialReactTable({
				id: "patient",
				columns,
				data: patients,
				title: "Pacientes Cadastrados",

				rowCount,

				onSubmit,

				enableRowSelection: true,

				toCreate: true,
				toEdit: true,
			})}
		</>
	);
};
