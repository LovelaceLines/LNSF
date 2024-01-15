import { useContext, useMemo, useState } from 'react'
import { useEffect } from "react"
import { RoomContext, iRoomFilter, iRoomObject } from '../../../Contexts';
import { Box, Button, Checkbox, IconButton, Typography, useMediaQuery, useTheme } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import CheckCircleOutlineRoundedIcon from '@mui/icons-material/CheckCircleOutlineRounded';
import UnpublishedOutlinedIcon from '@mui/icons-material/UnpublishedOutlined';
import HotelRoundedIcon from '@mui/icons-material/HotelRounded';
import AddIcon from '@mui/icons-material/Add';
import ContentPasteSearchIcon from '@mui/icons-material/ContentPasteSearch';
import EditRoundedIcon from '@mui/icons-material/EditRounded';
import { MRT_ColumnDef, MRT_ColumnFiltersState, MRT_PaginationState, MRT_Row, MRT_SortingState, MRT_TableInstance, MRT_VisibilityState, MaterialReactTable, useMaterialReactTable } from 'material-react-table';
import { LocalStorage } from '../../../Global';
import { iOrderBy, iPage } from '../../../Contexts/types';
import { MRT_Localization_PT_BR } from 'material-react-table/locales/pt-BR';

export const ViewRoom: React.FC = () => {
  const navigate = useNavigate();
  const theme = useTheme();
  const smDown = useMediaQuery(theme.breakpoints.down('sm'));
  const { getRooms, getCount } = useContext(RoomContext);
  const [count, setCount] = useState<number>();
  // const [globalFilter, setGlobalFilter] = useState<string>('');
  const [columnFilters, setColumnFilters] = useState<MRT_ColumnFiltersState>([]);
  const [sortFilters, setSortFilters] = useState<MRT_SortingState>([{ id: 'id', desc: false }]);
  const [columnVisibleState, setColumnVisibleState] = useState<MRT_VisibilityState>(LocalStorage.getColumnVisibilityRoom());
  const [pagination, setPagination] = useState<MRT_PaginationState>({ pageIndex: 0, pageSize: LocalStorage.getPageSize() });
  const [isFullScreen, setIsFullScreen] = useState<boolean>(false);
  const [rooms, setRooms] = useState<iRoomObject[]>([]);
  const [availableFilter, setAvailableFilter] = useState<boolean>(true);
  const [filters, setFilters] = useState<iRoomFilter>({
    page: { page: pagination.pageIndex, pageSize: pagination.pageSize },
    available: availableFilter,
  });

  const columns = useMemo<MRT_ColumnDef<iRoomObject>[]>(
    () => [
      {
        accessorKey: 'number',
        header: 'Número',
        size: 50,
        enableColumnActions: false,
      },
      {
        accessorKey: 'bathroom',
        header: 'Banheiro',
        size: 50,
        enableColumnActions: false,
        enableSorting: false,
        Cell: ({ row }) => {
          const bathroom = row.original.bathroom;
          return bathroom ? 'Individual' : 'Coletivo';
        },
      },
      {
        accessorKey: 'beds',
        header: 'Camas',
        size: 50,
        enableColumnActions: false,
        enableSorting: false,
      },
      {
        accessorKey: 'storey',
        header: 'Andar',
        size: 50,
        enableColumnActions: false,
        enableSorting: false,
      },
      {
        accessorKey: 'available',
        header: 'Disponível',
        size: 150,
        enableColumnActions: false,
        enableSorting: false,
        enableColumnFilter: false,
        Cell: ({ row }) => {
          const available = row.original.available;
          return available ? 
            <CheckCircleOutlineRoundedIcon color='primary' /> : 
            <UnpublishedOutlinedIcon color='error' />;
        },
      },
    ],
    [],
  );

  const fetchRooms = async () => {
    const rooms = await getRooms(filters);
    setRooms(rooms);
  };

  const fetchCount = async () => {
    const count = await getCount();
    setCount(count);
  };

  useEffect(() => {
    fetchCount();
  }, []);

  // useEffect(() => {
  //   setFilters({ ...filters, globalFilter: globalFilter });
  // }, [globalFilter]);
 
  useEffect(() => {
    const updatedFilters = { ...filters };
    const columnIds = columnFilters.map(columnFilter => columnFilter.id);
    let value: unknown;
 
    value = columnFilters.find(cf => cf.id === 'number')?.value;
    if (columnIds.includes('number') && typeof value === 'string')
      updatedFilters.number = value;
    else updatedFilters.number = undefined;
 
    value = columnFilters.find(cf => cf.id === 'bathroom')?.value;
    if (columnIds.includes('bathroom') && typeof value === 'string') {
      if ('individual'.includes(value.toLowerCase())) updatedFilters.bathroom = true;
      else if ('coletivo'.includes(value.toLowerCase())) updatedFilters.bathroom = false;
    }
    else updatedFilters.bathroom = undefined;
 
    value = columnFilters.find(cf => cf.id === 'beds')?.value;
    if (columnIds.includes('beds') && typeof value === 'string')
      updatedFilters.beds = parseInt(value);
    else updatedFilters.beds = undefined;

    value = columnFilters.find(cf => cf.id === 'storey')?.value;
    if (columnIds.includes('storey') && typeof value === 'string')
      updatedFilters.storey = parseInt(value);
    else updatedFilters.storey = undefined;
 
    setFilters(updatedFilters);
  }, [columnFilters]);
 
  useEffect(() => {
    const updatedFilters = { ...filters };
    const columnIds = sortFilters.map(sort => sort.id);
   
    const desc = sortFilters.find(cf => cf.id === 'number')?.desc;
    if (columnIds.includes('number') && typeof desc === 'boolean')
      updatedFilters.orderBy = desc ? iOrderBy.descendent : iOrderBy.ascendent;
    else updatedFilters.orderBy = undefined;
 
    setFilters(updatedFilters);
  }, [sortFilters]);

  useEffect(() => {
    LocalStorage.setColumnVisibilityRoom(columnVisibleState);
  }, [columnVisibleState]);
 
  useEffect(() => {
    const page: iPage = { page: pagination.pageIndex, pageSize: pagination.pageSize };
    setFilters({ ...filters, page: page });

    LocalStorage.setPageSize(page.pageSize!);
    
    const fetchRooms = async () => setRooms(await getRooms({ ...filters, page: page }));
    fetchRooms();
  }, [pagination]);

  useEffect(() => {
    setFilters({ ...filters, available: availableFilter });
  }, [availableFilter]);

  const renderTopToolbar = (table: MRT_TableInstance<iRoomObject>) => (
    <Box display='flex' flexDirection='column' gap={2} paddingRight='auto'>
      <Typography variant={smDown ? "h6" : "h5"} display='flex' alignItems='center' gap={1} paddingRight='auto' >
        <HotelRoundedIcon fontSize={smDown ? "medium" : "large"} color='primary' />
        Quartos
      </Typography>
      <Box display='flex' gap={2}>
        <Button variant='contained' size='small' startIcon={<AddIcon />} onClick={() => navigate('/inicio/apartamentos/gerenciar/cadastrar')}>
          Novo
        </Button>
        <Box display='flex' alignItems='center'>
          <Typography>
            Disponíveis
          </Typography>
          <Checkbox checked={availableFilter} onChange={(e) => setAvailableFilter(e.target.checked)} />
        </Box>
        <Button variant='contained' size='small' startIcon={<ContentPasteSearchIcon />} onClick={fetchRooms}>
          Buscar
        </Button>
      </Box>
    </Box>
  );

  const renderActions = (row: MRT_Row<iRoomObject>) => (
    <Box display='flex' flexDirection='row' flexWrap='nowrap'>
      <IconButton onClick={() => navigate(`/inicio/apartamentos/gerenciar/${row.original.id}`)}>
        <EditRoundedIcon />
      </IconButton>
    </Box>
  );

  const table = useMaterialReactTable<iRoomObject>({
    columns,
    data: rooms,
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
    // onGlobalFilterChange: setGlobalFilter,
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