import { useContext, useMemo, useState } from 'react'
import { useEffect } from "react"
import { Box, Button, IconButton, Typography, useMediaQuery, useTheme } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import EditRoundedIcon from '@mui/icons-material/EditRounded';
import AddIcon from '@mui/icons-material/Add';
import ContentPasteSearchIcon from '@mui/icons-material/ContentPasteSearch';
import { TreatmentContext } from '../../../Contexts/treatmentContext';
import { iTreatment, iTreatmentFilter, iTypeTreatment } from '../../../Contexts/treatmentContext/type';
import VaccinesIcon from '@mui/icons-material/Vaccines';
import { MRT_ColumnDef, MRT_ColumnFiltersState, MRT_PaginationState, MRT_Row, MRT_SortingState, MRT_TableInstance, MRT_VisibilityState, MaterialReactTable, useMaterialReactTable } from 'material-react-table';
import { LocalStorage } from '../../../Global';
import { iOrderBy, iPage } from '../../../Contexts/types';
import { MRT_Localization_PT_BR } from 'material-react-table/locales/pt-BR';

export const ViewTratamentos: React.FC = () => {
  const navigate = useNavigate();
  const theme = useTheme();
  const smDown = useMediaQuery(theme.breakpoints.down('sm'));
  const { getTreatments, getCount } = useContext(TreatmentContext);
  const [count, setCount] = useState<number>();
  const [globalFilter, setGlobalFilter] = useState<string>('');
  const [columnFilters, setColumnFilters] = useState<MRT_ColumnFiltersState>([]);
  const [sortFilters, setSortFilters] = useState<MRT_SortingState>([{ id: 'id', desc: false }]);
  const [columnVisibleState, setColumnVisibleState] = useState<MRT_VisibilityState>(LocalStorage.getColumnVisibilityTreatment());
  const [pagination, setPagination] = useState<MRT_PaginationState>({ pageIndex: 0, pageSize: LocalStorage.getPageSize() });
  const [isFullScreen, setIsFullScreen] = useState<boolean>(false);
  const [treatments, setTreatments] = useState<iTreatment[]>([]);
  const [filters, setFilters] = useState<iTreatmentFilter>({
    page: { page: pagination.pageIndex, pageSize: pagination.pageSize },
  });

  const columns = useMemo<MRT_ColumnDef<iTreatment>[]>(
    () => [
      {
        accessorKey: 'name',
        header: 'Nome',
        size: 150,
        enableColumnActions: false,
      },
      {
        accessorKey: 'type',
        header: 'Tipo',
        size: 50,
        enableColumnActions: false,
        enableSorting: false,
        Cell: ({ row }) => {
          const type = row.original.type;
          return type == 0 ? 'Câncer' :
            type == 1 ? 'Pré-Transplante' :
              type == 2 ? 'Pós-Transplante' :
                type == 3 ? 'Outro' : '???';
        },
      },
    ],
    [],
  );

  const fetchTreatments = async () => {
    const treatments = await getTreatments(filters);
    setTreatments(treatments);
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

    value = columnFilters.find(cf => cf.id === 'type')?.value;
    if (columnIds.includes('type') && typeof value === 'string') {
      if ('câncer'.includes(value.toLowerCase())) updatedFilters.type = iTypeTreatment.cancer;
      else if ('pré-transplante'.includes(value.toLowerCase())) updatedFilters.type = iTypeTreatment.pretransplant
      else if ('pós-transplante'.includes(value.toLowerCase())) updatedFilters.type = iTypeTreatment.posttransplant
      else if ('outro'.includes(value.toLowerCase())) updatedFilters.type = iTypeTreatment.other
    } else updatedFilters.type = undefined;

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
    LocalStorage.setColumnVisibilityTreatment(columnVisibleState);
  }, [columnVisibleState]);

  useEffect(() => {
    const page: iPage = { page: pagination.pageIndex, pageSize: pagination.pageSize };
    setFilters({ ...filters, page: page });

    LocalStorage.setPageSize(page.pageSize!);

    const fetchTreatments = async () => setTreatments(await getTreatments({ ...filters, page: page }));
    fetchTreatments();
  }, [pagination]);

  const renderTopToolbar = (table: MRT_TableInstance<iTreatment>) => (
    <Box display='flex' flexDirection='column' gap={2} paddingRight='auto'>
      <Typography variant={smDown ? "h6" : "h5"} display='flex' alignItems='center' gap={1} paddingRight='auto' >
        <VaccinesIcon fontSize={smDown ? "medium" : "large"} color='primary' />
        Tratamentos
      </Typography>
      <Box display='flex' gap={2}>
        <Button variant='contained' size='small' startIcon={<AddIcon />} onClick={() => navigate('/tratamentos/gerenciar/cadastrar')}>
          Novo
        </Button>
        <Box display='flex' alignItems='center'>
        </Box>
        <Button variant='contained' size='small' startIcon={<ContentPasteSearchIcon />} onClick={fetchTreatments}>
          Buscar
        </Button>
      </Box>
    </Box>
  );

  const renderActions = (row: MRT_Row<iTreatment>) => (
    <Box display='flex' flexDirection='row' flexWrap='nowrap'>
      <IconButton onClick={() => navigate(`/tratamentos/gerenciar/${row.original.id}`)}>
        <EditRoundedIcon />
      </IconButton>
    </Box>
  );

  const table = useMaterialReactTable<iTreatment>({
    columns,
    data: treatments,
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