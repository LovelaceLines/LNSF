import {
	MaterialReactTable,
	MRT_Row,
	MRT_ShowHideColumnsButton,
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
import { useCallback } from "react";
import { Link } from "react-router-dom";
import { toast } from "react-toastify";

import { DownloadExportDisplay } from "./components/download-export-display/downloadExportDisplay";
import { useModal } from "@/contexts";
import { useDebounced } from "@/hooks";
import { colors, useThemeContext } from "@/theme";
import { useTablePropsLocalStorage } from "./useTablePropsLocalStorage";
import { key } from "./types";
import { useTableState } from "./useTableState";

interface Props<TData extends MRT_RowData> extends MRT_TableOptions<TData> {
	columns: MRT_ColumnDef<TData>[];
	data: TData[];
	rowCount?: number;
	getRowId?: ((originalRow: TData, index: number, parentRow: MRT_Row<TData>) => string) | undefined;
	initialState?: Partial<MRT_TableState<TData>>;
	enableRowSelection?: boolean;
	onSubmit?: () => void;
	isLoading?: () => boolean;
	id: key;
	title: string;
	toCreate?: string | boolean;
	toEdit?: string | boolean;
	handleDelete?: (id: number) => void;
	handleSelect?: (ids: TData[]) => void;
	renderTopToolbarExtraCustomActions?: () => JSX.Element;
	renderTopToolbarFilterActions?: () => JSX.Element;
	renderToolbarExtraInternalActions?: () => JSX.Element;
}

export const useMaterialReactTable = <TData extends MRT_RowData>({
	id,
	columns,
	data,
	...props
}: Props<TData>) => {
	const { isOpen: isOpenModal, handleModalOpen } = useModal("download-export-display");
	const { tableProps } = useTablePropsLocalStorage();
	const {
		state,
		setColumnFilters,
		setGlobalFilter,
		setSorting,
		setPagination,
		setColumnOrder,
		setRowSelection,
		setColumnSizing,
		setColumnVisibility,
		setGrouping,
	} = useTableState();
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
		setGlobalFilter(id, "");
		setColumnFilters(id, []);
		setSorting(id, []);
		setColumnSizing(id, {});
	}, []);

	const handleDelete = useCallback(() => {
		if (!state[id].rowSelection) return;

		if (!Object.keys(state[id].rowSelection).length) {
			toast.warning("Selecione um registro para deletar.");
			return;
		}

		const row_id = parseInt(Object.keys(state[id].rowSelection)[0] ?? 0);
		setRowSelection(id, {});
		props.handleDelete && props.handleDelete(row_id);
		toast.info("Registro deletado! Atualize a página para ver as alterações.");
	}, [state[id].rowSelection]);

	const renderTopToolbarCustomActions = ({ table }: { table: MRT_TableInstance<TData> }) => (
		<Box display="flex" flexDirection="column" gap={1}>
			<Box display="flex" alignItems="center" gap={1}>
				{props.renderTopToolbarFilterActions && props.renderTopToolbarFilterActions()}
			</Box>
			<Box display="flex" alignItems="center" gap={1}>
				{props.onSubmit && (
					<Button key="run" variant="contained" size="small" onClick={props.onSubmit}>
						Executar
					</Button>
				)}
				{props.toCreate && (
					<Link to={props.toCreate === true ? "add" : props.toCreate}>
						<Button key="create" variant="outlined" size="small" color="info" endIcon={<Add />}>
							Criar
						</Button>
					</Link>
				)}
				{props.toEdit && (
					<Link
						to={`${props.toEdit === true ? "" : props.toEdit + "/"}${
							Object.keys(state[id].rowSelection ?? {})[0] ?? ""
						}`}
					>
						<Button
							key="edit"
							variant="outlined"
							size="small"
							color="warning"
							endIcon={<Edit />}
							disabled={!table.getIsSomeRowsSelected()}
						>
							Editar
						</Button>
					</Link>
				)}
				{props.handleDelete && (
					<Button
						key="delete"
						variant="outlined"
						size="small"
						color="error"
						endIcon={<Delete />}
						onClick={handleDelete}
						disabled={!table.getIsSomeRowsSelected()}
					>
						Deletar
					</Button>
				)}
				{props.handleSelect && (
					<Button
						key="select"
						variant="outlined"
						size="small"
						endIcon={<Check />}
						onClick={() =>
							props.handleSelect &&
							props.handleSelect(table.getSelectedRowModel().rows.map((r) => r.original))
						}
					>
						Selecionar
					</Button>
				)}
				{props.renderTopToolbarExtraCustomActions && props.renderTopToolbarExtraCustomActions()}
			</Box>
		</Box>
	);

	const renderToolbarInternalActions = ({ table }: { table: MRT_TableInstance<TData> }) => [
		<MRT_ToggleGlobalFilterButton key="globalFilter" table={table} />,
		props.renderToolbarExtraInternalActions && props.renderToolbarExtraInternalActions(),
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
		[state[id].globalFilter, state[id].columnFilters, state[id].sorting, state[id].pagination],
		200
	);

	const table = useMaterialReactTableMRT({
		columns,
		data,
		...props,

		renderTopToolbarCustomActions: renderTopToolbarCustomActions,
		renderToolbarInternalActions: renderToolbarInternalActions,

		//#region setStates

		onGlobalFilterChange: (value) => setGlobalFilter(id, value),

		manualFiltering: true,
		onColumnFiltersChange: (updaterValue) => {
			const value =
				typeof updaterValue === "function" ? updaterValue(state[id].columnFilters) : updaterValue;
			setColumnFilters(id, value);
		},

		manualSorting: true,
		onSortingChange: (updaterValue) => {
			const value = typeof updaterValue === "function" ? updaterValue(state[id].sorting) : updaterValue;
			setSorting(id, value);
		},

		manualPagination: true,
		onPaginationChange: (updaterValue) => {
			const value =
				typeof updaterValue === "function"
					? updaterValue(state[id].pagination ?? { pageIndex: 0, pageSize: 20 })
					: updaterValue;
			setPagination(id, value);
		},

		onColumnOrderChange: (updaterValue) => {
			const value =
				typeof updaterValue === "function" ? updaterValue(state[id].columnOrder) : updaterValue;
			setColumnOrder(id, value);
		},

		enableRowSelection: props.enableRowSelection ?? false,
		getRowId: props.getRowId ?? ((row) => String(row.id)),
		onRowSelectionChange: (updaterValue) => {
			const value =
				typeof updaterValue === "function"
					? updaterValue(state[id].rowSelection ?? {})
					: updaterValue;
			setRowSelection(id, value);
		},

		onColumnSizingChange: (updaterValue) => {
			const value =
				typeof updaterValue === "function" ? updaterValue(state[id].columnSizing) : updaterValue;
			setColumnSizing(id, value);
		},

		enableHiding: true,
		onColumnVisibilityChange: (updaterValue) => {
			const value =
				typeof updaterValue === "function"
					? updaterValue(state[id].columnVisibility ?? {})
					: updaterValue;
			setColumnVisibility(id, value);
		},

		enableGrouping: true,
		onGroupingChange: (updaterValue) => {
			const value =
				typeof updaterValue === "function" ? updaterValue(state[id].grouping ?? []) : updaterValue;
			setGrouping(id, value);
		},

		initialState: {
			showColumnFilters: tableProps.showColumnFilters ?? undefined,
			density: "compact",
			pagination: {
				pageIndex: 0,
				pageSize: 99999,
			},
			...props.initialState,
		},

		state: {
			isLoading: props.isLoading ? props.isLoading() : false,
			...state[id],
		},

		//#endregion

		layoutMode: "grid",
		enableColumnResizing: props.enableColumnResizing ?? true,
		positionToolbarAlertBanner: "none",

		rowCount: props.rowCount ?? data.length ?? undefined,

		localization: MRT_Localization_PT_BR,

		columnFilterDisplayMode: tableProps.columnFilterDisplayMode ?? undefined,

		enableColumnOrdering: true,

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
			rowsPerPageOptions: [5, 10, 15, 20, 25, 30, 50, 100, 200, 500, 1000],
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
