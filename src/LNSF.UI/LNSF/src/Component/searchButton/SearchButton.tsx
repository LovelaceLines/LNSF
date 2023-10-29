import React, { useState } from 'react';
import { TextField, MenuItem, InputAdornment, Box, useMediaQuery, useTheme } from "@mui/material";
import SearchIcon from '@mui/icons-material/Search';
import FilterAltRoundedIcon from '@mui/icons-material/FilterAltRounded';
import FilterAltOffRoundedIcon from '@mui/icons-material/FilterAltOffRounded';

interface ISearchButtonProps {
    textoDaBusca?: string;
    aoMudarTextoDeBusca?: (novoTexto: string) => void;
    filter: ifilterprops[];
    aoMudarFiltro?: (novoFiltro: string) => void;
}

interface ifilterprops {
    filter: string;
    tipo: string;
}

export const SearchButton: React.FC<ISearchButtonProps> = ({
    textoDaBusca,
    aoMudarTextoDeBusca,
    filter,
    aoMudarFiltro,
}) => {


    const [ativado, setAtivado] = useState('Nenhum');
    const theme = useTheme();
    const smDown = useMediaQuery(theme.breakpoints.down('sm'));

    const handleFiltroChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const novoFiltro = e.target.value;
        setAtivado(novoFiltro);
        aoMudarFiltro?.(novoFiltro);
    };

    return (
        <Box
            display='flex'
            flexDirection={ smDown ? 'column' : 'row'}
            paddingBottom='6px'
        >
            <TextField
                id="filtro"
                select
                label="Filtro"
                variant="standard"
                value={ativado}
                onChange={handleFiltroChange}
                InputProps={{
                    startAdornment: (
                        <InputAdornment position="start">
                            {ativado === 'Nenhum' ? <FilterAltRoundedIcon color='primary'/> : <FilterAltOffRoundedIcon color='primary'/>}
                        </InputAdornment>
                    ),
                }}
            >
                {filter.map((option) => (
                    <MenuItem key={option.filter} value={option.tipo}>
                        {option.tipo}
                    </MenuItem>
                ))}
            </TextField>

            {ativado !== 'Nenhum' && (
                <Box sx={{ display: 'flex', alignItems: 'flex-end' }} 
                >
                    <SearchIcon sx={{ color: 'action.active', mr: 1, my: 0.5 }} />

                    <TextField
                        id="input-with-sx"
                        label="Pesquisar…"
                        variant="standard"
                        value={textoDaBusca}
                        onChange={(e) => aoMudarTextoDeBusca?.(e.target.value)}
                        
                        />
                </Box>
            )}
        </Box>
    );
};

