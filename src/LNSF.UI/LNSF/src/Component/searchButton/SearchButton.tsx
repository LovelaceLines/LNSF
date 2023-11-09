import React, { useState } from 'react';
import { TextField, InputAdornment, Box, useMediaQuery, useTheme, Button } from "@mui/material";
import SearchIcon from '@mui/icons-material/Search';
import FilterAltRoundedIcon from '@mui/icons-material/FilterAltRounded';
import FilterAltOffRoundedIcon from '@mui/icons-material/FilterAltOffRounded';

interface ISearchButtonProps {
    textoDaBusca?: string;
    aoMudarTextoDeBusca?: (novoTexto: string) => void;
}


export const SearchButton: React.FC<ISearchButtonProps> = ({
    textoDaBusca,
    aoMudarTextoDeBusca,

}) => {


    const [ativado, setAtivado] = useState(false);
    const theme = useTheme();
    const smDown = useMediaQuery(theme.breakpoints.down('sm'));

    return (
        <Box
            display='flex'
            flexDirection={smDown ? 'column' : 'row'}
            paddingBottom='6px'
        >

            <Button
                onClick={() => {
                    setAtivado(!ativado)
                }}
            >
                <InputAdornment position="start">
                    {!ativado  ? <FilterAltRoundedIcon color='primary' /> : <FilterAltOffRoundedIcon color='primary' />}
                </InputAdornment>
            </Button>



            {
                ativado  && (
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
                )
            }
        </Box >
    );
};

