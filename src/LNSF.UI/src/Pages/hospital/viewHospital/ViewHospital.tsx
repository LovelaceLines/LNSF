import { useContext, useMemo, useState } from 'react'
import { useEffect } from "react"
import { Box, Button, IconButton, Typography, useMediaQuery, useTheme } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { HospitalContext } from '../../../Contexts/hospitalContext';
import { iHospitalFilter, iHospitalObject } from '../../../Contexts/hospitalContext/type';
import EditRoundedIcon from '@mui/icons-material/EditRounded';
import DomainIcon from '@mui/icons-material/Domain';
import AddIcon from '@mui/icons-material/Add';
import ContentPasteSearchIcon from '@mui/icons-material/ContentPasteSearch';
import { MRT_ColumnDef, MRT_ColumnFiltersState, MRT_PaginationState, MRT_Row, MRT_SortingState, MRT_TableInstance, MRT_VisibilityState, MaterialReactTable, useMaterialReactTable } from 'material-react-table';
import { LocalStorage } from '../../../Global';
import { iOrderBy, iPage } from '../../../Contexts/types';
import { MRT_Localization_PT_BR } from 'material-react-table/locales/pt-BR';

export const ViewHospital: React.FC = () => {
  const navigate = useNavigate();
  const theme = useTheme();
  const smDown = useMediaQuery(theme.breakpoints.down('sm'));
  const { getHospitals, getCount } = useContext(HospitalContext);
  const [count, setCount] = useState<number>();
  const [globalFilter, setGlobalFilter] = useState<string>('');
  const [columnFilters, setColumnFilters] = useState<MRT_ColumnFiltersState>([]);
  const [sortFilters, setSortFilters] = useState<MRT_SortingState>([{ id: "output", desc: true }]);
  const [columnVisibleState, setColumnVisibleState] = useState<MRT_VisibilityState>(LocalStorage.getColumnVisibilityHospital());
  const [pagination, setPagination] = useState<MRT_PaginationState>({ pageIndex: 0, pageSize: LocalStorage.getPageSize() });
  const [isFullScreen, setIsFullScreen] = useState<boolean>(false);
  const [hospitals, setHospitals] = useState<iHospitalObject[]>([]);
  const [filters, setFilters] = useState<iHospitalFilter>({
    page: { page: pagination.pageIndex, pageSize: pagination.pageSize },
  });

  const columns = useMemo<MRT_ColumnDef<iHospitalObject>[]>(
    () => [
      {
        accessorKey: 'name',
        header: 'Nome',
        size: 150,
        enableColumnActions: false,
        enableSorting: false,
      },
      {
        accessorKey: 'acronym',
        header: 'Sigla',
        size: 50,
        enableColumnActions: false,
        enableSorting: false,
      },
    ],
    [],
  );
   
  const fetchHospitals = async () => {
    const hospitals = await getHospitals(filters);
    setHospitals(hospitals);
  };

  const fetchCount = async () => {
    const count = await getCount()
    setCount(count);
  };

  useEffect(() => {
    // fetchHospitals();
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
 
    value = columnFilters.find(cf => cf.id === 'acronym')?.value;
    if (columnIds.includes('acronym') && typeof value === 'string')
      updatedFilters.acronym = value;
    else updatedFilters.acronym = undefined;
 
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
    LocalStorage.setColumnVisibilityHospital(columnVisibleState);
  }, [columnVisibleState]);
 
  useEffect(() => {
    const page: iPage = { page: pagination.pageIndex, pageSize: pagination.pageSize };
    setFilters({ ...filters, page: page });

    LocalStorage.setPageSize(page.pageSize!);
    
    const fetchHospitals = async () => setHospitals(await getHospitals({ ...filters, page: page }));
    fetchHospitals();
  }, [pagination]);
 
  const renderTopToolbar = (table: MRT_TableInstance<iHospitalObject>) => (
    <Box display='flex' flexDirection='column' gap={2} paddingRight='auto'>
      <Typography variant={smDown ? "h6" : "h5"} display='flex' alignItems='center' gap={1} paddingRight='auto' >
        <DomainIcon fontSize={smDown ? "medium" : "large"} color='primary' />
        Histórico de entrada e saída
      </Typography>
      <Box display='flex' gap={2}>
        <Button variant='contained' size='small' startIcon={<AddIcon />} onClick={() => navigate('/inicio/hospital/gerenciar/cadastrar')}>
          Novo
        </Button>
        <Box display='flex' alignItems='center'>
        </Box>
        <Button variant='contained' size='small' startIcon={<ContentPasteSearchIcon />} onClick={fetchHospitals}>
          Buscar
        </Button>
      </Box>
    </Box>
  );
 
  const renderActions = (row: MRT_Row<iHospitalObject>) => (
    <Box display='flex' flexDirection='row' flexWrap='nowrap'>
      <IconButton onClick={() => navigate(`/inicio/hospital/gerenciar/${row.original.id}`)}>
        <EditRoundedIcon />
      </IconButton>
    </Box>
  );
 
  const table = useMaterialReactTable<iHospitalObject>({
    columns,
    data: hospitals,
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