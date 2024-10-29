import { Add, Delete } from "@mui/icons-material";
import { Button, Divider, Grid2 as Grid, IconButton, TextField } from "@mui/material";
import { Controller } from "react-hook-form";

import { SelectField } from "@/components";
import { useUserFormPage } from "./useUserFormPage";

export const UserFormPage = () => {
	const {
		roles,
		role,
		setRole,
		handleSave,
		handleAddUserToRole,
		handleRemoveUserFromRole,
		control,
		errors,
		getValues,
		handleSubmit,
		register,
		watch,
	} = useUserFormPage();

	return (
		<>
			<Grid container spacing={2} component="form" onSubmit={handleSubmit(handleSave)}>
				<Grid size={{ xs: 12 }}>
					<Divider>Dados do Usuário</Divider>
				</Grid>
				<Grid size={{ xs: 12, sm: 2 }}>
					<Controller
						name="id"
						control={control}
						render={({ field }) => (
							<TextField
								label="Id"
								disabled
								{...field}
								error={!!errors.id}
								helperText={errors.id?.message}
								fullWidth
							/>
						)}
					/>
				</Grid>
				<Grid size={{ xs: 12, sm: 6 }}>
					<Controller
						name="name"
						control={control}
						render={({ field }) => (
							<TextField
								label="Nome"
								{...field}
								error={!!errors.name}
								helperText={errors.name?.message}
								fullWidth
							/>
						)}
					/>
				</Grid>
				<Grid size={{ xs: 12, sm: 4 }}>
					<Controller
						name="userName"
						control={control}
						render={({ field }) => (
							<TextField
								label="Username"
								{...field}
								error={!!errors.userName}
								helperText={errors.userName?.message}
								fullWidth
							/>
						)}
					/>
				</Grid>
				<Grid size={{ xs: 12, sm: 6 }}>
					<Controller
						name="email"
						control={control}
						render={({ field }) => (
							<TextField
								label="Email"
								{...field}
								error={!!errors.email}
								helperText={errors.email?.message}
								fullWidth
							/>
						)}
					/>
				</Grid>
				<Grid size={{ xs: 12, sm: 6 }}>
					<Controller
						name="phoneNumber"
						control={control}
						render={({ field }) => (
							<TextField
								label="Telefone"
								{...field}
								error={!!errors.phoneNumber}
								helperText={errors.phoneNumber?.message}
								fullWidth
							/>
						)}
					/>
				</Grid>
				{watch("id") ? null : (
					<>
						<Grid size={{ xs: 12, sm: 6 }}>
							<TextField
								label="Senha"
								type="password"
								{...register("password")}
								error={!!errors.password}
								helperText={errors.password?.message}
								fullWidth
							/>
						</Grid>
						<Grid size={{ xs: 12, sm: 6 }}>
							<TextField
								label="Confirmar Senha"
								type="password"
								{...register("confirmPassword", {
									validate: (value) =>
										value === getValues("password") || "As senhas devem ser iguais",
								})}
								error={!!errors.confirmPassword}
								helperText={errors.confirmPassword?.message}
								fullWidth
							/>
						</Grid>
					</>
				)}
				<Grid size={{ xs: 12 }}>
					<Button type="submit" variant="contained" color="primary" fullWidth>
						Salvar
					</Button>
				</Grid>
			</Grid>
			<Grid container spacing={2} direction="column" mt={2}>
				<Grid size={{ xs: 12 }}>
					<Divider>Permissões</Divider>
				</Grid>
				{watch("roles", []).map((role, index) => (
					<Grid container spacing={2} size={{ md: 6 }} key={index} alignItems="center">
						<Grid size={{ xs: 3, sm: 2 }}>
							<TextField label="Id Permissão" fullWidth disabled value={role.id} />
						</Grid>
						<Grid size="grow">
							<TextField label="Permissão" fullWidth disabled value={role.name} />
						</Grid>
						<Grid wrap="nowrap">
							<IconButton
								color="error"
								size="small"
								onClick={() => handleRemoveUserFromRole(+role.id!)}
							>
								<Delete />
							</IconButton>
						</Grid>
						<Grid size={{ xs: 12 }}>
							<Divider />
						</Grid>
					</Grid>
				))}
				<Grid container spacing={2} size={{ md: 6 }} alignItems="center">
					<Grid size={{ xs: 3, sm: 2 }}>
						<TextField
							label="Id Permissão"
							defaultValue={0}
							value={role?.id}
							onChange={(e) => setRole(roles.find((r) => r.id === Number(e.target.value)))}
							fullWidth
						/>
					</Grid>
					<Grid size="grow">
						<SelectField
							label="Permissão"
							options={roles}
							defaultValue={String(role?.id) || undefined}
							labelId="id"
							labelKey="name"
							valueKey="id"
							onClick={(value) => setRole(roles.find((r) => r.id === Number(value)))}
						/>
					</Grid>
					<Grid wrap="nowrap">
						<IconButton color="info" size="small" onClick={handleAddUserToRole}>
							<Add />
						</IconButton>
					</Grid>
				</Grid>
			</Grid>
		</>
	);
};
