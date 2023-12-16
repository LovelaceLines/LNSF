
import { useContext, useMemo, useState } from 'react'
import { useEffect } from "react"
import { Box, Button, LinearProgress, Pagination, Paper, Table, TableBody, TableCell, TableContainer, TableFooter, TableHead, TableRow, Toolbar, Typography, useMediaQuery, useTheme } from '@mui/material';
import { Environment } from '../../../environment';
import { ButtonAction, SearchButton } from '../../../Component';
import { useNavigate, useSearchParams } from 'react-router-dom';
import { useDebounce } from '../../../Component/hooks/UseDebounce';
import DeleteRoundedIcon from '@mui/icons-material/DeleteRounded';
import { HospitalContext } from '../../../Contexts/hospitalContext';
import { iHospitalObject } from '../../../Contexts/hospitalContext/type';
import EditRoundedIcon from '@mui/icons-material/EditRounded';
import DomainIcon from '@mui/icons-material/Domain';

export const ViewHospital: React.FC = () => {

    const { countHospital, viewHospital, deleteHospital} = useContext(HospitalContext);
    const [rows, setRows] = useState<iHospitalObject[]>([]);
    const [totalCount, setTotalCount] = useState(0);
    const [isLoadind, setIsLoading] = useState(true);
    const [searchParams, setSearchParams] = useSearchParams();
    const { debounce } = useDebounce();
    const theme = useTheme();
    const smDown = useMediaQuery(theme.breakpoints.down('sm'));
    const navigate = useNavigate();
    const [modify, setModify] = useState(false);

    const busca = useMemo(() => {
        return (searchParams.get('busca') || '');
    }, [searchParams])

    const pagina = useMemo(() => {
        return Number(searchParams.get('pagina') || '1');
    }, [searchParams])


    useEffect(() => {
        setIsLoading(true);

        debounce(() => {
           
            viewHospital(pagina, busca, 'name')
                .then((response) => {
                    if (response instanceof Error) {
                        setIsLoading(false);
                    } else {
                        setRows(response);
                        setIsLoading(false);
                    }
                })
                .catch((error) => {
                    setIsLoading(false);
                    console.error('Detalhes do erro:', error);
                });
        });

    }, [busca, pagina, modify]);

    useEffect(() => {
        countHospital()
            .then((response) => {
                setTotalCount(response);
            });
    }, [modify]);


    const deletehospital = (id_: number) => {

        if (confirm('Realmente deseja remover esse hospital?')) {
           
            setIsLoading(true);
            deleteHospital(String(id_))
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
                <Toolbar sx={{ margin: 0 }}>
                    <Typography
                        variant={smDown ? "h5" : "h4"}
                        noWrap
                        component="div"
                        sx={{ flexGrow: 1, display: 'flex', alignItems: 'flex-end' }}
                    >
                        {!smDown && (<DomainIcon color='primary' sx={{ fontSize: '2.7rem', paddingRight: '10px' }} />)}
                        Hospitais
                    </Typography>

                    <SearchButton
                        textoDaBusca={busca}
                        aoMudarTextoDeBusca={texto => setSearchParams({ busca: texto, pagina: '1' }, { replace: true })}
                        
                    />

                    < ButtonAction
                        mostrarBotaoNovo={true}
                        mostrarBotaoSalvar={false}
                        mostrarBotaoVoltar={false}

                        mostrarBotaoApagar={false}
                        aoClicarEmNovo={() => { navigate('/inicio/hospital/gerenciar/cadastrar') }}
                    />

                </Toolbar>
            </Box>

            <TableContainer component={Paper} variant='outlined' >
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell sx={{ textAlign: 'left' }}>Nome</TableCell>
                            <TableCell sx={{ textAlign: 'center' }}>Sigla</TableCell>
                            <TableCell sx={{ textAlign: 'center' }}>Editar</TableCell>
                            <TableCell sx={{ textAlign: 'center' }}>Deletar</TableCell>
                        </TableRow>
                    </TableHead>

                    <TableBody>
                        {rows.map(row => (
                            <TableRow key={row.id}>

                                <TableCell sx={{ textAlign: 'left' }}>
                                    {row.name}
                                </TableCell>

                                <TableCell sx={{ textAlign: 'center' }}>
                                    {row.acronym}
                                </TableCell>

                                <TableCell sx={{ textAlign: 'center' }}>
                                    <Button
                                        size='small'
                                        color='primary'
                                        disableElevation
                                        variant='outlined'
                                        onClick={() => navigate(`/inicio/hospital/gerenciar/${row.id}`)}
                                    >
                                        <EditRoundedIcon color='primary' fontSize='small' />
                                    </Button>
                                </TableCell>

                                <TableCell sx={{ textAlign: 'center' }}>
                                    <Button
                                        size='small'
                                        color='primary'
                                        disableElevation
                                        variant='outlined'
                                        onClick={() => {deletehospital(row.id)}}
                                    >
                                        { <DeleteRoundedIcon sx={{ color: 'red' }} fontSize='small' />}
                                    </Button>
                                </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>

                    {totalCount === 0 && !isLoadind && (
                        <caption>{Environment.LISTAGEM_VAZIA}</caption>
                    )}

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
                {(totalCount > 0 && totalCount > Environment.LIMITE_DE_LINHA) && (
                    <Box
                        display='flex'
                        flexDirection='column'
                        alignItems='center'
                        justifyContent='center'
                    >
                        <TableRow >
                            <TableCell>
                                <Pagination
                                    page={pagina}
                                    count={Math.ceil(totalCount / Environment.LIMITE_DE_LINHA)}
                                    color="primary"
                                    onChange={(_, newPage) => setSearchParams({ busca, pagina: newPage.toString() })}
                                />
                            </TableCell>
                        </TableRow>
                    </Box>
                )}
            </TableContainer>
        </Box>
    )
}