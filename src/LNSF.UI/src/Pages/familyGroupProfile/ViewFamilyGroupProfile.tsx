import { Box, Button, IconButton, Typography, useMediaQuery, useTheme } from "@mui/material";
import { useContext, useEffect, useMemo, useState } from "react";
import { useNavigate } from "react-router-dom";
import { FamilyGroupProfileContext } from "../../Contexts/familyGroupProfileContext";
import { MRT_ColumnDef, MRT_ColumnFiltersState, MRT_PaginationState, MRT_Row, MRT_SortingState, MRT_TableInstance, MRT_VisibilityState, MaterialReactTable, useMaterialReactTable } from "material-react-table";
import { LocalStorage } from "../../Global";
import { iFamilyGroupProfileObject, iFamilyGroupProfileFilter } from "../../Contexts/familyGroupProfileContext/type";
import { iOrderBy, iPage } from "../../Contexts/types";
import { MRT_Localization_PT_BR } from "material-react-table/locales/pt-BR";
import InfoOutlinedIcon from '@mui/icons-material/InfoOutlined';
import EditRoundedIcon from '@mui/icons-material/EditRounded';
import AddIcon from '@mui/icons-material/Add';
import ContentPasteSearchIcon from '@mui/icons-material/ContentPasteSearch';
import GroupsIcon from '@mui/icons-material/Groups';

export const ViewFamilyGroupProfile: React.FC = () => {
  const navigate = useNavigate();
  const theme = useTheme();
  const smDown = useMediaQuery(theme.breakpoints.down('sm'));
  const { getFamilyGroupProfiles, getCountFamilyGroupProfile } = useContext(FamilyGroupProfileContext);
  const [count, setCount] = useState<number>();
  const [globalFilter, setGlobalFilter] = useState<string>('');
  const [columnFilters, setColumnFilters] = useState<MRT_ColumnFiltersState>([]);
  const [sortFilters, setSortFilters] = useState<MRT_SortingState>([{ id: 'id', desc: false }]);
  const [columnVisibleState, setColumnVisibleState] = useState<MRT_VisibilityState>(LocalStorage.getColumnVisibilityFamilyGroupProfile());
  const [pagination, setPagination] = useState<MRT_PaginationState>({ pageIndex: 0, pageSize: LocalStorage.getPageSize() });
  const [isFullScreen, setIsFullScreen] = useState<boolean>(false);
  const [profiles, setProfiles] = useState<iFamilyGroupProfileObject[]>([]);
  const [filters, setFilters] = useState<iFamilyGroupProfileFilter>({
    getPatient: true,
    page: { page: pagination.pageIndex, pageSize: pagination.pageSize },
  });

  const columns = useMemo<MRT_ColumnDef<iFamilyGroupProfileObject>[]>(
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
        accessorKey: 'name',
        header: 'Nome',
        size: 150,
        enableColumnActions: false,
      },
      {
        accessorKey: 'kinship',
        header: 'Parentesco',
        size: 100,
        enableColumnActions: false,
        enableSorting: false,
      },
      {
        accessorKey: 'age',
        header: 'Idade',
        size: 50,
        enableColumnActions: false,
        enableSorting: false,
      },
      {
        accessorKey: 'profession',
        header: 'Profissão',
        size: 100,
        enableColumnActions: false,
        enableSorting: false,
      },
      {
        accessorKey: 'income',
        header: 'Renda',
        size: 100,
        enableColumnActions: false,
        enableSorting: false,
        Cell: ({ row }) => {
          const income = row.original.income;
          return !income ? '' : income.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' });
        }
      },
    ],
    [],
  );

  const fetchProfiles = async () => {
    const profiles = await getFamilyGroupProfiles(filters);
    setProfiles(profiles);
  };

  const fetchCount = async () => {
    const count = await getCountFamilyGroupProfile();
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

    value = columnFilters.find(cf => cf.id === 'kinship')?.value;
    if (columnIds.includes('kinship') && typeof value === 'string')
      updatedFilters.kinship = value;
    else updatedFilters.kinship = undefined;

    value = columnFilters.find(cf => cf.id === 'age')?.value;
    if (columnIds.includes('age') && typeof value === 'number')
      updatedFilters.age = value;
    else updatedFilters.age = undefined;

    value = columnFilters.find(cf => cf.id === 'profession')?.value;
    if (columnIds.includes('profession') && typeof value === 'string')
      updatedFilters.profession = value;
    else updatedFilters.profession = undefined;

    value = columnFilters.find(cf => cf.id === 'income')?.value;
    if (columnIds.includes('income') && typeof value === 'number')
      updatedFilters.income = value;
    else updatedFilters.income = undefined;

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
    LocalStorage.setColumnVisibilityFamilyGroupProfile(columnVisibleState);
  }, [columnVisibleState]);

  useEffect(() => {
    const page: iPage = { page: pagination.pageIndex, pageSize: pagination.pageSize };
    setFilters({ ...filters, page: page });

    LocalStorage.setPageSize(page.pageSize!);

    const fetchProfiles = async () => setProfiles(await getFamilyGroupProfiles({ ...filters, page: page }));
    fetchProfiles();
  }, [pagination]);

  const renderTopToolbar = (table: MRT_TableInstance<iFamilyGroupProfileObject>) => (
    <Box display='flex' flexDirection='column' gap={2} paddingRight='auto'>
      <Typography variant={smDown ? "h6" : "h5"} display='flex' alignItems='center' gap={1} paddingRight='auto' >
        <GroupsIcon fontSize={smDown ? "medium" : "large"} color='primary' />
        Perfil do Grupo Familiar
      </Typography>
      <Box display='flex' gap={2}>
        <Button variant='contained' size='small' startIcon={<AddIcon />} onClick={() => navigate('/pessoas/gerenciar/cadastrar')}>
          Novo
        </Button>
        <Box display='flex' alignItems='center'>

        </Box>
        <Button variant='contained' size='small' startIcon={<ContentPasteSearchIcon />} onClick={fetchProfiles}>
          Buscar
        </Button>
      </Box>
    </Box>
  );

  const renderActions = (row: MRT_Row<iFamilyGroupProfileObject>) => (
    <Box display='flex' flexDirection='row' flexWrap='nowrap'>
      <IconButton onClick={() => navigate(`/pessoa/editar/${row.original.id}`)}>
        <EditRoundedIcon />
      </IconButton>
      <IconButton onClick={() => navigate(`/pessoas/dados/${row.original.id}`)}>
        <InfoOutlinedIcon />
      </IconButton>
    </Box>
  );

  const table = useMaterialReactTable<iFamilyGroupProfileObject>({
    columns,
    data: profiles,
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