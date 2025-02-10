import { MRT_TableOptions } from "material-react-table";

export const useSettingsPage = () => {
  const columnFilterDisplayModeOptions = [
    {
      value: "default",
      label: "Padrão",
    },
    {
      value: "popover",
      label: "Popover",
    },
    {
      value: "subheader",
      label: "Sub Cabeçalho",
    },
  ] as {
    value: MRT_TableOptions<any>["columnFilterDisplayMode"];
    label: string;
  }[];

  return {
    columnFilterDisplayModeOptions,
  };
};
