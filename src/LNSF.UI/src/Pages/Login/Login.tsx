import { Box, Button, CircularProgress, TextField, Typography, useMediaQuery, useTheme } from "@mui/material";
import { useContext, useEffect, useState } from "react";
import * as yup from 'yup';
import { AuthContext } from "../../Contexts/authcontext/AuthContext_";
import nomelogo from '../../assets/lnsf_icone.svg';
import fundo from '../../assets/fundoLogin.jpg';
import { useNavigate } from 'react-router-dom'
import { Link as MaterialUILink } from '@mui/material';
import React from "react";
import { iDataLogin } from "../../Contexts";

const loginSchema = yup.object().shape({
  userName: yup.string().required("Nome de usuário é um campo obrigatório!"),
  password: yup.string().required("Senha é um campo obrigatório!"),
})

export const LoginPage: React.FC = () => {
  const [userName, setUserName] = useState('');
  const [password, setPassword] = useState('');
  const [userNameError, setUserNameError] = useState('');
  const [passwordError, setPasswordError] = useState('');

  const [isLoading, setIsLoading] = useState(false);
  const theme = useTheme();
  const smDown = useMediaQuery(theme.breakpoints.down('sm'));
  
  const [showPassword, setShowPassword] = React.useState(false);
  const handleClickShowPassword = () => setShowPassword(show => !show);
  
  const { isAuthenticated, login } = useContext(AuthContext);
  const navigate = useNavigate();
  useEffect(() => {
    if (isAuthenticated) { navigate("/inicio") }
  }, [isAuthenticated, navigate]);

  // TODO: Corrigir setIsLoading(false) quando o login falhar (não está funcionando)
  const handlesubimit = () => {
    setIsLoading(true)

    loginSchema.validate({ userName, password }, { abortEarly: false })
    .then(dadosValidados => {
      const data: iDataLogin = {
        userName: dadosValidados.userName,
        password: dadosValidados.password,
      }
      login(data)
    })
    .catch((errors: yup.ValidationError) => {
      errors.inner.forEach(error => {
        if (error.path === 'userName') setUserNameError(error.message);
        else if (error.path === 'password') setPasswordError(error.message);
      });
    });
    
    setIsLoading(false);
  };

  const header = () => (
    <Box>
      <Box
        padding={theme.spacing(3)}
        display='flex'
        alignItems='center'
        justifyContent='center'
      >
        <img src={nomelogo} />
      </Box>
      <Box>
        <Typography variant="h6" align="center"
          fontSize='1.5rem'
          fontStyle='normal'
          fontWeight='400'
          lineHeight='normal'
        >
          Seja bem-vindo ao nosso lar digital!
        </Typography>
        <Typography
          variant="subtitle1"
          align="center"
          fontSize='1rem'
          fontStyle='normal'
          fontWeight='400'
          lineHeight='normal'
          letterSpacing='0.00938rem'
          display='flex'
          flexDirection='column'
        >
          Estamos muito felizes em recebê-lo! 
          <br />
          Deixe tudo conosco, iremos facilitar todo o seu trabalho ❤️
        </Typography>
      </Box>
    </Box>
  );
  
  const loginForm = () => (
    <Box component="form" onSubmit={handlesubimit}>
      <Box display='flex' flexDirection='column' gap={2}>
        <TextField
          id="userName"
          label='Nome de Usuário'
          type="text"
          value={userName}
          error={!!userNameError}
          helperText={userNameError}
          onKeyUp={() => setUserNameError('')}
          onChange={e => setUserName(e.target.value)}
        />
        <Box display='flex' flexDirection='column' gap={1}>
        <TextField
          id="Senha"
          label='Senha'
          type={showPassword ? 'text' : 'password'}
          value={password}
          error={!!passwordError}
          onKeyUp={() => setPasswordError('')}
          onChange={e => setPassword(e.target.value)}
          helperText={passwordError}
        />
        <Box display='flex' justifyContent='left'>
          <Button 
            size="small"
            onClick={handleClickShowPassword}  
          >
            {showPassword ? 'Esconder senha' : 'Mostrar senha'}
          </Button>
        </Box>
        </Box>
        <Box display='flex' justifyContent='right'>
          <MaterialUILink href="https://www.exemplo.com" target="_blank" rel="noopener">
            Pedir ajuda
          </MaterialUILink>
        </Box>
        <Button
          variant="contained"
          size="large"
          onClick={handlesubimit}
          fullWidth
        >
          {isLoading ? <CircularProgress color="info"/> : 'Entrar'}
        </Button>
      </Box>      
    </Box>
  );

  return (
    <Box
      width='100%'
      height='100vh'
      display='flex'
      flexDirection={smDown ? 'column' : 'row'}
      alignItems='center'
      bgcolor={'white'}
      gap='2rem'
    >
      <Box width={smDown ? '100%' : '50%'} style={{ height: '100%' }}>
        <img src={fundo} style={{ width: '100%', height: '100%', objectFit: 'cover' }} />
      </Box>
      <Box
        display='flex'
        justifyContent='center'
        width={smDown ? '100%' : '50%'}
      >
        <Box display='flex' flexDirection='column' gap={2} width={smDown ? 350 : 400}>
            {header()}
            {loginForm()}
        </Box>
      </Box>
    </Box>
  );
}