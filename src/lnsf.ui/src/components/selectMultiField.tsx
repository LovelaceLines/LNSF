import { Autocomplete, Checkbox, FormControl, TextField } from "@mui/material";

export interface SelectMultiFieldProps {
	label: string;
	labelKey: string;
	options: any[];
	defaultValue?: any[];
	onChance: (value: any[]) => void;
	limitTags?: number;
}

export const SelectMultiField: React.FC<SelectMultiFieldProps> = ({ label, options, limitTags, defaultValue, labelKey, onChance }) => {
	return (
		<FormControl
			sx={{
				width: "100%",
				"& .MuiIconButton-root": { color: "inherit" },
			}}
		>
			<Autocomplete
				multiple
				limitTags={limitTags ?? 0}
				disableCloseOnSelect
				defaultValue={defaultValue}
				options={options}
				getOptionLabel={(option) => option[labelKey]}
				renderOption={({ key, ...props }, option, { selected }) => (
					<li key={key} {...props}>
						<Checkbox checked={selected} />
						{option[labelKey]}
					</li>
				)}
				onChange={(_, value) => onChance(value)}
				renderInput={(params) => <TextField {...params} variant="outlined" label={label} />}
			/>
		</FormControl>
	);
};
