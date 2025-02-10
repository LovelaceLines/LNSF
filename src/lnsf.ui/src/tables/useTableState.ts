import {
	MRT_ColumnFiltersState,
	MRT_ColumnOrderState,
	MRT_ColumnSizingState,
	MRT_GroupingState,
	MRT_PaginationState,
	MRT_RowSelectionState,
	MRT_SortingState,
	MRT_VisibilityState,
} from "material-react-table";
import { create } from "zustand";

import { baseFilter, sortOrder } from "@/types";
import { dateOnlyToStr, dateTimeToStr, strIsDateOnly, strIsDateTime } from "@/utils";
import { key, StateProps } from "./types";
import { getTableState, setTableState } from "./useTableStateLocalStorage";
import { keys } from "./consts";

type state = {
	state: {
		[id in key]: StateProps["state"];
	};
	setGlobalFilter: (id: key, value: string) => void;
	setSorting: (id: key, value: MRT_SortingState) => void;
	setColumnFilters: (id: key, value: MRT_ColumnFiltersState) => void;
	setRowSelection: (id: key, value: MRT_RowSelectionState) => void;
	setColumnOrder: (id: key, value: MRT_ColumnOrderState) => void;
	setPagination: (id: key, value: MRT_PaginationState) => void;
	setColumnSizing: (id: key, value: MRT_ColumnSizingState) => void;
	setColumnVisibility: (id: key, value: MRT_VisibilityState) => void;
	setGrouping: (id: key, value: MRT_GroupingState) => void;
};

const initialState = (): state["state"] => {
	const initial: state["state"] = {} as state["state"];
	keys.forEach((k: key) => {
		initial[k] = {
			globalFilter: "",
			sorting: [],
			columnFilters: [],
			columnOrder: [],
			pagination: { pageIndex: 0, pageSize: 20 },
			columnSizing: {},
			columnVisibility: {},
			rowSelection: {},
			grouping: [],
			...getTableState[k],
		};
	});
	return initial;
};

export const useTableState = create<state>((set, get) => ({
	state: initialState(),

	setGlobalFilter: (id, value) =>
		set((_state) => ({
			state: {
				..._state.state,
				[id]: {
					..._state.state[id],
					globalFilter: value,
				},
			},
		})),

	setSorting: (id, value) =>
		set((_state) => ({
			state: {
				..._state.state,
				[id]: {
					..._state.state[id],
					sorting: value,
				},
			},
		})),

	setColumnFilters: (id, value) =>
		set((_state) => ({
			state: {
				..._state.state,
				[id]: {
					..._state.state[id],
					columnFilters: value,
				},
			},
		})),

	setRowSelection: (id, value) =>
		set((_state) => ({
			state: {
				..._state.state,
				[id]: {
					..._state.state[id],
					rowSelection: value,
				},
			},
		})),

	setColumnOrder: (id, value) => {
		setTableState(id, { ...get().state[id], columnOrder: value });
		set((_state) => ({
			state: {
				..._state.state,
				[id]: {
					..._state.state[id],
					columnOrder: value,
				},
			},
		}));
	},

	setPagination: (id, value) => {
		setTableState(id, { ...get().state[id], pagination: value });
		set((_state) => ({
			state: {
				..._state.state,
				[id]: {
					..._state.state[id],
					pagination: value,
				},
			},
		}));
	},

	setColumnSizing: (id, value) => {
		setTableState(id, { ...get().state[id], columnSizing: value });
		set((_state) => ({
			state: {
				..._state.state,
				[id]: {
					..._state.state[id],
					columnSizing: value,
				},
			},
		}));
	},

	setColumnVisibility: (id, value) => {
		setTableState(id, { ...get().state[id], columnVisibility: value });
		set((_state) => ({
			state: {
				..._state.state,
				[id]: {
					..._state.state[id],
					columnVisibility: value,
				},
			},
		}));
	},

	setGrouping: (id, value) => {
		setTableState(id, { ...get().state[id], grouping: value });
		set((_state) => ({
			state: {
				..._state.state,
				[id]: {
					..._state.state[id],
					grouping: value,
				},
			},
		}));
	},
}));

interface FilteredObject extends baseFilter {
	[key: string]: any;
}

export const getFilteredObject = ({ state }: StateProps): FilteredObject => {
	const filteredObject: FilteredObject = {
		sort: state.sorting.map((s) => s.id)[0],
		sortBy: state.sorting.map((s) => (s.desc ? sortOrder.desc : sortOrder.asc))[0],
		page: state.pagination.pageIndex + 1,
		perPage: state.pagination.pageSize,
	};

	state.columnFilters.forEach((filter) => {
		// Caso o filtro seja um range de datas
		if (filter.value instanceof Array && filter.value.length === 2) {
			if (strIsDateOnly(filter.value[0]) && strIsDateOnly(filter.value[1])) {
				filteredObject[`${filter.id}.min`] = dateOnlyToStr(filter.value[0], ".Net");
				filteredObject[`${filter.id}.max`] = dateOnlyToStr(filter.value[1], ".Net");
			} else if (strIsDateTime(filter.value[0]) && strIsDateTime(filter.value[1])) {
				filteredObject[`${filter.id}.min`] = dateTimeToStr(filter.value[0], ".Net");
				filteredObject[`${filter.id}.max`] = dateTimeToStr(filter.value[1], ".Net");
			}
		} else {
			filteredObject[filter.id] = filter.value;
		}
	});

	return filteredObject;
};
