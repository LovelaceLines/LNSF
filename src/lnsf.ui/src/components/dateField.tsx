import { SxProps, TextField, Theme } from "@mui/material";
import React from "react";
import { UseFormRegisterReturn } from "react-hook-form";

export interface DateFieldProps {
  label: string;
  value?: string;
  register: UseFormRegisterReturn;
  error?: boolean;
  helperText?: string;
  sx?: SxProps<Theme> | undefined;
}

export const DateField: React.FC<DateFieldProps> = ({
  value,
  label,
  register,
  error,
  helperText,
  sx,
}): JSX.Element => {
  return (
    <TextField
      fullWidth
      label={label}
      type="date"
      value={value}
      {...register}
      error={error}
      helperText={helperText}
      slotProps={{
        inputLabel: {
          shrink: true,
        },
      }}
      sx={sx}
    />
  );
};

export const DateTimeField: React.FC<DateFieldProps> = ({
  value,
  label,
  register,
  error,
  helperText,
  sx,
}): JSX.Element => {
  return (
    <TextField
      fullWidth
      label={label}
      type="datetime-local"
      value={value}
      {...register}
      error={error}
      helperText={helperText}
      slotProps={{
        inputLabel: {
          shrink: true,
        },
      }}
      sx={sx}
    />
  );
};
