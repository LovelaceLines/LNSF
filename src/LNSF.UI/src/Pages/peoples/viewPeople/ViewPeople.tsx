import { useContext, useMemo, useState } from 'react'
import { useEffect } from "react"
import { Gender, PeopleContext, iPeopleFilter, iPeopleObject } from '../../../Contexts';
import { Box, Button, Checkbox, IconButton, Typography, useMediaQuery, useTheme } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import PersonIcon from '@mui/icons-material/Person';
import EditRoundedIcon from '@mui/icons-material/EditRounded';
import ContentPasteSearchIcon from '@mui/icons-material/ContentPasteSearch';
import AddIcon from '@mui/icons-material/Add';
import InfoOutlinedIcon from '@mui/icons-material/InfoOutlined';
import { MRT_ColumnDef, MRT_ColumnFiltersState, MRT_PaginationState, MRT_Row, MRT_SortingState, MRT_TableInstance, MRT_VisibilityState, MaterialReactTable, useMaterialReactTable } from 'material-react-table';
import { LocalStorage } from '../../../Global';
import { iOrderBy, iPage } from '../../../Contexts/types';
import { MRT_Localization_PT_BR } from 'material-react-table/locales/pt-BR';
import { format } from 'date-fns';

export const ViewPeople: React.FC = () => {
  const navigate = useNavigate();
  const theme = useTheme();
  const smDown = useMediaQuery(theme.breakpoints.down('sm'));
  const { getPeoples, getCount } = useContext(PeopleContext);
  const [count, setCount] = useState<number>();
  const [globalFilter, setGlobalFilter] = useState<string>('');
  const [columnFilters, setColumnFilters] = useState<MRT_ColumnFiltersState>([]);
  const [sortFilters, setSortFilters] = useState<MRT_SortingState>([{ id: 'id', desc: false }]);
  const [columnVisibleState, setColumnVisibleState] = useState<MRT_VisibilityState>(LocalStorage.getColumnVisibilityPeople());
  const [pagination, setPagination] = useState<MRT_PaginationState>({ pageIndex: 0, pageSize: LocalStorage.getPageSize() });
  const [isFullScreen, setIsFullScreen] = useState<boolean>(false);
  const [peoples, setPeoples] = useState<iPeopleObject[]>([]);
  const [activeFilter, setActiveFilter] = useState<boolean>(true);
  const [PatientFilter, setPatientFilter] = useState<boolean>(true);
  const [EscortFilter, setEscortFilter] = useState<boolean>(false);
  const [filters, setFilters] = useState<iPeopleFilter>({
    patient: PatientFilter,
    escort: EscortFilter,
    page: { page: pagination.pageIndex, pageSize: pagination.pageSize },
    active: activeFilter,
  });

  const columns = useMemo<MRT_ColumnDef<iPeopleObject>[]>(
    () => [
      {
        accessorKey: 'name',
        header: 'Nome',
        size: 150,
        enableColumnActions: false,
      },
      {
        accessorKey: 'rg',
        header: 'RG',
        size: 50,
        enableColumnActions: false,
        enableSorting: false,
      },
      {
        accessorKey: 'cpf',
        header: 'CPF',
        size: 50,
        enableColumnActions: false,
        enableSorting: false,
      },
      {
        accessorKey: 'gender',
        header: 'Sexo',
        size: 50,
        enableColumnActions: false,
        enableSorting: false,
        Cell: ({ row }) => {
          const gender = row.original.gender;
          return gender == 0 ? 'Masculino' : gender == 1 ? 'Feminino' : '';
        },
      },
      {
        accessorKey: 'phone',
        header: 'Telefone',
        size: 150,
        enableColumnActions: false,
        enableSorting: false,
      },
      {
        accessorKey: 'birthDate',
        header: 'Data de Nascimento',
        size: 50,
        enableColumnActions: false,
        enableSorting: false,
        Cell: ({ row }) => {
          const date = new Date(row.original.birthDate);
          return date ? format(date, 'dd/MM/yyyy') : '';
        },
      },
      {
        accessorKey: 'city',
        header: 'Cidade',
        size: 50,
        enableColumnActions: false,
        enableSorting: false,
      },
      {
        accessorKey: 'state',
        header: 'Estado',
        size: 50,
        enableColumnActions: false,
        enableSorting: false,
      },
      {
        accessorKey: 'neighborhood',
        header: 'Bairro',
        size: 50,
        enableColumnActions: false,
        enableSorting: false,
      },
      {
        accessorKey: 'street',
        header: 'Rua',
        size: 50,
        enableColumnActions: false,
        enableSorting: false,
      },
      {
        accessorKey: 'houseNumber',
        header: 'Número',
        size: 50,
        enableColumnActions: false,
        enableSorting: false,
      },
      {
        accessorKey: 'note',
        header: 'Observação',
        size: 200,
        enableColumnActions: false,
        enableSorting: false,
      },
    ],
    [],
  );

  const fetchPeoples = async () => {
    const peoples = await getPeoples(filters);
    setPeoples(peoples);
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
 
    value = columnFilters.find(cf => cf.id === 'name')?.value;
    if (columnIds.includes('name') && typeof value === 'string')
      updatedFilters.name = value;
    else updatedFilters.name = undefined;
 
    value = columnFilters.find(cf => cf.id === 'rg')?.value;
    if (columnIds.includes('rg') && typeof value === 'string')
      updatedFilters.rg = value;
    else updatedFilters.rg = undefined;
 
    value = columnFilters.find(cf => cf.id === 'cpf')?.value;
    if (columnIds.includes('cpf') && typeof value === 'string')
      updatedFilters.cpf = value;
    else updatedFilters.cpf = undefined;
 
    value = columnFilters.find(cf => cf.id === 'phone')?.value;
    if (columnIds.includes('phone') && typeof value === 'string')
      updatedFilters.phone = value;
    else updatedFilters.phone = undefined;

    value = columnFilters.find(cf => cf.id === 'gender')?.value;
    if (columnIds.includes('gender') && typeof value === 'string') {
      if ('masculino'.includes(value.toLowerCase())) updatedFilters.gender = Gender.male;
      else if ('feminino'.includes(value.toLowerCase())) updatedFilters.gender = Gender.female;
    } else updatedFilters.gender = undefined;

    value = columnFilters.find(cf => cf.id === 'birthDate')?.value;
    if (columnIds.includes('birthDate') && value instanceof Date)
      updatedFilters.birthDate = value;
    else updatedFilters.birthDate = undefined;

    value = columnFilters.find(cf => cf.id === 'city')?.value;
    if (columnIds.includes('city') && typeof value === 'string')
      updatedFilters.city = value;
    else updatedFilters.city = undefined;

    value = columnFilters.find(cf => cf.id === 'state')?.value;
    if (columnIds.includes('state') && typeof value === 'string')
      updatedFilters.state = value;
    else updatedFilters.state = undefined;

    value = columnFilters.find(cf => cf.id === 'neighborhood')?.value; 
    if (columnIds.includes('neighborhood') && typeof value === 'string')
      updatedFilters.neighborhood = value;
    else updatedFilters.neighborhood = undefined;

    value = columnFilters.find(cf => cf.id === 'street')?.value;
    if (columnIds.includes('street') && typeof value === 'string')
      updatedFilters.street = value;
    else updatedFilters.street = undefined;

    value = columnFilters.find(cf => cf.id === 'houseNumber')?.value;
    if (columnIds.includes('houseNumber') && typeof value === 'string')
      updatedFilters.houseNumber = value;
    else updatedFilters.houseNumber = undefined;

    value = columnFilters.find(cf => cf.id === 'note')?.value;
    if (columnIds.includes('note') && typeof value === 'string')
      updatedFilters.note = value;
    else updatedFilters.note = undefined;
 
    setFilters(updatedFilters);
  }, [columnFilters]);
 
  useEffect(() => {
    const updatedFilters = { ...filters };
    const columnIds = sortFilters.map(sort => sort.id);
   
    const desc = sortFilters.find(cf => cf.id === 'name')?.desc;
    if (columnIds.includes('name') && typeof desc === 'boolean')
      updatedFilters.orderBy = desc ? iOrderBy.descendent : iOrderBy.ascendent;
    else updatedFilters.orderBy = undefined;
 
    setFilters(updatedFilters);
  }, [sortFilters]);

  useEffect(() => {
    LocalStorage.setColumnVisibilityPeople(columnVisibleState);
  }, [columnVisibleState]);
 
  useEffect(() => {
    const page: iPage = { page: pagination.pageIndex, pageSize: pagination.pageSize };
    setFilters({ ...filters, page: page });

    LocalStorage.setPageSize(page.pageSize!);
    
    const fetchPeoples = async () => setPeoples(await getPeoples({ ...filters, page: page }));
    fetchPeoples();
  }, [pagination]);

  useEffect(() => {
    setFilters({ ...filters, active: activeFilter });
  }, [activeFilter]);

  useEffect(() => {
    setFilters({ ...filters, patient: PatientFilter });
  }, [PatientFilter]);

  useEffect(() => {
    setFilters({ ...filters, escort: EscortFilter });
  }, [EscortFilter]);

  const renderTopToolbar = (table: MRT_TableInstance<iPeopleObject>) => (
    <Box display='flex' flexDirection='column' gap={2} paddingRight='auto'>
      <Typography variant={smDown ? "h6" : "h5"} display='flex' alignItems='center' gap={1} paddingRight='auto' >
        <PersonIcon fontSize={smDown ? "medium" : "large"} color='primary' />
        Pessoas
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
          <Typography>
            Pacientes
          </Typography>
          <Checkbox checked={PatientFilter} onChange={(e) => setPatientFilter(e.target.checked)} />
          <Typography>
            Acompanhantes
          </Typography>
          <Checkbox checked={EscortFilter} onChange={(e) => setEscortFilter(e.target.checked)} />
        </Box>
        <Button variant='contained' size='small' startIcon={<ContentPasteSearchIcon />} onClick={fetchPeoples}>
          Buscar
        </Button>
      </Box>
    </Box>
  );

  const renderActions = (row: MRT_Row<iPeopleObject>) => (
    <Box display='flex' flexDirection='row' flexWrap='nowrap'>
      <IconButton onClick={() => navigate(`/inicio/pessoa/editar/${row.original.id}`)}>
        <EditRoundedIcon />
      </IconButton>
      <IconButton onClick={() => navigate(`/inicio/pessoas/dados/${row.original.id}`)}>
        <InfoOutlinedIcon />
      </IconButton>
    </Box>
  );

  const table = useMaterialReactTable<iPeopleObject>({
    columns,
    data: peoples,
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