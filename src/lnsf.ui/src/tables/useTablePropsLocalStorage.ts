import { create } from "zustand";

import { getStorageValue, setStorageValue } from "@/services";

export type key = "showColumnFilters" | "columnFilterDisplayMode";

type state = {
	tableProps: { [K in key]: any };

	setTableProp: (key: key, value: any) => void;
	resetTableProp: () => void;
};

export const useTablePropsLocalStorage = create<state>((set) => ({
	tableProps: { ...getStorageValue("table.props", {}) },

	setTableProp: (key: key, value) => {
		const newTableProp = { ...getStorageValue("table.props", {}), [key]: value };
		set({ tableProps: newTableProp });
		setStorageValue("table.props", newTableProp);
	},

	resetTableProp: () => {
		set({ tableProps: { showColumnFilters: undefined, columnFilterDisplayMode: undefined } });
		setStorageValue("table.props", {});
	},
}));
