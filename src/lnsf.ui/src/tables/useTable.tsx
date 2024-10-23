import { parseQueryParams } from "@/services";
import { baseFilter, sortOrder } from "@/types";
import { MRT_ColumnFiltersState, MRT_PaginationState, MRT_RowSelectionState, MRT_SortingState } from "material-react-table";
import React, { createContext, Dispatch, SetStateAction, useContext, useEffect, useState } from "react";

interface StateProps {
	state: {
		globalFilter: string;
		sorting: MRT_SortingState;
		columnFilters: MRT_ColumnFiltersState;
		rowSelection: MRT_RowSelectionState;
		pagination: MRT_PaginationState;
	};
}

interface TableProps extends StateProps {
	setGlobalFilter: Dispatch<SetStateAction<string>>;
	setSorting: Dispatch<SetStateAction<MRT_SortingState>>;
	setColumnFilters: Dispatch<SetStateAction<MRT_ColumnFiltersState>>;
	setRowSelection: Dispatch<SetStateAction<MRT_RowSelectionState>>;
	setPagination: Dispatch<SetStateAction<MRT_PaginationState>>;
}

const TableContext = createContext<TableProps>({} as TableProps);

export const TableProvider = ({ children }: { children: React.ReactNode }) => {
	const queryFilters = parseQueryParams<any>(window.location.search);

	const [state, setState] = useState<TableProps["state"]>({
		globalFilter: "",
		sorting: [],
		columnFilters: [],
		rowSelection: {},
		pagination: { pageIndex: queryFilters.pageIndex || 0, pageSize: queryFilters.pageSize || 20 },
	});

	const setGlobalFilter: Dispatch<SetStateAction<string>> = (value: SetStateAction<string>) => {
		return setState((prev) => ({
			...prev,
			globalFilter: typeof value === "function" ? (value as (prevState: string) => string)(prev.globalFilter) : value,
		}));
	};

	const setSorting: Dispatch<SetStateAction<MRT_SortingState>> = (value: SetStateAction<MRT_SortingState>) => {
		return setState((prev) => ({
			...prev,
			sorting: typeof value === "function" ? (value as (prevState: MRT_SortingState) => MRT_SortingState)(prev.sorting) : value,
		}));
	};

	const setColumnFilters: Dispatch<SetStateAction<MRT_ColumnFiltersState>> = (value: SetStateAction<MRT_ColumnFiltersState>) => {
		return setState((prev) => ({
			...prev,
			columnFilters:
				typeof value === "function"
					? (value as (prevState: MRT_ColumnFiltersState) => MRT_ColumnFiltersState)(prev.columnFilters)
					: value,
		}));
	};

	const setRowSelection: Dispatch<SetStateAction<MRT_RowSelectionState>> = (value: SetStateAction<MRT_RowSelectionState>) => {
		return setState((prev) => ({
			...prev,
			rowSelection:
				typeof value === "function"
					? (value as (prevState: MRT_RowSelectionState) => MRT_RowSelectionState)(prev.rowSelection)
					: value,
		}));
	};

	const setPagination: Dispatch<SetStateAction<MRT_PaginationState>> = (value: SetStateAction<MRT_PaginationState>) => {
		return setState((prev) => ({
			...prev,
			pagination:
				typeof value === "function" ? (value as (prevState: MRT_PaginationState) => MRT_PaginationState)(prev.pagination) : value,
		}));
	};

	return (
		<TableContext.Provider
			value={{
				state,
				setGlobalFilter,
				setSorting,
				setColumnFilters,
				setRowSelection,
				setPagination,
			}}
		>
			{children}
		</TableContext.Provider>
	);
};

export const useTable = () => {
	const { ...props } = useContext(TableContext);
	return { ...props };
};

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
		filteredObject[filter.id] = filter.value;
	});

	return filteredObject;
};
