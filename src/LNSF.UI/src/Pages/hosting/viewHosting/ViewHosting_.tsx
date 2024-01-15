import IconButton from '@mui/material/IconButton';
import Typography from '@mui/material/Typography';
import { useContext, useEffect, useMemo, useState } from 'react';
import { Box, Button, Checkbox, useMediaQuery, useTheme } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { HostingContext } from '../../../Contexts/hostingContext';
import { iHostingFilter, iHostingObject } from '../../../Contexts/hostingContext/type';
import { format } from 'date-fns';
import InfoOutlinedIcon from '@mui/icons-material/InfoOutlined';
import EditRoundedIcon from '@mui/icons-material/EditRounded';
import BedIcon from '@mui/icons-material/Bed';
import AddIcon from '@mui/icons-material/Add';
import ContentPasteSearchIcon from '@mui/icons-material/ContentPasteSearch';
import { MRT_ColumnDef, MRT_ColumnFiltersState, MRT_PaginationState, MRT_Row, MRT_SortingState, MRT_TableInstance, MRT_VisibilityState, MaterialReactTable, useMaterialReactTable } from 'material-react-table';
import { LocalStorage } from '../../../Global';
import { iOrderBy, iPage } from '../../../Contexts/types';
import { MRT_Localization_PT_BR } from 'material-react-table/locales/pt-BR';

