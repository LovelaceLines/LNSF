import { MenuItem, TextField, TextFieldProps } from "@mui/material"
import { useField } from "@unform/core";
import { useEffect, useState } from "react";

type MenuItem = TextFieldProps & {
    nome: string;
    id: string;
}

type TTextSelectCustomProps = TextFieldProps & {
    name: string;
    menu: MenuItem[];
}


export const TextSelectCustom: React.FC<TTextSelectCustomProps> = ({ name, menu, ...rest }) => {
    const { fieldName, registerField, defaultValue, error, clearError } = useField(name);
    const [value, setValue] = useState(defaultValue || '');

    useEffect(() => {
        registerField({
            name: fieldName,
            getValue: () => value,
            setValue: (_, newValue) => setValue(newValue),
        });
    }, [registerField, fieldName, value])


    return (

        <TextField
            {...rest}
            select
            error={!!error}
            helperText={error}
            onKeyDown={() => error ? clearError() : undefined}

            // value={value}
            // onChange={e => setValue(e.target.value)}
        >
            {menu.map((option) => (
                <MenuItem key={option.id} value={option.id}>
                    {option.nome}
                </MenuItem>
            ))}
        </TextField>

    )
}