import { Box, Button, Checkbox, FormControlLabel, TextField, Typography } from "@mui/material";
import { useForm } from "react-hook-form";
import { useNavigate } from "react-router-dom";

import { useEffect } from "react";
import { login } from "@/types";
import { isInRoles, useAuthStore } from "@/store/useAuthStore";

export const SingInPage = () => {
  const { user, loginUser } = useAuthStore();

  const { register, handleSubmit } = useForm<login>({
    values: {
      username: "",
      password: "",
    },
  });

  const navigate = useNavigate();

  useEffect(() => {
    if (user?.id && user.roles.length) {
      console.debug("User is logged in", isInRoles(["voluntario"]));
      if (isInRoles(["Voluntário"])) navigate("/app/registro-diario/", { replace: true });
      else navigate("/app/", { replace: true });
    }
  }, [user]);

  return (
    <Box width="-webkit-fill-available" component="form" onSubmit={handleSubmit(loginUser)}>
      <Typography variant="h5" align="center">
        Entrar
      </Typography>
      <TextField
        margin="normal"
        fullWidth
        label="Nome de usuário"
        autoFocus
        {...register("username", { required: true })}
      />
      <TextField
        margin="normal"
        fullWidth
        label="Senha"
        type="password"
        {...register("password", { required: true })}
      />
      <FormControlLabel control={<Checkbox value="remember" />} label="Lembrar-me" />
      <Button type="submit" fullWidth variant="contained" sx={{ my: 2 }}>
        Entrar
      </Button>
    </Box>
  );
};
