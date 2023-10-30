
import { useContext, useState } from 'react'
import { useEffect } from "react"
import { PeopleContext, RoomContext, TourContext, iTourPeopleRoom } from '../../../Contexts';
import { Box, Button, Divider, LinearProgress, Paper, Table, TableBody, TableCell, TableContainer, TableFooter, TableRow, Toolbar, Typography, useMediaQuery, useTheme } from '@mui/material';
import DescriptionRoundedIcon from '@mui/icons-material/DescriptionRounded';
import { format, parseISO } from 'date-fns';
import EditRoundedIcon from '@mui/icons-material/EditRounded';
import { useNavigate } from 'react-router-dom';

export const ViewTour: React.FC = () => {

    const { viewTourOutput } = useContext(TourContext);
    const { viewPeople } = useContext(PeopleContext);
    const { viewRoom } = useContext(RoomContext);
    const [datainput_, setDataInput_] = useState<iTourPeopleRoom[]>([]);
    const [isLoadind, setIsLoading] = useState(true);
    const navigate = useNavigate();
    const theme = useTheme();
    const smDown = useMediaQuery(theme.breakpoints.down('sm'));
    const BuscarPessoa = async (id: number) => {
        try {
            const response = await viewPeople(1, String(id), 'id');

            if (response instanceof Error) {
                return '';
            } else {
                return response[0];
            }
        } catch (error) {
            console.error('Detalhes do erro:', error);
            return '';
        }
    };

    const BuscarQuarto = async (id: number) => {
        try {
            const response = await viewRoom(1, String(id), 'id');

            if (response instanceof Error) {
                return 0;
            } else {
                return String(response[0].number);
            }
        } catch (error) {
            console.error('Detalhes do erro:', error);
            return 0;
        }
    };


    useEffect(() => {
        const fetchData = async () => {
            setIsLoading(true);

            try {
                const secondResponse = await viewTourOutput(false);

                if (secondResponse instanceof Error) {
                    setIsLoading(false);
                } else {
                    const updatedData = await Promise.all(
                        secondResponse.map(async row => {
                            const namePromise = BuscarPessoa(row.peopleId);
                            const name = await namePromise.then((result) => { if (result) { return result.name } })
                            const idRoom = await namePromise.then((result) => { if (result) { return result.roomId } })

                            if (typeof idRoom === 'number' && typeof name === 'string') {
                                const roomPromise = BuscarQuarto(idRoom);
                                const room = await roomPromise;

                                return {
                                    id: row.id,
                                    output: row.output,
                                    input: row.input,
                                    note: row.note,
                                    peopleId: row.peopleId,
                                    name: name,
                                    number: String(room),
                                };
                            } else {
                                return {
                                    id: row.id,
                                    output: row.output,
                                    input: row.input,
                                    note: row.note,
                                    peopleId: row.peopleId,
                                    name: name === undefined ? '' : name,
                                    number: '',
                                };
                            }
                        })
                    );
                    setDataInput_(updatedData)
                    setIsLoading(false);
                }
            } catch (error) {
                setIsLoading(false);
                console.error('Detalhes do erro:', error);
            }
        };

        fetchData();
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
                        {!smDown && (<DescriptionRoundedIcon color='primary' sx={{ fontSize: '2.7rem', paddingRight: '10px' }} />)}
                        Histórico
                    </Typography>
                </Toolbar>
            </Box>

            <Box
                display='flex'
                flexDirection={smDown ? 'column' : 'row'}
                width='100%'
                gap={smDown ? 5 : 0}
            >
                <Box
                    width='100%'
                    component={Paper}
                    bgcolor={theme.palette.background.default}
                    margin={smDown ? 0 : 1}
                    height={498}
                    overflow='scroll'
                    gap={smDown ? 5 : 0}
                >
                    <Typography variant="h6" padding={1}>
                        Histórico de entrada e saída
                        <Divider />
                    </Typography>
                    <Box padding={2}>
                        <TableContainer component={Paper} variant='outlined' >
                            <Table>
                                <TableBody >
                                    {datainput_.map(row => (
                                        <TableRow key={row.id}>
                                            <TableCell sx={{ textAlign: 'left' }}>
                                                {row.input !== null && (
                                                    <Box display='flex'
                                                        flexDirection='row'
                                                        width='100%'
                                                    >
                                                        <Box
                                                            display='flex'
                                                            flexDirection='column'
                                                            width='90%'
                                                        >
                                                            <Box display='flex'
                                                                flexDirection='row'
                                                            >
                                                                <Box
                                                                    width='100%'>
                                                                    <p>
                                                                        <strong>Nome:</strong> {row.name}
                                                                    </p>
                                                                    <Box display='flex'
                                                                        justifyContent='space-between'
                                                                    >
                                                                        {row.number !== '' && (<span > <strong>Apt</strong> {row.number}</span>)}
                                                                        <Box>
                                                                            <strong>Saída: </strong>  {format(parseISO(String(row.output)), 'HH:mm')} - {format(parseISO(String(row.output)), 'dd/MM/yyyy')}
                                                                        </Box>

                                                                        <Box>
                                                                            <strong>Retorno:</strong>  {format(parseISO(String(row.input)), 'HH:mm')} - {format(parseISO(String(row.input)), 'dd/MM/yyyy')}
                                                                        </Box>
                                                                    </Box>
                                                                    <p>
                                                                        <strong>Obs:</strong> {row.note}
                                                                    </p>
                                                                </Box>
                                                            </Box>
                                                        </Box>
                                                        <Box
                                                            width='10%'
                                                            display='flex'
                                                            justifyContent='center'
                                                            alignItems='center'
                                                        >
                                                            <Button
                                                                size='small'
                                                                color='primary'
                                                                disableElevation
                                                                variant='outlined'
                                                                onClick={() => { navigate(`/inicio/registrodiario/visualizar/${row.id}`) }}
                                                            >
                                                                <EditRoundedIcon color='primary' fontSize='small' />
                                                            </Button>
                                                        </Box>
                                                    </Box>
                                                )}
                                            </TableCell>

                                        </TableRow>
                                    ))}
                                </TableBody>

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
                        </TableContainer>
                    </Box>
                </Box>
            </Box>
        </Box>
    )
}