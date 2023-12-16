import { useContext, useEffect, useMemo, useState } from 'react';
import { Autocomplete, CircularProgress, TextField } from '@mui/material';

import { useField } from '@unform/core';
import { useDebounce } from '../hooks/UseDebounce';
import { TreatmentContext } from '../../Contexts/treatmentContext';


type TAutoCompleteOption = {
    id_: number;
    label: string;
}

interface IAutoCompleteTreatamentProps {
    isExternalLoading?: boolean;
    

}
export const AutoCompleteTreatament: React.FC<IAutoCompleteTreatamentProps> = ({ isExternalLoading = false }) => {
    const { fieldName, registerField, defaultValue, error, clearError } = useField('id_');
    const { debounce } = useDebounce();

    const [selectedId, setSelectedId] = useState<number | undefined>(defaultValue);

    const [opcoes, setOpcoes] = useState<TAutoCompleteOption[]>([]);
    const [isLoading, setIsLoading] = useState(false);
    const [busca, setBusca] = useState('');
    const { viewTreatment } = useContext(TreatmentContext);

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
            viewTreatment(1, busca, 'name')
                .then((result) => {
                    setIsLoading(false);

                    if (result instanceof Error) {
                        alert(result.message);
                    } else {
                        setOpcoes(
                            result.map(row => (
                                {
                                    id_: row.id,
                                    label: row.name + "(" + (row.type === 0 ? 'Cancer' : row.type === 1 ? 'Pre-Transplante' : row.type === 2 ? 'Pre-Transplante' : 'Outros') + ")" }))
                        );

                    }
                });
        });
    }, [busca]);

    const autoCompleteSelectedOption = useMemo(() => {
        if (!selectedId) return null;

        const selectedOption = opcoes.find(opcao => opcao.id_ === selectedId);
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
            onChange={(_, newValue) => { setSelectedId(newValue?.id_); setBusca(''); clearError(); }}
            popupIcon={(isExternalLoading || isLoading) ? <CircularProgress size={28} /> : undefined}
            renderInput={(params) => (
                <TextField

                    {...params}

                    name="name"
                    label="Escolher Tratamento"
                    error={!!error}
                    helperText={error}
                />
            )
            }
        />
    );
};