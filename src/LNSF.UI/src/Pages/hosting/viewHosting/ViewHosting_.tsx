
import Collapse from '@mui/material/Collapse';
import IconButton from '@mui/material/IconButton';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Typography from '@mui/material/Typography';
import Paper from '@mui/material/Paper';
import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
import KeyboardArrowUpIcon from '@mui/icons-material/KeyboardArrowUp';
import { Fragment, useContext, useEffect, useMemo, useState } from 'react';
import { Box, LinearProgress, Pagination, TableFooter, Toolbar, useMediaQuery, useTheme } from '@mui/material';
import { ButtonAction, SearchButton } from '../../../Component';
import { useNavigate, useSearchParams } from 'react-router-dom';
import { useDebounce } from '../../../Component/hooks/UseDebounce';
import { HostingContext } from '../../../Contexts/hostingContext';
import { iHostingObject } from '../../../Contexts/hostingContext/type';
import { format, isValid, parseISO } from 'date-fns';
import { Environment } from '../../../environment';


function createData(
    id: number,
    checkIn: Date,
    checkOut: Date,
    escortInfos: [{
        id: number,
        checkIn: Date,
        checkOut: Date,
    }],
    patientId: number,
) {
    return {
        id,
        checkIn,
        checkOut,
        escortInfos: [{
            id,
            checkIn,
            checkOut,
        }],
        patientId,
    };
}

function Row(props: { row: ReturnType<typeof createData> }) {
    const { row } = props;
    const [open, setOpen] = useState(false);

    return (
        <Fragment>
            <TableRow sx={{ '& > *': { borderBottom: 'unset' } }}>
                <TableCell>
                    <IconButton
                        aria-label="expand row"
                        size="small"
                        onClick={() => setOpen(!open)}
                    >
                        {open ? <KeyboardArrowUpIcon /> : <KeyboardArrowDownIcon />}
                    </IconButton>
                </TableCell>
                <TableCell>
                    {row.id}
                </TableCell>

                <TableCell component="th" scope="row">
                    {isValid(parseISO(String(row.checkIn))) ? format(parseISO(String(row.checkIn)), 'dd/MM/yyyy') : '-'}
                </TableCell>
                <TableCell component="th" scope="row">
                    {isValid(parseISO(String(row.checkOut))) ? format(parseISO(String(row.checkOut)), 'dd/MM/yyyy') : '-'}
                </TableCell>
            </TableRow>

            <TableRow>
                <TableCell style={{ paddingBottom: 0, paddingTop: 0 }} colSpan={6}>
                    <Collapse in={open} timeout="auto" unmountOnExit>
                        <Box sx={{ margin: 1 }}>
                            <Typography variant="h6" gutterBottom component="div">
                                Acompanhantes
                            </Typography>
                            <Table size="small" aria-label="purchases">
                                <TableHead>
                                    <TableRow>
                                        <TableCell>Nome</TableCell>
                                        <TableCell>Chegada</TableCell>
                                        <TableCell>Saída</TableCell>
                                    </TableRow>
                                </TableHead>
                                <TableBody>
                                    {row.escortInfos.map((historyRow) => (
                                        <TableRow key={historyRow.id}>
                                            <TableCell component="th" scope="row">
                                                {historyRow.id}
                                            </TableCell>
                                            <TableCell component="th" scope="row">
                                                {isValid(parseISO(String(historyRow.checkIn))) ? format(parseISO(String(historyRow.checkIn)), 'dd/MM/yyyy') : '-'}
                                            </TableCell>

                                            <TableCell component="th" scope="row">
                                                {isValid(parseISO(String(historyRow.checkOut))) ? format(parseISO(String(historyRow.checkOut)), 'dd/MM/yyyy') : '-'}
                                            </TableCell>

                                        </TableRow>
                                    ))}
                                </TableBody>
                            </Table>
                        </Box>
                    </Collapse>
                </TableCell>
            </TableRow>
        </Fragment>
    );
}


export const Hosting: React.FC = () => {
    const [isLoadind, setIsLoading] = useState(false);
    const [searchParams, setSearchParams] = useSearchParams();
    const { debounce } = useDebounce();
    const { viewHosting, countHosting } = useContext(HostingContext);
    const [rows, setRows] = useState<iHostingObject[]>([]);
    const [totalCount, setTotalCount] = useState(0);
    const theme = useTheme();
    const navigate = useNavigate();
    const smDown = useMediaQuery(theme.breakpoints.down('sm'));

    const busca = useMemo(() => {
        return (searchParams.get('busca') || '');
    }, [searchParams])

    const pagina = useMemo(() => {
        return Number(searchParams.get('pagina') || '1');
    }, [searchParams])

    useEffect(() => {
        setIsLoading(true);

        debounce(() => {

            viewHosting(pagina, busca, 'id')
                .then((response) => {
                    if (response instanceof Error) {
                        setIsLoading(false);
                    } else {
                        setRows(response);

                        response.map((row) => (
                            createData(
                                row.id,
                                row.checkIn,
                                row.checkOut,
                                row.escortInfos,
                                row.patientId)
                        ))

                        setIsLoading(false);
                    }


                })
                .catch((error) => {
                    setIsLoading(false);
                    console.error('Detalhes do erro:', error);
                });
        });

    }, [busca, pagina]);

    useEffect(() => {
        console.log(totalCount, isLoadind)
        countHosting()
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
                <Toolbar sx={{ margin: 0 }}>
                    <Typography
                        variant={smDown ? "h5" : "h4"}
                        noWrap
                        component="div"
                        sx={{ flexGrow: 1, display: 'flex', alignItems: 'flex-end' }}
                    >
                        {/* {!smDown && (<PersonIcon color='primary' sx={{ fontSize: '2.7rem', paddingRight: '10px' }} />)} */}
                        Pessoas
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
                        aoClicarEmNovo={() => { navigate('/inicio/pessoas/gerenciar/cadastrar') }}
                    />

                </Toolbar>
            </Box>
            <TableContainer component={Paper}>
                <Table aria-label="collapsible table">
                    <TableHead>
                        <TableRow>
                            <TableCell />
                            <TableCell>Nome</TableCell>
                            <TableCell>Chegada</TableCell>
                            <TableCell>Saída</TableCell>

                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {rows.map((row) => (

                            <Row key={row.id} row={row} />
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
                        <TableRow>
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
    );
}