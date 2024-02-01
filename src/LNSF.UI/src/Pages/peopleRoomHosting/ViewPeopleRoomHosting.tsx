import { useContext, useMemo, useState } from 'react'
import { useEffect } from "react"
import { PeopleRoomHostingContext, iPeopleRoomHostingFilter, iPeopleRoomHostingObject } from '../../Contexts';
import { Box, Button, Checkbox, IconButton, Typography, useMediaQuery, useTheme } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import PersonIcon from '@mui/icons-material/Person';
import EditRoundedIcon from '@mui/icons-material/EditRounded';
import ContentPasteSearchIcon from '@mui/icons-material/ContentPasteSearch';
import AddIcon from '@mui/icons-material/Add';
import InfoOutlinedIcon from '@mui/icons-material/InfoOutlined';
import { MRT_ColumnDef, MRT_ColumnFiltersState, MRT_PaginationState, MRT_Row, MRT_SortingState, MRT_TableInstance, MRT_VisibilityState, MaterialReactTable, useMaterialReactTable } from 'material-react-table';
import { LocalStorage } from '../../Global';
import { iOrderBy, iPage } from '../../Contexts/types';
import { MRT_Localization_PT_BR } from 'material-react-table/locales/pt-BR';
import { format, parseISO } from 'date-fns';

