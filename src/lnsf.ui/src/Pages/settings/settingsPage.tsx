import {
	Button,
	Divider,
	FormControlLabel,
	Grid2 as Grid,
	IconButton,
	Switch,
	Typography,
} from "@mui/material";

import { SelectField } from "@/components";
import { useTablePropsLocalStorage } from "@/tables";
import { useSettingsPage } from "./useSettingsPage";
import { useThemeContext } from "@/theme";
import { Brightness4, Brightness5 } from "@mui/icons-material";

export const SettingsPage = () => {
	const { tableProps, setTableProp, resetTableProp } = useTablePropsLocalStorage();
	const { columnFilterDisplayModeOptions } = useSettingsPage();
	const { themeName, toggleTheme } = useThemeContext();

	return (
		<Grid container spacing={2}>
			<Grid container spacing={2} size={12} alignItems="center">
				<Grid size={12}>
					<Divider>Tabelas</Divider>
				</Grid>
				<Grid size={{ xs: 12, sm: 6, md: 3 }}>
					<SelectField
						label="Estilo de exibição de Filtros"
						labelId="label"
						defaultValue={tableProps.columnFilterDisplayMode ?? "default"}
						valueKey="value"
						labelKey="label"
						options={columnFilterDisplayModeOptions}
						onClick={(value) =>
							setTableProp("columnFilterDisplayMode", value === "default" ? undefined : value)
						}
					/>
				</Grid>
				<Grid size={{ xs: 12, sm: 6, md: 4 }}>
					<FormControlLabel
						control={
							<Switch
								defaultChecked={tableProps.showColumnFilters}
								checked={tableProps.showColumnFilters ? true : false}
								onChange={() =>
									setTableProp("showColumnFilters", !tableProps.showColumnFilters)
								}
							/>
						}
						label="Exibir filtros por padrão"
						labelPlacement="start"
					/>
				</Grid>
				<Grid size={12} textAlign="center">
					<Button variant="outlined" onClick={resetTableProp}>
						Resetar Configurações de Tabela
					</Button>
				</Grid>
			</Grid>
			<Grid container spacing={2} size={12} alignItems="center">
				<Grid size={12}>
					<Divider>Tema</Divider>
				</Grid>
				<Grid size={12} display="flex" alignItems="center" direction="row">
					<Typography>Trocar tema</Typography>
					<IconButton onClick={toggleTheme} color="inherit" sx={{ display: "flex" }}>
						{themeName === "light" ? <Brightness4 /> : <Brightness5 />}
					</IconButton>
				</Grid>
			</Grid>
		</Grid>
	);
};
