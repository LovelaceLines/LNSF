import { Controller } from "react-hook-form";

import { useCurrentUserFormPage } from "./useCurrentUserFormPage";
import { Button, Divider, Grid2 as Grid, TextField } from "@mui/material";

export const CurrentUserFormPage = () => {
	const { control, errors, getValues, handleSave, handleSubmit, handlePassword } = useCurrentUserFormPage();

	return (
		<>
			<Grid container spacing={2} component="form" onSubmit={handleSubmit(handleSave)}>
				<Grid size={12}>
					<Divider>Minha Conta</Divider>
				</Grid>
				<Grid size={{ xs: 12, sm: 2 }}>
					<Controller name="id" control={control} render={({ field }) => <TextField {...field} label="Id" fullWidth />} />
				</Grid>
				<Grid size={{ xs: 12, sm: 6 }}>
					<Controller name="name" control={control} render={({ field }) => <TextField {...field} label="Name" fullWidth />} />
				</Grid>
				<Grid size={{ xs: 12, sm: 4 }}>
					<Controller
						name="userName"
						control={control}
						render={({ field }) => <TextField {...field} label="Username" fullWidth />}
					/>
				</Grid>
				<Grid size="grow">
					<Controller name="email" control={control} render={({ field }) => <TextField {...field} label="Email" fullWidth />} />
				</Grid>
				<Grid size="grow">
					<Controller
						name="phoneNumber"
						control={control}
						render={({ field }) => <TextField {...field} label="Telefone" fullWidth />}
					/>
				</Grid>
				<Grid size={{ xs: 12 }}>
					<Button type="submit" variant="contained" color="primary" fullWidth>
						Salvar
					</Button>
				</Grid>
			</Grid>
			<Grid container spacing={2} mt={2} component="form" onSubmit={handleSubmit(handlePassword)}>
				<Grid size={{ xs: 12 }}>
					<Divider>Alterar Senha</Divider>
				</Grid>
				<Grid size={{ xs: 12, sm: 4 }}>
					<Controller
						name="password"
						control={control}
						render={({ field }) => <TextField type="password" {...field} label="Senha Atual" fullWidth />}
					/>
				</Grid>
				<Grid size={{ xs: 12, sm: 4 }}>
					<Controller
						name="newPassword"
						control={control}
						render={({ field }) => <TextField type="password" {...field} label="Nova Senha" fullWidth />}
					/>
				</Grid>
				<Grid size={{ xs: 12, sm: 4 }}>
					<Controller
						name="confirmPassword"
						control={control}
						rules={{
							validate: (value) => value === getValues("newPassword") || "As senhas devem ser iguais",
						}}
						render={({ field }) => (
							<TextField
								type="password"
								{...field}
								label="Confirmar Senha"
								error={!!errors.confirmPassword}
								helperText={errors.confirmPassword?.message}
								fullWidth
							/>
						)}
					/>
				</Grid>
				<Grid size={12}>
					<Button type="submit" variant="outlined" color="primary" fullWidth>
						Alterar Senha
					</Button>
				</Grid>
			</Grid>
		</>
	);
};
