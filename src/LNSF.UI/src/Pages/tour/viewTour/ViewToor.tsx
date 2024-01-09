import { useContext, useState, useMemo } from 'react'
import { MaterialReactTable, useMaterialReactTable, type MRT_ColumnDef, MRT_TableOptions, MRT_GlobalFilterTextField } from 'material-react-table';
import { MRT_Localization_PT_BR } from 'material-react-table/locales/pt-BR';
import { useEffect } from "react"
import SearchIcon from '@mui/icons-material/Search';
import DeleteIcon from '@mui/icons-material/Delete';
import { PeopleContext, RoomContext, TourContext, iTourFilter, iTourObject, iTourPeopleRoom } from '../../../Contexts';
import { Box, Button, Divider, Icon, IconButton, LinearProgress, Paper, Table, TableBody, TableCell, TableContainer, TableFooter, TableRow, Toolbar, Typography, useMediaQuery, useTheme } from '@mui/material';
import DescriptionRoundedIcon from '@mui/icons-material/DescriptionRounded';
import { format, parseISO } from 'date-fns';
import EditRoundedIcon from '@mui/icons-material/EditRounded';
import { useNavigate } from 'react-router-dom';
import { iOrderBy } from '../../../Contexts/types';

export const ViewTour: React.FC = () => {
    const { viewTourOutput, getTours } = useContext(TourContext);
    const { viewPeople } = useContext(PeopleContext);
    const { viewRoom } = useContext(RoomContext);
    const [datainput_, setDataInput_] = useState<iTourPeopleRoom[]>([]);
    const [data, setTours] = useState<iTourObject[]>([]);
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
        const testTours = async () => {
            const filter: iTourFilter = {getPeople: true, orderBy: iOrderBy.ascendent}; 
            const tours = await getTours(filter);
            setTours(tours);
        }

        testTours();

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
                            console.log(namePromise)
                            const name = await namePromise.then((result) => { if (result) { return result.name } })
                            //const idRoom = await namePromise.then((result) => { if (result) { return result.roomId } })
                            const idRoom = 210
;                            if (typeof idRoom === 'number' && typeof name === 'string') {
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

    const columns = useMemo<MRT_ColumnDef<iTourObject>[]>(
        () => [
            {
                accessorKey: 'people.name',
                header: 'Nome',
                size: 100,
                enableColumnActions: false,
                enableColumnFilter: false,
                enableSorting: false,
            },
            {
                accessorKey: 'people.rg',
                header: 'RG',
                size: 100,
                enableColumnActions: false,
                enableColumnFilter: false,
                enableSorting: false,
            },
            {
                accessorKey: 'people.cpf',
                header: 'CPF',
                size: 100,
                enableColumnActions: false,
                enableColumnFilter: false,
                enableSorting: false,
            },
            {
                accessorKey: 'output',
                header: 'Saída',
                size: 100,
                enableColumnActions: false,
                enableSorting: false,
            },
            {
                accessorKey: 'input',
                header: 'Retorno',
                size: 100,
                enableColumnActions: false,
                enableSorting: false,
            },
            {
                accessorKey: 'note',
                header: 'Observação',
                size: 200,
                enableSorting: false,
            },
        ],
        [],
    );

    return (
        <Box>
            <MaterialReactTable 
                columns={columns}
                data={data}

                
                
                enableRowActions
                renderTopToolbarCustomActions={() => {
                    return <Typography variant={smDown ? "h6" : "h5"} display='flex' alignItems='center' >
                        <DescriptionRoundedIcon fontSize={smDown ? "medium" : "large"} color='primary' />
                        Histórico de entrada e saída
                    </Typography>;
                }}
                
                renderRowActions={({
                    row
                }) => <Box display='flex' flexDirection='row' flexWrap='nowrap'>
                            <IconButton onClick={() => console.info('View Profile', row.original)} >
                                <SearchIcon />
                            </IconButton>
                            <IconButton>
                                <EditRoundedIcon />
                            </IconButton>   
                            <IconButton>
                                <DeleteIcon />
                            </IconButton>
                        </Box>}
                
                localization={MRT_Localization_PT_BR}
            />
        </Box>
    )
}