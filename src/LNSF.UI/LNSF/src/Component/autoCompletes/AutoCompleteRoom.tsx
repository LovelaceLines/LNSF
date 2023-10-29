import { useContext, useEffect, useMemo, useState } from 'react';
import { Autocomplete, CircularProgress, TextField } from '@mui/material';

import { useField } from '@unform/core';
import { useDebounce } from '../hooks/UseDebounce';
import { RoomContext } from '../../Contexts';


type TAutoCompleteOption = {
    roomId: number;
    label: string;
}

interface IAutoCompleteRoomProps {
    isExternalLoading?: boolean;

}
export const AutoCompleteRoom: React.FC<IAutoCompleteRoomProps> = ({ isExternalLoading = false }) => {
    const { fieldName, registerField, defaultValue, error, clearError } = useField('roomId');
    const { debounce } = useDebounce();

    const [selectedId, setSelectedId] = useState<number | undefined>(defaultValue);

    const [opcoes, setOpcoes] = useState<TAutoCompleteOption[]>([]);
    const [isLoading, setIsLoading] = useState(false);
    const [busca, setBusca] = useState('');
    const { viewRoom } = useContext(RoomContext);

    useEffect(() => {
        registerField({
            name: fieldName,
            getValue: () => selectedId,
            setValue: (_, newSelectedId) => setSelectedId(newSelectedId),
        });
    }, [registerField, fieldName, selectedId]);

    useEffect(() => {
        setIsLoading(true);

        debounce(() => {
            viewRoom(1, busca, ' id', 40)
                .then((result) => {
                    setIsLoading(false);

                    if (result instanceof Error) {
                        // alert(result.message);
                    } else {
                        console.log(result);

                        setOpcoes(
                            result
                                .filter(row => row.available === true)
                                .map(row => ({ roomId: row.id, label: row.number }))
                        );

                    }
                });
        });
    }, [busca]);

    const autoCompleteSelectedOption = useMemo(() => {
        if (!selectedId) return null;

        const selectedOption = opcoes.find(opcao => opcao.roomId === selectedId);
        if (!selectedOption) return null;

        return selectedOption;
    }, [selectedId, opcoes]);


    return (
        <Autocomplete
            openText='Abrir'
            closeText='Fechar'
            noOptionsText='Sem opções'
            loadingText='Carregando...'

            disablePortal

            options={opcoes}
            loading={isLoading}
            disabled={isExternalLoading}
            value={autoCompleteSelectedOption}
            onInputChange={(_, newValue) => setBusca(newValue)}
            onChange={(_, newValue) => { setSelectedId(newValue?.roomId); setBusca(''); clearError(); }}
            popupIcon={(isExternalLoading || isLoading) ? <CircularProgress size={28} /> : undefined}
            renderInput={(params) => (
                <TextField
                  
                    {...params}
                 
                    name="roomId"
                    label="Escolher quartos"
                    error={!!error}
                    helperText={error}
                />
            )
            }
        />
    );
};