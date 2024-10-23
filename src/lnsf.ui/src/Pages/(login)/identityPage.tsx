import { Box, Button, Link, TextField, Typography } from "@mui/material";
import { SubmitHandler, useForm } from "react-hook-form";

type Inputs = {
	username: string;
	email: string;
};

const Links = () => (
	<Box display="flex" justifyContent="space-between">
		<Link href="signin" variant="body2">
			Já possui uma conta? Entrar
		</Link>
		<Link href="signup" variant="body2">
			Não possui uma conta? Inscrever-se
		</Link>
	</Box>
);

export const IdentityPage = () => {
	const { register, handleSubmit } = useForm<Inputs>();

	const onSubmit: SubmitHandler<Inputs> = (data) => alert(data.username + " " + data.email);

	return (
		<Box component="form" onSubmit={handleSubmit(onSubmit)} width="-webkit-fill-available">
			<Typography variant="h5" align="center">
				Recuperar conta
			</Typography>
			<TextField margin="normal" fullWidth label="Nome de usuário" autoFocus {...register("username", { required: true })} />
			<TextField margin="normal" fullWidth label="Email" autoFocus {...register("email", { required: true })} />
			<Typography variant="body2" align="center" mt={1}>
				Você receberá um email com instruções para recuperar sua conta.
			</Typography>
			<Button type="submit" fullWidth variant="contained" sx={{ my: 2 }}>
				Recuperar
			</Button>
			<Links />
		</Box>
	);
};
