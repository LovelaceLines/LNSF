import {
	MaterialReactTable,
	MRT_ColumnFiltersState,
	MRT_PaginationState,
	MRT_Row,
	MRT_RowSelectionState,
	MRT_ShowHideColumnsButton,
	MRT_SortingState,
	MRT_TableInstance,
	MRT_TableState,
	MRT_ToggleDensePaddingButton,
	MRT_ToggleFiltersButton,
	MRT_ToggleFullScreenButton,
	MRT_ToggleGlobalFilterButton,
	useMaterialReactTable as useMaterialReactTableMRT,
	type MRT_ColumnDef,
	type MRT_RowData,
	type MRT_TableOptions,
} from "material-react-table";
import { MRT_Localization_PT_BR } from "material-react-table/locales/pt-BR";
import { Add, Check, ClearAll, Delete, Edit, FileDownload, Share } from "@mui/icons-material";
import { Box, Button, IconButton, Tooltip } from "@mui/material";
import { Dispatch, SetStateAction, useCallback } from "react";
import { Link } from "react-router-dom";
import { toast } from "react-toastify";

import { DownloadExportDisplay } from "./components/download-export-display/downloadExportDisplay";
import { useModal } from "@/contexts";
import { useDebounced } from "@/hooks";
import { colors, useThemeContext } from "@/theme";

interface Props<TData extends MRT_RowData> extends MRT_TableOptions<TData> {
	columns: MRT_ColumnDef<TData>[];
	data: TData[];
	setGlobalFilter?: Dispatch<SetStateAction<string>>;
	setColumnFilters?: Dispatch<SetStateAction<MRT_ColumnFiltersState>>;
	setSorting?: Dispatch<SetStateAction<MRT_SortingState>>;
	setPagination?: Dispatch<SetStateAction<MRT_PaginationState>>;
	rowCount?: number;
	getRowId?: ((originalRow: TData, index: number, parentRow: MRT_Row<TData>) => string) | undefined;
	initialState?: Partial<MRT_TableState<TData>>;
	state?: Partial<MRT_TableState<TData>>;
	setRowSelection?: Dispatch<SetStateAction<MRT_RowSelectionState>>;
	enableRowSelection?: boolean;
	onSubmit?: () => void;
	isLoading?: () => boolean;
	title: string;
	toCreate?: string | boolean;
	toEdit?: string | boolean;
	handleDelete?: (id: number) => void;
	handleSelect?: (ids: TData[]) => void;
	renderTopToolbarExtraCustomActions?: () => JSX.Element;
}

