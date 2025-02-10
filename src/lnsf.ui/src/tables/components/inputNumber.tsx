import { Close } from "@mui/icons-material";
import { IconButton, Input, Tooltip } from "@mui/material";
import { MRT_Column, MRT_RowData } from "material-react-table";
import { useState } from "react";

interface MRTInputNumberProps<TData extends MRT_RowData> {
  column: MRT_Column<TData>;
}

export const MRTInputNumber = <TData extends MRT_RowData>({ column }: MRTInputNumberProps<TData>) => {
  const [value, setValue] = useState(column.getFilterValue() || undefined);

  return (
    <Input
      type="number"
      fullWidth
      value={value}
      onChange={(e) => {
        column.setFilterValue(e.target.value);
        setValue(e.target.value);
      }}
      placeholder={`Filtrar por ${column.columnDef.header}`}
      endAdornment={
        <Tooltip title="Limpar filtros" placement="right">
          <IconButton
            size="small"
            onClick={() => {
              column.setFilterValue("");
              setValue("");
            }}
            sx={{ m: 0, p: 0 }}
            disabled={!column.getFilterValue()}
          >
            <Close />
          </IconButton>
        </Tooltip>
      }
    />
  );
};
