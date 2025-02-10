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

export type key =
	| "emergencyContact"
	| "escort"
	| "hospital"
	| "hosting"
	| "log"
	| "notification"
	| "patient"
	| "peopleRoomHosting"
	| "people"
	| "room"
	| "tour"
	| "treatment"
	| "user";

export interface StateProps {
	state: {
		globalFilter: string;
		sorting: MRT_SortingState;
		columnFilters: MRT_ColumnFiltersState;
		columnOrder: MRT_ColumnOrderState;
		rowSelection?: MRT_RowSelectionState;
		pagination: MRT_PaginationState;
		columnSizing: MRT_ColumnSizingState;
		columnVisibility?: MRT_VisibilityState;
		grouping: MRT_GroupingState;
	};
}