export const useMaterialReactTable = <TData extends MRT_RowData>({ columns, data, ...props }: Props<TData>) => {
	const { isOpen: isOpenModal, handleModalOpen } = useModal("download-export-display");
	const { themeName } = useThemeContext();

	const handleShare = useCallback(async () => {
		const url = window.location.href;

		if (navigator.clipboard) {
			navigator.clipboard.writeText(url);
			toast.error("Erro ao copiar link.");
			return;
		}

		const textArea = document.createElement("textarea");
		textArea.value = url;
		textArea.style.position = "absolute";
		textArea.style.left = "-999999px";

		document.body.prepend(textArea);
		textArea.select();

		try {
			document.execCommand("copy");
			toast.info("Link copiado para a área de transferência.");
		} catch {
			toast.error("Erro ao copiar link.");
		}
	}, []);

	const handleClearFilters = useCallback(() => {
		props.setGlobalFilter && props.setGlobalFilter("");
		props.setColumnFilters && props.setColumnFilters([]);
		props.setSorting && props.setSorting([]);
	}, []);

	const handleDelete = useCallback(() => {
		if (!props.state?.rowSelection || !props.setRowSelection) return;

		if (!Object.keys(props.state?.rowSelection).length) {
			toast.warning("Selecione um registro para deletar.");
			return;
		}

		const id = parseInt(Object.keys(props.state?.rowSelection)[0] ?? 0);
		props.setRowSelection({});
		props.handleDelete && props.handleDelete(id);
		toast.info("Registro deletado! Atualize a página para ver as alterações.");
	}, [props.state?.rowSelection]);

	const renderTopToolbarCustomActions = ({ table }: { table: MRT_TableInstance<TData> }) => (
		<Box display="flex" alignItems="center" gap={1}>
			{props.onSubmit && (
				<Button key="run" variant="contained" size="small" onClick={props.onSubmit}>
					Executar
				</Button>
			)}
			{props.toCreate && (
				<Link to={props.toCreate === true ? "add" : props.toCreate}>
					<Button key="create" variant="outlined" size="small" endIcon={<Add />}>
						Criar
					</Button>
				</Link>
			)}
			{props.toEdit && (
				<Link to={`${props.toEdit === true ? "" : props.toEdit + "/"}${Object.keys(props.state?.rowSelection ?? {})[0] ?? ""}`}>
					<Button key="edit" variant="outlined" size="small" endIcon={<Edit />}>
						Editar
					</Button>
				</Link>
			)}
			{props.handleDelete && (
				<Button key="delete" variant="outlined" size="small" endIcon={<Delete />} onClick={handleDelete}>
					Deletar
				</Button>
			)}
			{props.handleSelect && (
				<Button
					key="select"
					variant="outlined"
					size="small"
					endIcon={<Check />}
					onClick={() => props.handleSelect && props.handleSelect(table.getSelectedRowModel().rows.map((r) => r.original))}
				>
					Selecionar
				</Button>
			)}
			{props.renderTopToolbarExtraCustomActions && props.renderTopToolbarExtraCustomActions()}
		</Box>
	);

	const renderToolbarInternalActions = ({ table }: { table: MRT_TableInstance<TData> }) => [
		<MRT_ToggleGlobalFilterButton key="globalFilter" table={table} />,
		<Tooltip key="share" title="Compartilhar">
			<IconButton size="medium" aria-label="teste" onClick={handleShare}>
				<Share />
			</IconButton>
		</Tooltip>,
		<Tooltip key="clearFilters" title="Limpar filtros">
			<IconButton size="medium" onClick={handleClearFilters}>
				<ClearAll />
			</IconButton>
		</Tooltip>,
		<Tooltip key="export" title="Exportar">
			<IconButton size="medium" onClick={handleModalOpen}>
				<FileDownload />
			</IconButton>
		</Tooltip>,
		<MRT_ToggleFiltersButton key="toggleFilters" table={table} />,
		<MRT_ShowHideColumnsButton key="showHideColumns" table={table} />,
		<MRT_ToggleDensePaddingButton key="toggleDensePadding" table={table} />,
		<MRT_ToggleFullScreenButton key="toggleFullScreen" table={table} />,
	];

	useDebounced(
		() => props.onSubmit && props.onSubmit(),
		[props.state?.globalFilter, props.state?.columnFilters, props.state?.sorting, props.state?.pagination],
		200
	);

	const table = useMaterialReactTableMRT({
		columns,
		data,
		...props,

		renderTopToolbarCustomActions: props.renderTopToolbarCustomActions ?? renderTopToolbarCustomActions,
		renderToolbarInternalActions: props.renderToolbarInternalActions ?? renderToolbarInternalActions,

		//#region setStates

		onGlobalFilterChange: props.setGlobalFilter,

		manualFiltering: props.setColumnFilters ? true : false,
		onColumnFiltersChange: props.setColumnFilters,

		manualSorting: props.setSorting ? true : false,
		onSortingChange: props.setSorting,

		manualPagination: props.setPagination ? true : false,
		onPaginationChange: props.setPagination,

		enableRowSelection: props.enableRowSelection ?? false,
		getRowId: props.getRowId ?? ((row) => String(row.id)),
		onRowSelectionChange: props.setRowSelection,

		initialState: {
			showColumnFilters: true,
			density: "compact",
			...props.initialState,
		},

		state: {
			isLoading: props.isLoading ? props.isLoading() : false,
			...props.state,
		},

		//#endregion

		layoutMode: "grid",
		enableColumnResizing: props.enableColumnResizing ?? true,
		positionToolbarAlertBanner: "none",
		enableClickToCopy: true,

		rowCount: props.rowCount ?? data.length ?? undefined,

		localization: MRT_Localization_PT_BR,

		//#region Styles

		muiTablePaperProps: ({ table }) => ({
			style: {
				zIndex: table.getState().isFullScreen ? 9999 : undefined,
			},
		}),

		muiTopToolbarProps: ({ table }) => ({
			sx: () => ({
				"& .MuiIconButton-root": {
					color: `${themeName === "light" ? colors.black : colors.white}`,
				},
			}),
		}),

		muiTableProps: ({ table }) => ({
			sx: () => ({
				"& .MuiIconButton-root, .MuiSelect-icon": {
					color: `${themeName === "light" ? colors.black : colors.white}`,
				},
			}),
		}),

		muiFilterSliderProps: ({ table }) => ({
			sx: () => ({
				"& .MuiIconButton-root, .MuiSelect-icon": {
					color: `${themeName === "light" ? colors.black : colors.white}`,
				},
			}),
		}),

		muiBottomToolbarProps: ({ table }) => ({
			sx: () => ({
				"& .MuiIconButton-root, .MuiSelect-icon": {
					color: `${themeName === "light" ? colors.black : colors.white}`,
				},
			}),
		}),

		muiPaginationProps: ({ table }) => ({
			rowsPerPageOptions: [5, 10, 15, 20, 25, 30, 50, 100, 200, 500],
		}),

		//#endregion
	});

	return (
		<>
			<MaterialReactTable table={table} />
			{isOpenModal && (
				<DownloadExportDisplay
					fileName={props.title}
					head={table
						.getVisibleFlatColumns()
						.map((c) => c.columnDef)
						.map((c) => ({ id: c.id ?? "", value: c.header?.toString() ?? "" }))
						.filter((c) => c.id !== "mrt-row-select" && c.id !== "mrt-row-actions")}
					allRows={table.getRowModel().rows.map((r) => r.original)}
					selectedRows={table.getSelectedRowModel().rows.map((r) => r.original)}
				/>
			)}
		</>
	);
};