export const Hosting: React.FC = () => {
  const navigate = useNavigate();
  const theme = useTheme();
  const smDown = useMediaQuery(theme.breakpoints.down('sm'));
  const { getHostings, getCount } = useContext(HostingContext);
  const [count, setCount] = useState<number>();
  const [globalFilter, setGlobalFilter] = useState<string>('');
  const [columnFilters, setColumnFilters] = useState<MRT_ColumnFiltersState>([]);
  const [sortFilters, setSortFilters] = useState<MRT_SortingState>([{ id: 'id', desc: false }]);
  const [columnVisibleState, setColumnVisibleState] = useState<MRT_VisibilityState>(LocalStorage.getColumnVisibilityHosting());
  const [pagination, setPagination] = useState<MRT_PaginationState>({ pageIndex: 0, pageSize: LocalStorage.getPageSize() });
  const [isFullScreen, setIsFullScreen] = useState<boolean>(false);
  const [hostings, setHostings] = useState<iHostingObject[]>([]);
  const [activeFilter, setActiveFilter] = useState<boolean>(true);
  const [filters, setFilters] = useState<iHostingFilter>({
    getPatient: true,
    getPatientPeople: true,
    getEscort: true,
    getEscortPeople: true,
    page: { page: pagination.pageIndex, pageSize: pagination.pageSize },
    active: activeFilter,
  });

  const columns = useMemo<MRT_ColumnDef<iHostingObject>[]>(
    () => [
      {
        accessorKey: 'patient.people.name',
        header: 'Paciente',
        size: 150,
        enableColumnActions: false,
        enableSorting: false,
        enableColumnFilter: false,
      },
      {
        accessorKey: 'patient.people.rg',
        header: 'RG',
        size: 50,
        enableColumnActions: false,
        enableSorting: false,
        enableColumnFilter: false,
      },
      {
        accessorKey: 'patient.people.cpf',
        header: 'CPF',
        size: 50,
        enableColumnActions: false,
        enableSorting: false,
        enableColumnFilter: false,
      },
      {
        accessorKey: 'escorts.people.name',
        header: 'Acompanhante',
        size: 150,
        enableColumnActions: false,
        enableSorting: false,
        enableColumnFilter: false,
        Cell: ({ row }) => {
          const escorts = row.original.escorts;
          return !escorts ? '' : escorts.length <= 0 ? '' : 
            escorts.map(e => e.people!.name).join(', ');
        },
      },
      {
        accessorKey: 'checkIn',
        header: 'Check-in',
        size: 100,
        enableColumnActions: false,
        Cell: ({ row }) => {
          const date = new Date(row.original.checkIn);
          return date ? format(date, 'dd/MM/yyyy HH:mm') : '';
        },
      },
      {
        accessorKey: 'checkOut',
        header: 'Check-out',
        size: 100,
        enableColumnActions: false,
        enableSorting: false,
        Cell: ({ row }) => {
          const date = new Date(row.original.checkOut);
          return date ? format(date, 'dd/MM/yyyy HH:mm') : '';
        },
      },
    ],
    [],
  );

  const fetchHostings = async () => {
    const hostings = await getHostings(filters);
    setHostings(hostings);
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
    
    const desc = sortFilters.find(cf => cf.id === 'checkIn')?.desc;
    if (columnIds.includes('checkIn') && typeof desc === 'boolean')
      updatedFilters.orderBy = desc ? iOrderBy.descendent : iOrderBy.ascendent;
    else updatedFilters.orderBy = undefined;
  
    setFilters(updatedFilters);
  }, [sortFilters]);

  useEffect(() => {
    LocalStorage.setColumnVisibilityHosting(columnVisibleState);
  }, [columnVisibleState]);
  
  useEffect(() => {
    const page: iPage = { page: pagination.pageIndex, pageSize: pagination.pageSize };
    setFilters({ ...filters, page: page });

    LocalStorage.setPageSize(page.pageSize!);
    
    const fetchHostings = async () => setHostings(await getHostings({ ...filters, page: page }));
    fetchHostings();
  }, [pagination]);

  useEffect(() => {
    setFilters({ ...filters, active: activeFilter });
  }, [activeFilter]);

  const renderTopToolbar = (table: MRT_TableInstance<iHostingObject>) => (
    <Box display='flex' flexDirection='column' gap={2} paddingRight='auto'>
      <Typography variant={smDown ? "h6" : "h5"} display='flex' alignItems='center' gap={1} paddingRight='auto' >
        <BedIcon fontSize={smDown ? "medium" : "large"} color='primary' />
        Hospedagem: Check-in / Check-out
      </Typography>
      <Box display='flex' gap={2}>
        <Button variant='contained' size='small' startIcon={<AddIcon />} onClick={() => navigate('/inicio/pessoas/gerenciar/cadastrar')}>
          Novo
        </Button>
        <Box display='flex' alignItems='center'>
          <Typography>
            Ativos
          </Typography>
          <Checkbox checked={activeFilter} onChange={(e) => setActiveFilter(e.target.checked)} />
        </Box>
        <Button variant='contained' size='small' startIcon={<ContentPasteSearchIcon />} onClick={fetchHostings}>
          Buscar
        </Button>
      </Box>
    </Box>
  );

  const renderActions = (row: MRT_Row<iHostingObject>) => (
    <Box display='flex' flexDirection='row' flexWrap='nowrap'>
      <IconButton onClick={() => navigate(`/inicio/pessoa/editar/${row.original.id}`)}>
        <EditRoundedIcon />
      </IconButton>
      <IconButton onClick={() => navigate(`/inicio/pessoas/dados/${row.original.id}`)}>
        <InfoOutlinedIcon />
      </IconButton>
    </Box>
  );

  const table = useMaterialReactTable<iHostingObject>({
    columns,
    data: hostings,
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
    enableGlobalFilter: false,
    onColumnFiltersChange: setColumnFilters,
  
    manualSorting: true,
    onSortingChange: setSortFilters,

    onColumnVisibilityChange: setColumnVisibleState,
  
    manualPagination: true,
    onPaginationChange: setPagination,
    paginationDisplayMode: 'pages',
    rowCount: count,

    onIsFullScreenChange: () => setIsFullScreen(!isFullScreen),

    muiTablePaperProps: ({ table }) => ({ style: {
      zIndex: isFullScreen ? 10000 : undefined,
    }}),

    mrtTheme : {
      baseBackgroundColor: theme.palette.background.paper,
    },
  
    localization: MRT_Localization_PT_BR,
  });

  return <MaterialReactTable table={table} />
}