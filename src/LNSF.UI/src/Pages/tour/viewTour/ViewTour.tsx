import { useContext, useState, useMemo } from 'react'
import { useEffect } from "react"
import { useNavigate } from 'react-router-dom';
import { TourContext, iTourFilter, iTourObject, iTourUpdate } from '../../../Contexts';
import { iOrderBy, iPage } from '../../../Contexts/types';
import { MaterialReactTable, useMaterialReactTable, type MRT_ColumnDef, MRT_Row, MRT_TableInstance, MRT_ColumnFiltersState, MRT_SortingState, MRT_PaginationState, MRT_VisibilityState } from 'material-react-table';
import { MRT_Localization_PT_BR } from 'material-react-table/locales/pt-BR';
import { Box, Button, Checkbox, IconButton, Typography, useMediaQuery, useTheme } from '@mui/material';
import AddIcon from '@mui/icons-material/Add';
import ContentPasteSearchIcon from '@mui/icons-material/ContentPasteSearch';
import CheckIcon from '@mui/icons-material/Check';
import DescriptionRoundedIcon from '@mui/icons-material/DescriptionRounded';
import EditRoundedIcon from '@mui/icons-material/EditRounded';
import { LocalStorage } from '../../../Global/LocalStorage';
import { format, parseISO } from 'date-fns';
 