export const ViewPeopleRoomHosting: React.FC = () => {
  const navigate = useNavigate();
  const theme = useTheme();
  const smDown = useMediaQuery(theme.breakpoints.down('sm'));
  const { getPeoplesRoomsHostings, getCount } = useContext(PeopleRoomHostingContext);
  const [count, setCount] = useState<number>();
  const [globalFilter, setGlobalFilter] = useState<string>('');
  const [columnFilters, setColumnFilters] = useState<MRT_ColumnFiltersState>([]);
  const [sortFilters, setSortFilters] = useState<MRT_SortingState>([{ id: 'id', desc: false }]);
  const [columnVisibleState, setColumnVisibleState] = useState<MRT_VisibilityState>(LocalStorage.getColumnPeopleRoomHosting());
  const [pagination, setPagination] = useState<MRT_PaginationState>({ pageIndex: 0, pageSize: LocalStorage.getPageSize() });
  const [isFullScreen, setIsFullScreen] = useState<boolean>(false);
  const [peoplesRoomsHostings, setPeoples] = useState<iPeopleRoomHostingObject[]>([]);
  const [active, setActive] = useState<boolean>(true);
  const [filters, setFilters] = useState<iPeopleRoomHostingFilter>({
    page: { page: pagination.pageIndex, pageSize: pagination.pageSize },
    active: active,
    getPeople: true,
    getRoom: true,
    getHosting: true,
  });

  const columns = useMemo<MRT_ColumnDef<iPeopleRoomHostingObject>[]>(
    () => [
      {
        accessorKey: 'people.name',
        header: 'Nome',
        size: 150,
        enableColumnActions: false,
        enableSorting: false,
        enableColumnFilter: false,
      },
      {
        accessorKey: 'people.rg',
        header: 'RG',
        size: 50,
        enableColumnActions: false,
        enableSorting: false,
        enableColumnFilter: false,
      },
      {
        accessorKey: 'people.cpf',
        header: 'CPF',
        size: 50,
        enableColumnActions: false,
        enableSorting: false,
        enableColumnFilter: false,
      },
      {
        accessorKey: 'people.note',
        header: 'Observação',
        size: 200,
        enableColumnActions: false,
        enableSorting: false,
        enableColumnFilter: false,
      },
      {
        accessorKey: 'room.number',
        header: 'Quarto',
        size: 50,
        enableColumnActions: false,
        enableSorting: false,
        enableColumnFilter: false,
      },
      {
        accessorKey: 'hosting.checkIn',
        header: 'Check-in',
        size: 50,
        enableColumnActions: false,
        Cell: ({ row }) => {
          const checkIn = row.original.hosting.checkIn;
          return format(parseISO(checkIn.toString()), 'dd/MM/yyyy HH:mm')
        }
      },
      {
        accessorKey: 'hosting.checkOut',
        header: 'Check-out',
        size: 50,
        enableColumnActions: false,
        enableSorting: false,
        Cell: ({ row }) => {
          const checkOut = row.original.hosting.checkOut;
          return format(parseISO(checkOut.toString()), 'dd/MM/yyyy HH:mm')
        }
      }
    ],
    [],
  );

  const fetchPeoplesRoomsHostings = async () => {
    const peoplesRoomsHostings = await getPeoplesRoomsHostings(filters);
    setPeoples(peoplesRoomsHostings);
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

    value = columnFilters.find(cf => cf.id === 'checkIn')?.value;
    if (columnIds.includes('checkIn') && value instanceof Date)
      updatedFilters.checkIn = value;
    else updatedFilters.checkIn = undefined;

    value = columnFilters.find(cf => cf.id === 'checkOut')?.value;
    if (columnIds.includes('checkOut') && value instanceof Date)
      updatedFilters.checkOut = value;
    else updatedFilters.checkOut = undefined;

    setFilters(updatedFilters);
  }, [columnFilters]);

  useEffect(() => {
    const updatedFilters = { ...filters };
    const columnIds = sortFilters.map(sort => sort.id);

    // const desc = sortFilters.find(cf => cf.id === 'name')?.desc;
    // if (columnIds.includes('name') && typeof desc === 'boolean')
    //     updatedFilters.orderBy = desc ? iOrderBy.descendent : iOrderBy.ascendent;
    // else updatedFilters.orderBy = undefined;

    setFilters(updatedFilters);
  }, [sortFilters]);

  useEffect(() => {
    LocalStorage.setColumnPeopleRoomHosting(columnVisibleState);
  }, [columnVisibleState]);

  useEffect(() => {
    const page: iPage = { page: pagination.pageIndex, pageSize: pagination.pageSize };
    setFilters({ ...filters, page: page });

    LocalStorage.setPageSize(page.pageSize!);

    const fetchPeoplesRoomsHostings = async () => setPeoples(await getPeoplesRoomsHostings({ ...filters, page: page }));
    fetchPeoplesRoomsHostings();
  }, [pagination]);

  useEffect(() => {
    setFilters({ ...filters, active: active });
  }, [active]);

  const renderTopToolbar = (table: MRT_TableInstance<iPeopleRoomHostingObject>) => (
    <Box display='flex' flexDirection='column' gap={2} paddingRight='auto'>
      <Typography variant={smDown ? "h6" : "h5"} display='flex' alignItems='center' gap={1} paddingRight='auto' >
        <PersonIcon fontSize={smDown ? "medium" : "large"} color='primary' />
        Pessoas Hospedadas
      </Typography>
      <Box display='flex' gap={2}>
        <Button variant='contained' size='small' startIcon={<AddIcon />} onClick={() => navigate('/pessoas/gerenciar/cadastrar')}>
          Novo
        </Button>
        <Box display='flex' alignItems='center'>
          <Typography>Ativos</Typography>
          <Checkbox checked={active} onChange={(e) => setActive(e.target.checked)} />
        </Box>
        <Button variant='contained' size='small' startIcon={<ContentPasteSearchIcon />} onClick={fetchPeoplesRoomsHostings}>
          Buscar
        </Button>
      </Box>
    </Box>
  );

  const renderActions = (row: MRT_Row<iPeopleRoomHostingObject>) => (
    <Box display='flex' flexDirection='row' flexWrap='nowrap'>
      <IconButton onClick={() => navigate(`/pessoa/editar/${row.original.peopleId}`)}>
        <EditRoundedIcon />
      </IconButton>
      <IconButton onClick={() => navigate(`/pessoas/dados/${row.original.peopleId}`)}>
        <InfoOutlinedIcon />
      </IconButton>
    </Box>
  );

  const table = useMaterialReactTable<iPeopleRoomHostingObject>({
    columns,
    data: peoplesRoomsHostings,
    state: {
      sorting: sortFilters,
      pagination: pagination,
      columnVisibility: columnVisibleState,
      isFullScreen,
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

    onIsFullScreenChange: () => setIsFullScreen(!isFullScreen),

    muiTablePaperProps: ({ table }) => ({
      style: {
        zIndex: isFullScreen ? 10000 : undefined,
      }
    }),

    mrtTheme: {
      baseBackgroundColor: theme.palette.background.paper,
    },

    localization: MRT_Localization_PT_BR,
  });

  return <MaterialReactTable table={table} />
}