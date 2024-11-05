import { create } from "zustand";

import { getStorageValue, setStorageValue } from "@/services";

export type key = "showColumnFilters" | "columnFilterDisplayMode";

export const getKey = (key: string): string => `@LNSF:${key}`;

type state = {
	tableProp: { [K in key]: any };

	setTableProp: (key: key, value: any) => void;
	resetTableProp: () => void;
};

export const useTableLocalStorage = create<state>((set) => ({
	tableProp: { ...getStorageValue("table", {}) },

	setTableProp: (key: key, value) => {
		const newTableProp = { ...getStorageValue("table", {}), [key]: value };
		set({ tableProp: newTableProp });
		setStorageValue("table", newTableProp);
	},

	resetTableProp: () => {
		set({ tableProp: { showColumnFilters: undefined, columnFilterDisplayMode: undefined } });
		setStorageValue("table", {});
	},
}));
