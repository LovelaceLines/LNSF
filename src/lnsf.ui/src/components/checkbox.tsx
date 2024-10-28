import {
	Checkbox as CheckboxMUI,
	CheckboxProps as CheckboxPropsMUI,
	FormControlLabel,
	SxProps,
	Theme,
} from "@mui/material";
import { UseFormRegisterReturn } from "react-hook-form";

export interface CheckboxProps extends CheckboxPropsMUI {
	label: string;
	register?: UseFormRegisterReturn;
	sxFormControl?: SxProps<Theme>;
}

export const Checkbox = ({ label, register, sxFormControl, ...rest }: CheckboxProps): JSX.Element => {
	return (
		<FormControlLabel
			control={<CheckboxMUI {...register} {...rest} />}
			label={label}
			sx={sxFormControl}
		/>
	);
};
