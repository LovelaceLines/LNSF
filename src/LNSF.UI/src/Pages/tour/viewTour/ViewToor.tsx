import { useContext, useState, useMemo, useCallback } from 'react'
import { MaterialReactTable, useMaterialReactTable, type MRT_ColumnDef, MRT_TableOptions, MRT_GlobalFilterTextField, MRT_Cell, MRT_Row, MRT_TableInstance, MRT_ColumnFiltersState, MRT_SortingState, MRT_PaginationState } from 'material-react-table';
import { MRT_Localization_PT_BR } from 'material-react-table/locales/pt-BR';
import { useEffect } from "react"
import SearchIcon from '@mui/icons-material/Search';
import DeleteIcon from '@mui/icons-material/Delete';
import AddIcon from '@mui/icons-material/Add';
import ContentPasteSearchIcon from '@mui/icons-material/ContentPasteSearch';
import { PeopleContext, RoomContext, TourContext, iTourFilter, iTourObject, iTourPeopleRoom } from '../../../Contexts';
import { Box, Button, Checkbox, Divider, Icon, IconButton, LinearProgress, Paper, Table, TableBody, TableCell, TableContainer, TableFooter, TableRow, Toolbar, Typography, useMediaQuery, useTheme } from '@mui/material';
import DescriptionRoundedIcon from '@mui/icons-material/DescriptionRounded';
import { format, parseISO, set } from 'date-fns';
import EditRoundedIcon from '@mui/icons-material/EditRounded';
import { useNavigate } from 'react-router-dom';
import { iOrderBy, iPage } from '../../../Contexts/types';
import { CheckBox } from '@mui/icons-material';

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
  const [filters, setFilters] = useState<iTourFilter>({getPeople: true});
  const [pagination, setPagination] = useState<MRT_PaginationState>({ pageIndex: 0, pageSize: 10 });
  const [columnFilters, setColumnFilters] = useState<MRT_ColumnFiltersState>([]);
  const [sorting, setSorting] = useState<MRT_SortingState>([]);
    
  const fetchTours = async () => {
    console.log(filters);
    const tours = await getTours(filters);
    setTours(tours);
  }
  
  useEffect(() => {
    fetchTours();
  }, []);

  const columns = useMemo<MRT_ColumnDef<iTourObject>[]>(
    () => [
      {
        accessorKey: 'people.name',
        header: 'Nome',
        size: 100,
        enableColumnActions: false,
        enableSorting: false,
      },
      {
        accessorKey: 'people.rg',
        header: 'RG',
        size: 100,
        enableColumnActions: false,
        enableSorting: false,
      },
      {
        accessorKey: 'people.cpf',
        header: 'CPF',
        size: 100,
        enableColumnActions: false,
        enableSorting: false,
      },
      {
        accessorKey: 'output',
        header: 'Saída',
        size: 100,
        enableColumnActions: false,
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
        enableColumnActions: false,
        enableSorting: false,
      },
    ],
    [],
  );

  const renderTopToolbar = (table: MRT_TableInstance<iTourObject>) => (
    <Box display='flex' flexDirection='column' gap={2} paddingRight='auto'>
      <Typography variant={smDown ? "h6" : "h5"} display='flex' alignItems='center' gap={1} paddingRight='auto' >
        <DescriptionRoundedIcon fontSize={smDown ? "medium" : "large"} color='primary' />
        Histórico de entrada e saída
      </Typography>
      <Box display='flex' gap={2}>
        <Button
          variant='contained'
          startIcon={<AddIcon />}
          size='small'
        >
          Novo
        </Button>
        <Box display='flex' alignItems='center'>
          <Typography>
            Saídas sem entradas
          </Typography>
          <Checkbox 
            name="jason" 
            defaultChecked
            onChange={() => console.log('checked')} 
          />
        </Box>
        <Button
          variant='contained'
          startIcon={<ContentPasteSearchIcon />}
          size='small'
          onClick={fetchTours}
        >
          Buscar
        </Button>
      </Box>
    </Box>
  );

  const renderActions = (row: MRT_Row<iTourObject>) => (
    <Box display='flex' flexDirection='row' flexWrap='nowrap'>
      <IconButton onClick={() => console.log(columnFilters)}>
        <EditRoundedIcon />
      </IconButton> 
      <IconButton onClick={() => console.log(columnFilters)}>
        <DeleteIcon />
      </IconButton>
    </Box>
  );

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
    const columnIds = sorting.map(sort => sort.id);
    
    const desc = sorting.find(cf => cf.id === 'output')?.desc;
    if (columnIds.includes('output') && typeof desc === 'boolean') 
      updatedFilters.orderBy = desc ? iOrderBy.descendent : iOrderBy.ascendent;
    else updatedFilters.orderBy = undefined;

    setFilters(updatedFilters);
  }, [sorting]);

  useEffect(() => {
    const page: iPage = { page: pagination.pageIndex, pageSize: pagination.pageSize };
    setFilters({ ...filters, page });
  }, [pagination]);

  const table = useMaterialReactTable<iTourObject>({
    columns,
    data,

    renderTopToolbarCustomActions: ({ table }) => renderTopToolbar(table),

    enableRowActions: true,
    renderRowActions: ({ row, cell, table }) => renderActions(row),

    manualFiltering: true,
    onGlobalFilterChange: (globalFilter) => setFilters({ ...filters, globalFilter:globalFilter }),
    onColumnFiltersChange: setColumnFilters,

    manualSorting: true,
    // state: { sorting },
    onSortingChange: setSorting,

    manualPagination: true,
    onPaginationChange: setPagination,
    state: { pagination },

    localization: MRT_Localization_PT_BR,
  });

  return <MaterialReactTable table={table} />
}