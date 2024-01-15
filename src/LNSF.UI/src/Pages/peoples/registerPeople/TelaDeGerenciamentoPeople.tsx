import { Box, Divider, Grid, LinearProgress, Toolbar, Typography, useMediaQuery, useTheme } from "@mui/material";
import { useNavigate, useParams } from "react-router-dom"
import PersonIcon from '@mui/icons-material/Person';
import { ButtonAction } from "../../../Component";
import { useContext, useEffect, useState, } from "react";
import { PeopleContext, iPeopleObject, iPeopleRegister, iPeopleUpdate } from "../../../Contexts";
import { Form } from "@unform/web";
import { IFormErrorsCustom, TextFieldCustom, useCustomForm } from "../../../Component/forms";
import * as yup from 'yup';
import { TextSelectCustom } from "../../../Component/forms/TextSelectCustom";

const estadosBrasil = [
    { id: 'Acre', nome: 'AC' },
    { id: 'Alagoas', nome: 'AL' },
    { id: 'Amapá', nome: 'AP' },
    { id: 'Amazonas', nome: 'AM' },
    { id: 'Bahia', nome: 'BA' },
    { id: 'Ceará', nome: 'CE' },
    { id: 'Distrito Federal', nome: 'DF' },
    { id: 'Espírito Santo', nome: 'ES' },
    { id: 'Goiás', nome: 'GO' },
    { id: 'Maranhão', nome: 'MA' },
    { id: 'Mato Grosso', nome: 'MT' },
    { id: 'Mato Grosso do Sul', nome: 'MS' },
    { id: 'Minas Gerais', nome: 'MG' },
    { id: 'Pará', nome: 'PA' },
    { id: 'Paraíba', nome: 'PB' },
    { id: 'Paraná', nome: 'PR' },
    { id: 'Pernambuco', nome: 'PE' },
    { id: 'Piauí', nome: 'PI' },
    { id: 'Rio de Janeiro', nome: 'RJ' },
    { id: 'Rio Grande do Norte', nome: 'RN' },
    { id: 'Rio Grande do Sul', nome: 'RS' },
    { id: 'Rondônia', nome: 'RO' },
    { id: 'Roraima', nome: 'RR' },
    { id: 'Santa Catarina', nome: 'SC' },
    { id: 'São Paulo', nome: 'SP' },
    { id: 'Sergipe', nome: 'SE' },
    { id: 'Tocantins', nome: 'TO' },
];

const formValidateSchema: yup.Schema = yup.object().shape({
    name: yup.string().required().min(1),
    gender: yup.number().required(),
    birthDate: yup.date().required(),
    rg: yup
        .string()
        .required('RG é obrigatório'),
    cpf: yup
        .string()
        .required('CPF é obrigatório')
        .matches(
            /^\d{3}\.\d{3}\.\d{3}-\d{2}$/,
            'CPF inválido. Use o formato 000.000.000-00'
        ),
    street: yup.string().required().min(1),
    houseNumber: yup.string().required().min(1),
    neighborhood: yup.string().required().min(1),
    city: yup.string().required(),
    state: yup.string().required(),
    phone: yup
        .string()
        .required('Número de telefone é obrigatório')
        .matches(
            /^\(\d{2}\) \d \d{4}-\d{4}$/,
            'Número de telefone inválido. Use o formato (99) 9 9999-9999'
        ),
    note: yup.string().required().min(1),

})

