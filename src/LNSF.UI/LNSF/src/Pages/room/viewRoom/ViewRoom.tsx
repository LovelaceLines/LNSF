
import { useContext, useMemo, useState } from 'react'
import { useEffect } from "react"
import { RoomContext, iRoomObject } from '../../../Contexts';
import { Box, LinearProgress, Pagination, Paper, Table, TableBody, TableCell, TableContainer, TableFooter, TableHead, TableRow, Toolbar, Typography, useMediaQuery, useTheme } from '@mui/material';
import { Environment } from '../../../environment';
import { SearchButton } from '../../../Component';
import { useSearchParams } from 'react-router-dom';
import { useDebounce } from '../../../Component/hooks/UseDebounce';
import CheckCircleRoundedIcon from '@mui/icons-material/CheckCircleRounded';
import UnpublishedRoundedIcon from '@mui/icons-material/UnpublishedRounded';
import HotelRoundedIcon from '@mui/icons-material/HotelRounded';

export const ViewRoom: React.FC = () => {

    const { viewRoom, returnQuantity } = useContext(RoomContext);
    const [rows, setRows] = useState<iRoomObject[]>([]);
    const [totalCount, setTotalCount] = useState(0);
    const [isLoadind, setIsLoading] = useState(true);
    const [selectedFilter, setSelectedFilter] = useState('');
    const [searchParams, setSearchParams] = useSearchParams();
    const { debounce } = useDebounce();
    const theme = useTheme();
    const smDown = useMediaQuery(theme.breakpoints.down('sm'));


    const datafilter = [
        {
            filter: 'number',
            tipo: 'Nenhum',
        },
        {
            filter: 'number',
            tipo: 'N°',
        },
        {
            filter: 'storey',
            tipo: 'Andar',
        },
        {
            filter: 'available',
            tipo: 'Disponível',
        },
        {
            filter: 'available',
            tipo: 'Indisponível',
        },
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

            viewRoom(pagina, busca, filter)
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
                        variant= {smDown ? "h5" : "h4"}
                        noWrap
                        component="div"
                        sx={{ flexGrow: 1, display: 'flex', alignItems: 'flex-end' }}
                    >
                        {!smDown && (<HotelRoundedIcon color='primary' sx={{ fontSize: '2.7rem', paddingRight: '10px' }} />)}
                        Apartamentos
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
                            <TableCell sx={{ textAlign: 'center' }}>N°</TableCell>
                            <TableCell sx={{ textAlign: 'center' }}>Ocupação</TableCell>
                            <TableCell sx={{ textAlign: 'center' }}>Andar</TableCell>
                            <TableCell sx={{ textAlign: 'center' }}>Banheiro</TableCell>
                            <TableCell sx={{ textAlign: 'center' }}>Disponível</TableCell>
                        </TableRow>
                    </TableHead>

                    <TableBody >
                        {rows.map(row => (
                            <TableRow key={row.id}>
                                <TableCell sx={{ textAlign: 'center' }}>{row.number}</TableCell>
                                <TableCell sx={{ textAlign: 'center' }}>{row.occupation}</TableCell>
                                <TableCell sx={{ textAlign: 'center' }}>{row.storey}</TableCell>
                                <TableCell sx={{ textAlign: 'center' }}>{row.bathroom ? 'Individual' : 'Coletivo'}</TableCell>
                                <TableCell sx={{ textAlign: 'center' }}>{row.available ? <CheckCircleRoundedIcon color='primary' /> : <UnpublishedRoundedIcon sx={{ color: 'rgba(255, 0, 0, 0.8)' }} />}</TableCell>
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

        </Box>
    )
}