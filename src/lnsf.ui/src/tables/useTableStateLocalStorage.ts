import { create } from "zustand";

import { getStorageValue, setStorageValue } from "@/services";
import { key } from "./types";

type state = {
	tableState: { [K in key]: any };

	setTableState: (key: key, value: any) => void;
};

export const getTableState = getStorageValue("table.state", {});

export const setTableState = (key: key, value: any) =>
	setStorageValue("table.state", { ...getTableState, [key]: value });

export const useTableStateLocalStorage = create<state>((set) => ({
	tableState: getTableState,

	setTableState: (key: key, value) => {
		const newTableProp = { ...getTableState, [key]: value };
		set({ tableState: newTableProp });
		setStorageValue("table.state", newTableProp);
	},
}));