export const TelaDeGerenciamentoPeople: React.FC = () => {

    const { id = 'cadastrar' } = useParams<'id'>();
    const theme = useTheme();
    const smDown = useMediaQuery(theme.breakpoints.down('sm'));
    const navigate = useNavigate();
    const [isLoadind, setIsLoading] = useState(false);
    const { viewPeople, registerPeople, updatePeople } = useContext(PeopleContext);


    const { formRef, save, isSaveAndClose, saveAndClose } = useCustomForm();

    useEffect(() => {

        if (id !== 'cadastrar') {
            setIsLoading(true);

            viewPeople(1, id, 'id')
                .then((response) => {
                    if (response instanceof Error) {
                        setIsLoading(false);
                    } else {
                        formRef.current?.setData(response[0]);
                        setIsLoading(false);
                    }
                })
                .catch((error) => {
                    setIsLoading(false);
                    console.error('Detalhes do erro:', error);
                });
        }
    }, [id])


    const handSave = (dados: iPeopleObject) => {

        formValidateSchema.
            validate(dados, { abortEarly: false })
            .then((dadosValidados) => {
                setIsLoading(true)

                if (id === 'cadastrar') {

                    const data: iPeopleRegister = {
                        name: dadosValidados.name,
                        gender: Number(dadosValidados.gender),
                        birthDate: dadosValidados.birthDate,
                        rg: dadosValidados.rg,
                        cpf: dadosValidados.cpf,
                        street: dadosValidados.street,
                        houseNumber: dadosValidados.houseNumber,
                        neighborhood: dadosValidados.neighborhood,
                        city: dadosValidados.city,
                        state: dadosValidados.state,
                        phone: dadosValidados.phone,
                        note: dadosValidados.note
                    }

                    registerPeople(data)
                        .then((response) => {
                            if (response instanceof Error) {
                                setIsLoading(false);
                            } else {
                                setIsLoading(false);
                                if (isSaveAndClose()) {
                                    navigate('/inicio/pessoas/visualizar')
                                } else {
                                    navigate(`/inicio/pessoas/visualizar/${response.id}`)
                                }
                            }
                        })
                        .catch((error) => {
                            setIsLoading(false);
                            console.error('Detalhes do erro:', error);
                        });

                } else {

                    const data: iPeopleUpdate = {
                        id: Number(id),
                        name: dadosValidados.name,
                        gender: Number(dadosValidados.gender),
                        birthDate: dadosValidados.birthDate,
                        rg: dadosValidados.rg,
                        cpf: dadosValidados.cpf,
                        street: dadosValidados.street,
                        houseNumber: dadosValidados.houseNumber,
                        neighborhood: dadosValidados.neighborhood,
                        city: dadosValidados.city,
                        state: dadosValidados.state,
                        phone: dadosValidados.phone,
                        note: dadosValidados.note,
                    }
                    console.log("data ", data)
                    updatePeople(data)
                        .then((response) => {
                            if (response instanceof Error) {
                                setIsLoading(false);
                            } else {
                                setIsLoading(false);
                            }
                        })
                        .catch((error) => {
                            setIsLoading(false);
                            console.error('Detalhes do erro:', error);
                        });
                }
            })
            .catch((errors: yup.ValidationError) => {
                const ValidationError: IFormErrorsCustom = {}

                errors.inner.forEach(error => {
                    if (!error.path) return;
                    ValidationError[error.path] = error.message;
                });
                console.log(errors.errors);
                formRef.current?.setErrors(ValidationError)
            })
    };

    return (
        <Box
            display='flex'
            flexDirection='column'
            width='100%'

        >
            <Box>
                <Toolbar >
                    <Typography
                        variant={smDown ? "h5" : "h4"}
                        noWrap
                        component="div"
                        sx={{ flexGrow: 1, display: 'flex', alignItems: 'flex-end' }}
                    >
                        {!smDown && (<PersonIcon color='primary' sx={{ fontSize: '2.7rem', paddingRight: '10px' }} />)}
                        Pessoas
                    </Typography>

                    < ButtonAction
                        // mostrarBotaoNovo={id !== 'cadastrar'}
                        mostrarBotaoNovo={false}
                        mostrarBotaoApagar={false}
                        mostrarBotaoSalvar={id === 'cadastrar' ? false : true}
                        mostrarBotaoSalvarEFechar={id !== 'cadastrar' ? false : true}
                        aoClicarEmSalvar={id !== 'cadastrar' ? save : undefined}
                        aoClicarEmSalvarEFechar={id === 'cadastrar' ? saveAndClose : undefined}
                        //aoClicarEmNovo={() => { navigate('/inicio/apartamentos/gerenciar/cadastrar') }}
                        aoClicarEmVoltar={() => { navigate(`/inicio/pessoas/visualizar`) }}
                    />
                </Toolbar>

            </Box>
            <Divider />
            <Box style={{ maxHeight: '350px', overflowY: 'auto' }}>
                <Form ref={formRef} onSubmit={(dados) => handSave(dados)}>
                    <Box margin={1} display='flex' flexDirection='column' >
                        <Grid container direction='column' padding={2} spacing={2}>
                            {isLoadind && (
                                <Grid item>
                                    <LinearProgress variant="indeterminate" />
                                </Grid>
                            )
                            }
                            <Grid item>
                                <Typography variant={smDown ? "h6" : "h5"} >
                                    {(id !== 'cadastrar') ? 'Editar esta pessoa' : 'Cadastrar uma nova pessoa'}
                                    <Divider />
                                </Typography>
                            </Grid>
                            <Grid container item direction='row' spacing={2}>
                                <Grid item xs={6}>
                                    <TextFieldCustom
                                        fullWidth
                                        label="Nome"
                                        name="name"
                                        disabled={isLoadind}
                                    />
                                </Grid>
                                <Grid item xs={3}>
                                    <TextSelectCustom
                                        fullWidth
                                        name="gender"
                                        menu={[
                                            {
                                                nome: 'Masculino',
                                                id: '0'
                                            },
                                            {
                                                nome: 'Femenino',
                                                id: '1'
                                            }
                                        ]}
                                        disabled={isLoadind}
                                        label="Gênero"

                                    />
                                </Grid>
                                <Grid item xs={3} >

                                    <TextFieldCustom
                                        fullWidth
                                        type="datetime-local"
                                        label="Data de aniversário"
                                        name="birthDate"
                                        disabled={isLoadind}
                                        

                                    />
                                </Grid>
                            </Grid>


                            <Grid container item direction='row' spacing={2}>

                                <Grid item xs={4}>
                                    <TextFieldCustom
                                        fullWidth
                                        label="RG"
                                        name="rg"
                                        disabled={isLoadind}
                                    />
                                </Grid>
                                <Grid item xs={4}>
                                    <TextFieldCustom
                                        fullWidth
                                        label="CPF"
                                        name="cpf"
                                        disabled={isLoadind}
                                    />
                                </Grid>
                                <Grid item xs={4}>
                                    <TextFieldCustom
                                        fullWidth
                                        label="Telefone"
                                        name="phone"
                                        disabled={isLoadind}
                                    />
                                </Grid>
                            </Grid>

                            <Grid container item direction='row' spacing={2}>

                                <Grid item xs={2}>

                                    <TextSelectCustom
                                        fullWidth
                                        name="state"
                                        menu={estadosBrasil}
                                        disabled={isLoadind}
                                        label="Estado"

                                    />
                                </Grid>
                                <Grid item xs={3}>
                                    <TextFieldCustom
                                        fullWidth
                                        label="Cidade"
                                        name="city"
                                        disabled={isLoadind}
                                    />
                                </Grid>
                                <Grid item xs={3}>
                                    <TextFieldCustom
                                        fullWidth
                                        label="Rua"
                                        name="street"
                                        disabled={isLoadind}
                                    />
                                </Grid>
                                <Grid item xs={2}>
                                    <TextFieldCustom
                                        fullWidth
                                        label="Bairro"
                                        name="neighborhood"
                                        disabled={isLoadind}
                                    />
                                </Grid>
                                <Grid item xs={2}>
                                    <TextFieldCustom
                                        fullWidth
                                        label="N°"
                                        name="houseNumber"
                                        disabled={isLoadind}
                                    />
                                </Grid>

                                <Grid item xs={12}>
                                    <TextFieldCustom
                                        fullWidth
                                        label="Observação"
                                        name="note"
                                        disabled={isLoadind}
                                    />
                                </Grid>

                            </Grid>
                        </Grid>
                    </Box>
                </Form>
            </Box>



        </Box>
    )
}