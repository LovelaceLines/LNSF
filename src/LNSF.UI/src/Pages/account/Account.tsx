
import { useContext, useState } from 'react'
import { useEffect } from "react"
import { Box, Button, LinearProgress, Paper, Table, TableBody, TableCell, TableContainer, TableFooter, TableHead, TableRow, Toolbar, Typography, useMediaQuery, useTheme } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import SupervisedUserCircleRoundedIcon from '@mui/icons-material/SupervisedUserCircleRounded';
import EditRoundedIcon from '@mui/icons-material/EditRounded';
import DeleteRoundedIcon from '@mui/icons-material/DeleteRounded';
import { AccountContext } from '../../Contexts';
import { useDebounce } from '../../Component/hooks/UseDebounce';
import { ButtonAction } from '../../Component';
import { iUser, idelUserID } from '../../Contexts/accountContext/type';

export const Account: React.FC = () => {

    const { viewUser, deleteUserId } = useContext(AccountContext);
    const [account, setAccount] = useState<iUser[]>([]);
    const [isLoadind, setIsLoading] = useState(true);
    const { debounce } = useDebounce();
    const theme = useTheme();
    const smDown = useMediaQuery(theme.breakpoints.down('sm'));
    const navigate = useNavigate();
    const [modify, setModify] = useState(false);

    useEffect(() => {
        setIsLoading(true);

        debounce(() => {

            viewUser()
                .then((response) => {
                    if (response instanceof Error) {
                        setIsLoading(false);
                    } else {
                        setAccount(response);
                        setIsLoading(false);
                    }
                })
                .catch((error) => {
                    setIsLoading(false);
                    console.error('Detalhes do erro:', error);
                });
        });

    }, [modify]);

    const deletePeopleUser = (id_: string) => {

        if (confirm('Realmente deseja remover usuário?')) {
            const data: idelUserID = {
                id: id_
            }
            setIsLoading(true);
            deleteUserId(data)
                .then((response) => {
                    if (response instanceof Error) {
                        setIsLoading(false);
                    } else {
                        setModify(!modify)
                        setIsLoading(false);
                    }
                })
                .catch((error) => {
                    setIsLoading(false);
                    console.error('Detalhes do erro:', error);
                });
        }
    }

    return (
        <Box
            display='flex'
            flexDirection='column'
            width='100%'
        >
            <Box>
                <Toolbar>
                    <Typography
                        variant={smDown ? "h5" : "h4"}
                        noWrap
                        component="div"
                        sx={{ flexGrow: 1, display: 'flex', alignItems: 'flex-end' }}
                    >
                        {!smDown && (<SupervisedUserCircleRoundedIcon color='primary' sx={{ fontSize: '2.7rem', paddingRight: '10px' }} />)}
                        Usuários
                    </Typography>

                    < ButtonAction
                        mostrarBotaoNovo={true}
                        mostrarBotaoSalvar={false}
                        mostrarBotaoVoltar={false}

                        mostrarBotaoApagar={false}
                        aoClicarEmNovo={() => { navigate('/inicio/usuarios/gerenciar/cadastrar') }}
                    />

                </Toolbar>
            </Box>

            <TableContainer component={Paper} variant='outlined' >
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell sx={{ textAlign: 'center' }}>Email</TableCell>
                            <TableCell sx={{ textAlign: 'center' }}>Função</TableCell>
                            <TableCell sx={{ textAlign: 'center' }}>Editar</TableCell>
                            <TableCell sx={{ textAlign: 'center' }}>Deletar</TableCell>
                        </TableRow>
                    </TableHead>

                    <TableBody>
                        {account.map(row => (
                            <TableRow key={row.id}>
                                <TableCell sx={{ textAlign: 'center' }}>{row.userName}</TableCell>
                                <TableCell sx={{ textAlign: 'center' }}>
                                    oiii
                                    {/* {row === 0 ? 'Voluntário' :
                                        row.role === 1 ? 'Administrador' :
                                            row.role === 2 ? 'Assistente Social' :
                                                'Secretária'
                                    } */}
                                </TableCell>

                                <TableCell sx={{ textAlign: 'center' }}>
                                    <Button
                                        size='small'
                                        color='primary'
                                        disableElevation
                                        variant='outlined'
                                        onClick={() => navigate(`/inicio/usuarios/gerenciar/${row.userName}`)}
                                    >
                                         <EditRoundedIcon fontSize='small' />
                                    </Button>
                                </TableCell>

                                <TableCell sx={{ textAlign: 'center' }}>
                                    <Button
                                        size='small'
                                        color='primary'
                                        disableElevation
                                        variant='outlined'
                                        onClick={() => deletePeopleUser(row.id)}
                                    >
                                        <DeleteRoundedIcon sx={{ color: 'red' }} fontSize='small' />
                                    </Button>
                                </TableCell>

                            </TableRow>
                        ))}
                    </TableBody>

                    <TableFooter>
                        {isLoadind && (
                            <TableRow>
                                <TableCell colSpan={7}>
                                    <LinearProgress />
                                </TableCell>
                            </TableRow>
                        )}
                    </TableFooter>
                </Table>
            </TableContainer>

        </Box>
    )
}