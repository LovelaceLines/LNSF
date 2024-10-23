import { FormControl, InputLabel, Select, MenuItem, IconButton } from "@mui/material";
import { ArrowDropDown, Clear } from "@mui/icons-material";
import { useEffect, useState } from "react";
import { v4 as uuidv4 } from "uuid";

export interface SelectFieldProps {
	label: string;
	labelId: string;
	options: any[];
	defaultValue?: string;
	valueKey: string;
	labelKey: string;
	onClick: (value: any) => void;
}

export const SelectField: React.FC<SelectFieldProps> = ({ label, labelId, labelKey, options, defaultValue, valueKey, onClick }) => {
	const [selectedValue, setSelectedValue] = useState<string | null>(defaultValue || null);

	useEffect(() => {
		if (defaultValue) setSelectedValue(defaultValue);
	}, [defaultValue]);

	const handleSelect = (value: React.SetStateAction<any>) => {
		setSelectedValue(value);
		onClick(value);
	};

	const handleClear = () => {
		setSelectedValue(null);
		onClick(null);
	};

	return (
		<FormControl
			sx={{
				width: "100%",
				"& .MuiSvgIcon-root": { color: "inherit" },
				"& .MuiSelect-icon": { color: "inherit" },
			}}
		>
			<InputLabel id={labelId}>{label}</InputLabel>
			<Select labelId={labelId} label={label} IconComponent={selectedValue ? Clear : ArrowDropDown} value={selectedValue}>
				<MenuItem value="" disabled>
					{label}
				</MenuItem>
				{options.map((option) => (
					<MenuItem
						key={`${getNestedProperty(option, valueKey)}-${uuidv4()}`}
						value={getNestedProperty(option, valueKey)}
						onClick={() => handleSelect(getNestedProperty(option, valueKey))}
					>
						{getNestedProperty(option, labelKey)}
					</MenuItem>
				))}
			</Select>
			{selectedValue && (
				<IconButton
					id="clear-button"
					onClick={handleClear}
					sx={{
						position: "absolute",
						right: 0,
						top: "50%",
						transform: "translateY(-50%)",
					}}
				>
					<Clear />
				</IconButton>
			)}
		</FormControl>
	);
};

const getNestedProperty = (obj: any, path: string) => {
	return path.split(".").reduce((acc, part) => acc && acc[part], obj);
};