export const ViewTour: React.FC = () => {
  const navigate = useNavigate();
  const smDown = useMediaQuery(useTheme().breakpoints.down('sm'));
  const { getTours, putTour, getCount } = useContext(TourContext);
  const [count, setCount] = useState<number>();
  const [globalFilter, setGlobalFilter] = useState<string>('');
  const [columnFilters, setColumnFilters] = useState<MRT_ColumnFiltersState>([]);
  const [sortFilters, setSortFilters] = useState<MRT_SortingState>([{ id: "output", desc: true }]);
  const [columnVisibleState, setColumnVisibleState] = useState<MRT_VisibilityState>(LocalStorage.getColumnVisibilityPeople());
  const [pagination, setPagination] = useState<MRT_PaginationState>({ pageIndex: 0, pageSize: LocalStorage.getPageSize() });
  const [tours, setTours] = useState<iTourObject[]>([]);
  const [inOpenFilter, setInOpenFilter] = useState<boolean>(true);
  const [filters, setFilters] = useState<iTourFilter>({
    page: { page: pagination.pageIndex, pageSize: pagination.pageSize },
    orderBy: iOrderBy.descendent,
    inOpen: inOpenFilter, 
    getPeople: true
  });

  const columns = useMemo<MRT_ColumnDef<iTourObject>[]>(
    () => [
      {
        accessorKey: 'people.name',
        header: 'Nome',
        size: 150,
        enableColumnActions: false,
        enableSorting: false,
      },
      {
        accessorKey: 'people.rg',
        header: 'RG',
        size: 50,
        enableColumnActions: false,
        enableSorting: false,
      },
      {
        accessorKey: 'people.cpf',
        header: 'CPF',
        size: 50,
        enableColumnActions: false,
        enableSorting: false,
      },
      {
        accessorKey: 'output',
        header: 'Saída',
        size: 150,
        enableColumnActions: false,
        Cell: ({ row }) => {
          const output = row.original.output;
          return format(parseISO(output.toString()), 'dd/MM/yyyy HH:mm')
        } 
      },
      {
        accessorKey: 'input',
        header: 'Retorno',
        size: 150,
        enableColumnActions: false,
        enableSorting: false,
        Cell: ({ row }) => {
          const input = row.original.input;
          return input ? format(parseISO(input.toString()), 'dd/MM/yyyy HH:mm') :
          (
            <Button variant='contained' fullWidth onClick={() => confirmTourReturn({id: row.original.id, peopleId: row.original.peopleId, note: row.original.note})}>
              <CheckIcon />
            </Button>
          )
        }
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
   
  const fetchTours = async () => {
    const tours = await getTours(filters);
    setTours(tours);
  };

  const fetchCount = async () => {
    const count = await getCount()
    setCount(count);
  };

  const confirmTourReturn = async (tour: iTourUpdate) => {
    await putTour(tour);
    await fetchTours();
  };

  useEffect(() => {
    // fetchTours();
    fetchCount();
  }, []);

  useEffect(() => {
    setFilters({ ...filters, globalFilter: globalFilter });
  }, [globalFilter]);
 
  useEffect(() => {
    const updatedFilters = { ...filters };
    const columnIds = columnFilters.map(columnFilter => columnFilter.id);
    let value: unknown;
 
    value = columnFilters.find(cf => cf.id === 'people.name')?.value;
    if (columnIds.includes('people.name') && typeof value === 'string')
      updatedFilters.peopleName = value;
    else updatedFilters.peopleName = undefined;
 
    value = columnFilters.find(cf => cf.id === 'people.rg')?.value;
    if (columnIds.includes('people.rg') && typeof value === 'string')
      updatedFilters.peopleRG = value;
    else updatedFilters.peopleRG = undefined;
 
    value = columnFilters.find(cf => cf.id === 'people.cpf')?.value;
    if (columnIds.includes('people.cpf') && typeof value === 'string')
      updatedFilters.peopleCPF = value;
    else updatedFilters.peopleCPF = undefined;
 
    value = columnFilters.find(cf => cf.id === 'output')?.value;
    if (columnIds.includes('output') && value instanceof Date)
      updatedFilters.output = value;
    else updatedFilters.output = undefined;
 
    value = columnFilters.find(cf => cf.id === 'input')?.value;
    if (columnIds.includes('input') && value instanceof Date)
      updatedFilters.input = value;
    else updatedFilters.input = undefined;
 
    value = columnFilters.find(cf => cf.id === 'note')?.value;
    if (columnIds.includes('note') && typeof value === 'string')
      updatedFilters.note = value;
    else updatedFilters.note = undefined;
 
    setFilters(updatedFilters);
  }, [columnFilters]);
 
  useEffect(() => {
    const updatedFilters = { ...filters };
    const columnIds = sortFilters.map(sort => sort.id);
   
    const desc = sortFilters.find(cf => cf.id === 'output')?.desc;
    if (columnIds.includes('output') && typeof desc === 'boolean')
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
    
    const fetchTours = async () => setTours(await getTours({ ...filters, page: page }));
    fetchTours();
  }, [pagination]);

  useEffect(() => {
    setFilters({ ...filters, inOpen: inOpenFilter });
  }, [inOpenFilter]);
 
  const renderTopToolbar = (table: MRT_TableInstance<iTourObject>) => (
    <Box display='flex' flexDirection='column' gap={2} paddingRight='auto'>
      <Typography variant={smDown ? "h6" : "h5"} display='flex' alignItems='center' gap={1} paddingRight='auto' >
        <DescriptionRoundedIcon fontSize={smDown ? "medium" : "large"} color='primary' />
        Histórico de entrada e saída
      </Typography>
      <Box display='flex' gap={2}>
        <Button variant='contained' size='small' startIcon={<AddIcon />} onClick={() => navigate('/inicio/registrodiario/cadastrar')}>
          Novo
        </Button>
        <Box display='flex' alignItems='center'>
          <Typography>
            Saídas sem entrada
          </Typography>
          <Checkbox checked={inOpenFilter} onChange={(e) => setInOpenFilter(e.target.checked)} />
        </Box>
        <Button variant='contained' size='small' startIcon={<ContentPasteSearchIcon />} onClick={fetchTours}>
          Buscar
        </Button>
      </Box>
    </Box>
  );
 
  const renderActions = (row: MRT_Row<iTourObject>) => (
    <Box display='flex' flexDirection='row' flexWrap='nowrap'>
      <IconButton onClick={() => navigate(`/inicio/passeio/editar/${row.original.id}`)}>
        <EditRoundedIcon />
      </IconButton>
    </Box>
  );
 
  const table = useMaterialReactTable<iTourObject>({
    columns,
    data: tours,
    state: { 
      sorting: sortFilters, 
      pagination: pagination, 
      columnVisibility: columnVisibleState
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