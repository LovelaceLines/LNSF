import { Close } from "@mui/icons-material";
import { IconButton, Input, Tooltip } from "@mui/material";
import { MRT_Column, MRT_RowData } from "material-react-table";
import { useState } from "react";

interface MRTInputDateOnlyProps<TData extends MRT_RowData> {
  column: MRT_Column<TData>;
  rangeFilterIndex?: number;
}

export const MRTInputDateOnly = <TData extends MRT_RowData>({
  column,
  rangeFilterIndex,
}: MRTInputDateOnlyProps<TData>) => {
  const [value, setValue] = useState(column.getFilterValue() || undefined);

  return (
    <Input
      type="date"
      fullWidth
      value={value}
      onChange={(e) => {
        if (rangeFilterIndex === undefined) column.setFilterValue(e.target.value);
        else {
          const newValue = column.getFilterValue() || [];
          newValue[rangeFilterIndex] = e.target.value;
          column.setFilterValue(newValue);
        }
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

export const MRTInputDateTime = <TData extends MRT_RowData>({
  column,
  rangeFilterIndex,
}: MRTInputDateOnlyProps<TData>) => {
  const [value, setValue] = useState(column.getFilterValue() || undefined);

  return (
    <Input
      type="datetime-local"
      fullWidth
      value={value}
      onChange={(e) => {
        if (rangeFilterIndex === undefined) column.setFilterValue(e.target.value);
        else {
          const newValue = column.getFilterValue() || [];
          newValue[rangeFilterIndex] = e.target.value;
          column.setFilterValue(newValue);
        }
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
