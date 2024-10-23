import { Box, Button, Checkbox, FormControlLabel, Link, TextField, Typography } from "@mui/material";
import { SubmitHandler, useForm } from "react-hook-form";

type Inputs = {
	username: string;
	email: string;
	password: string;
};

const Links = () => (
	<Box display="flex" justifyContent="space-between">
		<Link href="signin" variant="body2">
			Possui uma conta? Entrar
		</Link>
	</Box>
);

export const SingUpPage = () => {
	const { register, handleSubmit } = useForm<Inputs>();

	const onSubmit: SubmitHandler<Inputs> = (data) => alert(data.username + " " + data.email + " " + data.password);

	return (
		<Box component="form" onSubmit={handleSubmit(onSubmit)} width="-webkit-fill-available">
			<Typography variant="h5" align="center">
				Inscrever-se
			</Typography>
			<TextField
				autoComplete="username"
				margin="normal"
				fullWidth
				label="Nome de usuário"
				autoFocus
				{...register("username", { required: true })}
			/>
			<TextField margin="normal" fullWidth label="Email" autoFocus {...register("email", { required: true })} />
			<TextField margin="normal" fullWidth label="Senha" type="password" {...register("password", { required: true })} />
			<FormControlLabel control={<Checkbox value="allowExtraEmails" />} label="Eu quero receber notícias e ofertas por email." />
			<Button type="submit" fullWidth variant="contained" sx={{ my: 2 }}>
				Inscrever-se
			</Button>
			<Links />
		</Box>
	);
};
