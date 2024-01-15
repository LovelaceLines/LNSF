import { useContext, useMemo, useState } from 'react'
import { useEffect } from "react"
import { Box, Button, IconButton, Typography, useMediaQuery, useTheme } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import EditRoundedIcon from '@mui/icons-material/EditRounded';
import { AccountContext, RoleString } from '../../Contexts';
import { useDebounce } from '../../Component/hooks/UseDebounce';
import PersonIcon from '@mui/icons-material/Person';
import ContentPasteSearchIcon from '@mui/icons-material/ContentPasteSearch';
import InfoOutlinedIcon from '@mui/icons-material/InfoOutlined';
import AddIcon from '@mui/icons-material/Add';
import { iUser, iUserFilter, idelUserID } from '../../Contexts/accountContext/type';
import { MRT_ColumnDef, MRT_ColumnFiltersState, MRT_PaginationState, MRT_Row, MRT_SortingState, MRT_TableInstance, MRT_VisibilityState, MaterialReactTable, useMaterialReactTable } from 'material-react-table';
import { LocalStorage } from '../../Global';
import { iOrderBy, iPage } from '../../Contexts/types';
import { MRT_Localization_PT_BR } from 'material-react-table/locales/pt-BR';

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

    // return (
    //     <Box
    //         display='flex'
    //         flexDirection='column'
    //         width='100%'
    //     >
    //         <Box>
    //             <Toolbar>
    //                 <Typography
    //                     variant={smDown ? "h5" : "h4"}
    //                     noWrap
    //                     component="div"
    //                     sx={{ flexGrow: 1, display: 'flex', alignItems: 'flex-end' }}
    //                 >
    //                     {!smDown && (<SupervisedUserCircleRoundedIcon color='primary' sx={{ fontSize: '2.7rem', paddingRight: '10px' }} />)}
    //                     Usuários
    //                 </Typography>

    //                 < ButtonAction
    //                     mostrarBotaoNovo={true}
    //                     mostrarBotaoSalvar={false}
    //                     mostrarBotaoVoltar={false}

    //                     mostrarBotaoApagar={false}
    //                     aoClicarEmNovo={() => { navigate('/inicio/usuarios/gerenciar/cadastrar') }}
    //                 />

    //             </Toolbar>
    //         </Box>

    //         <TableContainer component={Paper} variant='outlined' >
    //             <Table>
    //                 <TableHead>
    //                     <TableRow>
    //                         <TableCell sx={{ textAlign: 'center' }}>Email</TableCell>
    //                         <TableCell sx={{ textAlign: 'center' }}>Função</TableCell>
    //                         <TableCell sx={{ textAlign: 'center' }}>Editar</TableCell>
    //                         <TableCell sx={{ textAlign: 'center' }}>Deletar</TableCell>
    //                     </TableRow>
    //                 </TableHead>

    //                 <TableBody>
    //                     {account.map(row => (
    //                         <TableRow key={row.id}>
    //                             <TableCell sx={{ textAlign: 'center' }}>{row.userName}</TableCell>
    //                             <TableCell sx={{ textAlign: 'center' }}>
    //                                 oiii
    //                                 {/* {row === 0 ? 'Voluntário' :
    //                                     row.role === 1 ? 'Administrador' :
    //                                         row.role === 2 ? 'Assistente Social' :
    //                                             'Secretária'
    //                                 } */}
    //                             </TableCell>

    //                             <TableCell sx={{ textAlign: 'center' }}>
    //                                 <Button
    //                                     size='small'
    //                                     color='primary'
    //                                     disableElevation
    //                                     variant='outlined'
    //                                     onClick={() => navigate(`/inicio/usuarios/gerenciar/${row.userName}`)}
    //                                 >
    //                                      <EditRoundedIcon fontSize='small' />
    //                                 </Button>
    //                             </TableCell>

    //                             <TableCell sx={{ textAlign: 'center' }}>
    //                                 <Button
    //                                     size='small'
    //                                     color='primary'
    //                                     disableElevation
    //                                     variant='outlined'
    //                                     onClick={() => deletePeopleUser(row.id)}
    //                                 >
    //                                     <DeleteRoundedIcon sx={{ color: 'red' }} fontSize='small' />
    //                                 </Button>
    //                             </TableCell>

    //                         </TableRow>
    //                     ))}
    //                 </TableBody>

    //                 <TableFooter>
    //                     {isLoadind && (
    //                         <TableRow>
    //                             <TableCell colSpan={7}>
    //                                 <LinearProgress />
    //                             </TableCell>
    //                         </TableRow>
    //                     )}
    //                 </TableFooter>
    //             </Table>
    //         </TableContainer>

    //     </Box>
    // )



  // const navigate = useNavigate();
  // const smDown = useMediaQuery(useTheme().breakpoints.down('sm'));
  const { getUsers, getCount } = useContext(AccountContext);
  const [count, setCount] = useState<number>();
  const [globalFilter, setGlobalFilter] = useState<string>('');
  const [columnFilters, setColumnFilters] = useState<MRT_ColumnFiltersState>([]);
  const [sortFilters, setSortFilters] = useState<MRT_SortingState>([{ id: 'id', desc: false }]);
  const [columnVisibleState, setColumnVisibleState] = useState<MRT_VisibilityState>(LocalStorage.getColumnVisibilityUser());
  const [pagination, setPagination] = useState<MRT_PaginationState>({ pageIndex: 0, pageSize: LocalStorage.getPageSize() });
  const [users, setUsers] = useState<iUser[]>([]);
  const [filters, setFilters] = useState<iUserFilter>({
    page: { page: pagination.pageIndex, pageSize: pagination.pageSize },
  });

  const columns = useMemo<MRT_ColumnDef<iUser>[]>(
    () => [
      {
        accessorKey: 'userName',
        header: 'Nome de Usuário',
        size: 150,
        enableColumnActions: false,
      },
      {
        accessorKey: 'email',
        header: 'Email',
        size: 150,
        enableColumnActions: false,
        enableSorting: false,
      },
      {
        accessorKey: 'phoneNumber',
        header: 'Telefone',
        size: 100,
        enableColumnActions: false,
        enableSorting: false,
      },
      {
        accessorKey: 'roles',
        header: 'Função',
        size: 100,
        enableColumnActions: false,
        enableSorting: false,
        Cell: ({ row }) => {
          const roles = row.original.roles;
          return !roles ? '' : roles.join(', ');
        },
      },
    ],
    [],
  );

  const fetchUsers = async () => {
    const users = await getUsers(filters);
    setUsers(users);
  };

  const fetchCount = async () => {
    const count = await getCount();
    setCount(count);
  };

  useEffect(() => {
    fetchCount();
  }, []);

  useEffect(() => {
    setFilters({ ...filters, globalFilter: globalFilter });
  }, [globalFilter]);
 
  useEffect(() => {
    const updatedFilters = { ...filters };
    const columnIds = columnFilters.map(columnFilter => columnFilter.id);
    let value: unknown;
 
    value = columnFilters.find(cf => cf.id === 'userName')?.value;
    if (columnIds.includes('userName') && typeof value === 'string')
      updatedFilters.userName = value;
    else updatedFilters.userName = undefined;
 
    value = columnFilters.find(cf => cf.id === 'email')?.value;
    if (columnIds.includes('email') && typeof value === 'string')
      updatedFilters.email = value;
    else updatedFilters.email = undefined;
 
    value = columnFilters.find(cf => cf.id === 'phoneNumber')?.value;
    if (columnIds.includes('phoneNumber') && typeof value === 'string')
      updatedFilters.phoneNumber = value;
    else updatedFilters.phoneNumber = undefined;
 
    value = columnFilters.find(cf => cf.id === 'roles')?.value;
    if (columnIds.includes('roles') && typeof value === 'string') {
      if ('administrador'.includes(value.toLowerCase())) updatedFilters.role = RoleString.administrador;
      else if ('assistente social'.includes(value.toLowerCase())) updatedFilters.role = RoleString.assistenteSocial;
      else if ('secretário'.includes(value.toLowerCase())) updatedFilters.role = RoleString.secretario;
      else if ('voluntário'.includes(value.toLowerCase())) updatedFilters.role = RoleString.voluntario;
      else if ('desenvolvedor'.includes(value.toLowerCase())) updatedFilters.role = RoleString.desenvolvedor;
    }
    else updatedFilters.role = undefined;
 
    setFilters(updatedFilters);
  }, [columnFilters]);
 
  useEffect(() => {
    const updatedFilters = { ...filters };
    const columnIds = sortFilters.map(sort => sort.id);
   
    const desc = sortFilters.find(cf => cf.id === 'userName')?.desc;
    if (columnIds.includes('userName') && typeof desc === 'boolean')
      updatedFilters.orderBy = desc ? iOrderBy.descendent : iOrderBy.ascendent;
    else updatedFilters.orderBy = undefined;
 
    setFilters(updatedFilters);
  }, [sortFilters]);

  useEffect(() => {
    LocalStorage.setColumnVisibilityUser(columnVisibleState);
  }, [columnVisibleState]);
 
  useEffect(() => {
    const page: iPage = { page: pagination.pageIndex, pageSize: pagination.pageSize };
    setFilters({ ...filters, page: page });

    LocalStorage.setPageSize(page.pageSize!);
    
    const fetchUsers = async () => setUsers(await getUsers({ ...filters, page: page }));
    fetchUsers();
  }, [pagination]);

  const renderTopToolbar = (table: MRT_TableInstance<iUser>) => (
    <Box display='flex' flexDirection='column' gap={2} paddingRight='auto'>
      <Typography variant={smDown ? "h6" : "h5"} display='flex' alignItems='center' gap={1} paddingRight='auto' >
        <PersonIcon fontSize={smDown ? "medium" : "large"} color='primary' />
        Usuários
      </Typography>
      <Box display='flex' gap={2}>
        <Button variant='contained' size='small' startIcon={<AddIcon />} onClick={() => navigate('/inicio/pessoas/gerenciar/cadastrar')}>
          Novo
        </Button>
        <Box display='flex' alignItems='center'>
        </Box>
        <Button variant='contained' size='small' startIcon={<ContentPasteSearchIcon />} onClick={fetchUsers}>
          Buscar
        </Button>
      </Box>
    </Box>
  );

  const renderActions = (row: MRT_Row<iUser>) => (
    <Box display='flex' flexDirection='row' flexWrap='nowrap'>
      <IconButton onClick={() => navigate(`/inicio/pessoa/editar/${row.original.id}`)}>
        <EditRoundedIcon />
      </IconButton>
      <IconButton onClick={() => navigate(`/inicio/pessoas/dados/${row.original.id}`)}>
        <InfoOutlinedIcon />
      </IconButton>
    </Box>
  );

  const table = useMaterialReactTable<iUser>({
    columns,
    data: users,
    state: { 
      sorting: sortFilters, 
      pagination: pagination,
      columnVisibility: columnVisibleState,
    },
 
    renderTopToolbarCustomActions: ({ table }) => renderTopToolbar(table),
 
    enableRowActions: true,
    renderRowActions: ({ row, cell, table }) => renderActions(row),
 
    manualFiltering: true,
    onGlobalFilterChange: setGlobalFilter,
    onColumnFiltersChange: setColumnFilters,
 
    manualSorting: true,
    onSortingChange: setSortFilters,

    onColumnVisibilityChange: setColumnVisibleState,
 
    manualPagination: true,
    onPaginationChange: setPagination,
    paginationDisplayMode: 'pages',
    rowCount: count,
 
    localization: MRT_Localization_PT_BR,
  });

  return <MaterialReactTable table={table} />


}