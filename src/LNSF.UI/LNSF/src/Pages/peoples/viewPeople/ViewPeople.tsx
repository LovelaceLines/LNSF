
import { useContext, useMemo, useState } from 'react'
import { useEffect } from "react"
import { PeopleContext, iPeopleObject } from '../../../Contexts';
import { Box, Button, Fade, IconButton, LinearProgress, Modal, Pagination, Paper, Table, TableBody, TableCell, TableContainer, TableFooter, TableHead, TableRow, Toolbar, Typography, useMediaQuery, useTheme } from '@mui/material';
import { Environment } from '../../../environment';
import { SearchButton } from '../../../Component';
import { useSearchParams } from 'react-router-dom';
import { useDebounce } from '../../../Component/hooks/UseDebounce';
import PersonIcon from '@mui/icons-material/Person';
import { format, parseISO } from 'date-fns';
import CloseRoundedIcon from '@mui/icons-material/CloseRounded';
import PreviewRoundedIcon from '@mui/icons-material/PreviewRounded';

export const ViewPatients: React.FC = () => {

    const { viewPeople, returnQuantity } = useContext(PeopleContext);
    const [rows, setRows] = useState<iPeopleObject[]>([]);
    const [totalCount, setTotalCount] = useState(0);
    const [isLoadind, setIsLoading] = useState(true);
    const [selectedFilter, setSelectedFilter] = useState('');
    const [searchParams, setSearchParams] = useSearchParams();
    const { debounce } = useDebounce();
    const theme = useTheme();
    const smDown = useMediaQuery(theme.breakpoints.down('sm'));

    const [isModalOpenNota, setIsModalOpenNota] = useState(false);
    const [notaModal, setNotaModal] = useState('');

    const datafilter = [
        {
            filter: 'number',
            tipo: 'Nenhum',
        },
        // {
        //     filter: 'number',
        //     tipo: 'N°',
        // },
        // {
        //     filter: 'storey',
        //     tipo: 'Andar',
        // },
        // {
        //     filter: 'available',
        //     tipo: 'Disponível',
        // },
        // {
        //     filter: 'available',
        //     tipo: 'Indisponível',
        // },
    ]

    const busca = useMemo(() => {
        if (selectedFilter === 'Disponível') {
            return 'true';
        } else if (selectedFilter === 'Indisponível') {
            return 'false';
        } else {
            return (searchParams.get('busca') || '');
        }
    }, [searchParams])


    const pagina = useMemo(() => {
        return Number(searchParams.get('pagina') || '1');
    }, [searchParams])


    const filter = useMemo(() => {
        if (selectedFilter !== 'Nenhum') {
            const filtroEncontrado = datafilter.find((item) => item.tipo === selectedFilter);
            return filtroEncontrado?.filter ?? '';
        } else {
            return 'number';
        }
    }, [searchParams])


    useEffect(() => {
        setIsLoading(true);

        debounce(() => {
            if (selectedFilter === 'Nenhum') {
                setSearchParams({ busca: '', pagina: '1' }, { replace: true });
            }

            viewPeople(pagina, busca, filter)
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

    }, [busca, pagina, selectedFilter]);

    useEffect(() => {
        returnQuantity()
            .then((response) => {
                setTotalCount(response);
            });
    }, []);


    const viewNota = (nota: string) => {
        setNotaModal(nota)
        setIsModalOpenNota(true)
    }


    return (

        <Box
            display='flex'
            flexDirection='column'
            width='100%'
        >
            <Box>
                {/* <AppBar position="static"> */}
                <Toolbar sx={{ margin: 0 }}>
                    <Typography
                        variant={smDown ? "h5" : "h4"}
                        noWrap
                        component="div"
                        sx={{ flexGrow: 1, display: 'flex', alignItems: 'flex-end' }}
                    >
                        {!smDown && (<PersonIcon color='primary' sx={{ fontSize: '2.7rem', paddingRight: '10px' }} />)}
                        Pessoas
                    </Typography>

                    <SearchButton
                        textoDaBusca={busca}
                        aoMudarTextoDeBusca={texto => setSearchParams({ busca: texto, pagina: '1' }, { replace: true })}
                        filter={datafilter}
                        aoMudarFiltro={(novoFiltro) => { setSelectedFilter(novoFiltro); }}
                    />
                </Toolbar>
                {/* </AppBar> */}
            </Box>


            <TableContainer component={Paper} variant='outlined' >
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell sx={{ textAlign: 'center', width: '30%' }}>Nome</TableCell>
                            <TableCell sx={{ textAlign: 'center' }}>RG</TableCell>
                            <TableCell sx={{ textAlign: 'center' }}>CPF</TableCell>
                            <TableCell sx={{ textAlign: 'center' }}>Data de Nascimento</TableCell>
                            <TableCell sx={{ textAlign: 'center' }}>Gênero</TableCell>
                            <TableCell sx={{ textAlign: 'center' }}>Estado</TableCell>
                            <TableCell sx={{ textAlign: 'center' }}>Cidade</TableCell>
                            <TableCell sx={{ textAlign: 'center' }}>Bairro</TableCell>
                            <TableCell sx={{ textAlign: 'center' }}>N°</TableCell>
                            <TableCell sx={{ textAlign: 'center' }}>Telefone</TableCell>
                            <TableCell sx={{ textAlign: 'center',  width: '5%' }}>Nota</TableCell>
                        </TableRow>
                    </TableHead>

                    <TableBody >
                        {rows.map(row => (
                            <TableRow key={row.id}>
                                <TableCell sx={{ textAlign: 'center', width: '30%' }}>{row.name}</TableCell>
                                <TableCell sx={{ textAlign: 'center' }}>{row.rg}</TableCell>
                                <TableCell sx={{ textAlign: 'center' }}>{row.cpf}</TableCell>
                                <TableCell sx={{ textAlign: 'center' }}>{format(parseISO(String(row.birthDate)), 'dd/MM/yyyy')}</TableCell>
                                <TableCell sx={{ textAlign: 'center' }}>{row.gender}</TableCell>
                                <TableCell sx={{ textAlign: 'center' }}>{row.state}</TableCell>
                                <TableCell sx={{ textAlign: 'center' }}>{row.city}</TableCell>
                                <TableCell sx={{ textAlign: 'center' }}>{row.neighborhood}</TableCell>
                                <TableCell sx={{ textAlign: 'center' }}>{row.houseNumber}</TableCell>
                                <TableCell sx={{ textAlign: 'center' }}>{row.phone}</TableCell>
                                <TableCell sx={{ textAlign: 'center',  width: '5%' }}>
                                    <IconButton size='small' onClick={() => { viewNota(row.note) }}>
                                        <PreviewRoundedIcon color='primary' fontSize='small' />
                                    </IconButton>


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
                                <TableCell colSpan={5}>
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

            <Modal
                open={isModalOpenNota}
                onClose={() => setIsModalOpenNota(false)}
                closeAfterTransition
                sx={
                    {
                        display: 'flex',
                        alignItems: 'center',
                        justifyContent: 'center'
                    }
                }
            >
                <Fade in={isModalOpenNota}>
                    <Box sx={{
                        minWidth: 300,
                        maxWidth: 500,
                        bgcolor: 'background.paper',
                        p: 2,
                        borderRadius: 1.5,
                        position: 'relative',
                    }}>
                        <Box sx={{
                            display: 'flex',
                            justifyContent: 'end',
                            alignItem: 'center',
                            position: 'absolute',
                            top: 10,
                            right: 10,
                            zIndex: 1
                        }}>
                            <Button onClick={() => { setIsModalOpenNota(false) }}>
                                < CloseRoundedIcon sx={{ fontSize: '2rem' }} />
                            </Button>
                        </Box>
                        <Box
                            display='flex'
                            margin={2}
                            gap={2}
                            flexDirection='column'
                            alignItems='center'
                            justifyContent='center'
                        >
                            <Typography variant='h6'>
                                Observação
                            </Typography>
                            <Typography variant='subtitle1'>
                                {notaModal}
                            </Typography>
                        </Box>
                    </Box>


                </Fade>
            </Modal>

        </Box>
    )
}