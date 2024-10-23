import { Box, Button, Checkbox, FormControlLabel, Link, TextField, Typography } from "@mui/material";
import { SubmitHandler, useForm } from "react-hook-form";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";

import { useSnackbar } from "@/contexts";
import { loginUser } from "@/redux/features/auth/thunks";
import { AppDispatch } from "@/redux/store";
import { selectError, selectStatus } from "@/redux/features/auth/slice";
import { useEffect } from "react";
import { login } from "@/types";

const Links = () => (
	<Box display="flex" justifyContent="space-between">
		<Link href="identity" variant="body2">
			Esqueceu a senha?
		</Link>
		<Link href="signup" variant="body2">
			Não possui uma conta? Inscrever-se
		</Link>
	</Box>
);

export const SingInPage = () => {
	const navigate = useNavigate();
	const dispatch = useDispatch<AppDispatch>();
	const error = useSelector(selectError);
	const status = useSelector(selectStatus);
	const snackbar = useSnackbar();

	const { register, handleSubmit } = useForm<login>();

	const onSubmit: SubmitHandler<login> = (data) => dispatch(loginUser(data));

	useEffect(() => {
		if (status === "failed" && error) snackbar.Snackbar(error);
		if (status === "succeeded") navigate("/app");
	}, [status]);

	return (
		<Box component="form" onSubmit={handleSubmit(onSubmit)} width="-webkit-fill-available">
			<Typography variant="h5" align="center">
				Entrar
			</Typography>
			<TextField
				margin="normal"
				fullWidth
				label="Nome de usuário"
				autoFocus
				{...register("username", { required: true })}
				autoComplete="username"
			/>
			<TextField
				margin="normal"
				fullWidth
				label="Senha"
				type="password"
				{...register("password", { required: true })}
				autoComplete="current-password"
			/>
			<FormControlLabel control={<Checkbox value="remember" />} label="Lembrar-me" />
			<Button type="submit" fullWidth variant="contained" sx={{ my: 2 }}>
				Entrar
			</Button>
			<Links />
		</Box>
	);
};
