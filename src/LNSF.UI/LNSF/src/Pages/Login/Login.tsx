
import { Box, Button, Card, CardContent, CircularProgress, FormControl, FormHelperText, IconButton, InputAdornment, InputLabel, OutlinedInput, TextField, Typography, useMediaQuery, useTheme } from "@mui/material";
import { useContext, useEffect, useState } from "react";
import * as yup from 'yup';
import { AuthContext } from "../../Contexts/authcontext/AuthContext_";
import nomelogo from '../../assets/lnsf_icone.svg';
import fundo from '../../assets/fundoLogin.jpg';
import { useNavigate } from 'react-router-dom'

import { Link as MaterialUILink } from '@mui/material';
import { Visibility, VisibilityOff } from "@mui/icons-material";
import React from "react";

const loginSchema = yup.object().shape({
    userName: yup.string().email().required("Email é um campo obrigatório"),
    password: yup.string().required("A senha deve ter pelo menos 6 caracteres").min(6),
})

export const LoginPage: React.FC = () => {
    const { loginUser } = useContext(AuthContext)
    const accessToken = localStorage.getItem("@lnsf:accessToken") || ''
    const [userName, setUserName] = useState('');
    const [password, setPassword] = useState('');
    const [isLoading, setIsLoading] = useState(false);

    const [emailError, setEmailError] = useState('');
    const [passwordError, setPasswordError] = useState('');

    const theme = useTheme();
    const smDown = useMediaQuery(theme.breakpoints.down('sm'));

    const [showPassword, setShowPassword] = React.useState(false);
    const navigate = useNavigate();

    const handleClickShowPassword = () => setShowPassword((show) => !show);
    const handleMouseDownPassword = (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
    };

    useEffect(() => {
        if (accessToken) {
            navigate("/inicio")
        }

    }, []);



    const handlesubimit = () => {

        loginSchema
            .validate({ userName, password }, { abortEarly: false })
            .then(dadosValidados => {
                const data = {
                    userName: dadosValidados.userName,
                    password: dadosValidados.password,
                }
                loginUser(data)

                setIsLoading(false)
            })
            .catch((errors: yup.ValidationError) => {

                errors.inner.forEach(error => {
                    if (error.path === 'email') {
                        setEmailError(error.message);
                    }
                    else if (error.path === 'password') {
                        setPasswordError(error.message);
                    }
                });
            });
    };

    return (
        <Box
            width='100%'
            height='100vh'
            overflow='hidden'
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
                <Box>
                    <Box>
                        <Box display='flex' flexDirection='column' gap={2} width={smDown ? 350 : 400}>

                            <Box
                                width='100%'
                                padding={theme.spacing(3)}
                                display='flex'
                                alignItems='center'
                                justifyContent='center'
                            >
                                <img src={nomelogo} />
                            </Box>

                            <Box
                                display='flex'
                                flexDirection='column'
                                alignItems='center'
                                justifyContent='center'

                            >
                                <Typography variant="h6" align="center"
                                    fontSize='1.5rem'
                                    fontStyle='normal'
                                    fontWeight='400'
                                    lineHeight='normal'

                                >
                                    Seja bem-vindo ao nosso lar digital!
                                </Typography>
                                <Typography variant="subtitle1" align="center"

                                    fontSize='1rem'
                                    fontStyle='normal'
                                    fontWeight='400'
                                    lineHeight='normal'
                                    letterSpacing='0.00938rem'

                                    display='flex'
                                    flexDirection='column'
                                >
                                    <span>
                                        Estamos muito felizes em recebê-lo!
                                    </span>
                                    <span>
                                        Deixe tudo conosco, iremos facilitar todo o seu trabalho ❤️
                                    </span>
                                </Typography>
                            </Box>

                            <form onSubmit={handlesubimit}>
                                <Box display='flex' flexDirection='column' gap={2} >
                                    <TextField
                                        id="Email"
                                        label='Email'
                                        type="email"
                                        value={userName}
                                        error={!!emailError}
                                        helperText={emailError}
                                        onKeyDown={() => setEmailError('')}
                                        onChange={e => setUserName(e.target.value)}

                                    />

                                    <FormControl variant="outlined">
                                        <InputLabel htmlFor="outlined-adornment-password">Senha</InputLabel>
                                        <OutlinedInput
                                            id="Senha"
                                            type={showPassword ? 'text' : 'password'}
                                            value={password}
                                            error={!!passwordError}
                                            //helperText={passwordError}
                                            onKeyDown={() => setPasswordError('')}
                                            onChange={e => setPassword(e.target.value)}
                                            endAdornment={
                                                <InputAdornment position="end">
                                                    <IconButton
                                                        aria-label="toggle password visibility"
                                                        onClick={handleClickShowPassword}
                                                        onMouseDown={handleMouseDownPassword}
                                                        edge="end"
                                                    >
                                                        {showPassword ? <VisibilityOff /> : <Visibility />}
                                                    </IconButton>
                                                </InputAdornment>
                                            }
                                            label="Password"
                                            aria-describedby="component-error-text"

                                        />
                                        <FormControl error>
                                            <FormHelperText id="component-error-text">{passwordError}</FormHelperText>
                                        </FormControl>
                                    </FormControl>

                                    <Box
                                        display='flex'
                                        justifyContent='right'
                                    >
                                        <MaterialUILink href="https://www.exemplo.com" target="_blank" rel="noopener">
                                            Pedir ajuda
                                        </MaterialUILink>
                                    </Box>

                                    <Button
                                        variant="contained"
                                        onClick={() => {
                                            setIsLoading(true);
                                            handlesubimit();
                                        }}
                                        fullWidth
                                    >
                                        {isLoading ? <CircularProgress color="info" /> : 'Entrar'}


                                    </Button>
                                </Box>
                            </form>
                        </Box>
                    </Box>
                </Box>
            </Box>

        </Box>);

}