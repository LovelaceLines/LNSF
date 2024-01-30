import { Box, Divider, Grid, LinearProgress, Toolbar, Typography, useMediaQuery, useTheme } from "@mui/material";
import { useNavigate, useParams } from "react-router-dom"
import PersonIcon from '@mui/icons-material/Person';
import { ButtonAction } from "../../../Component";
import { useContext, useEffect, useState, } from "react";
import { EmergencyContactContext, iEmergencyContactObject } from "../../../Contexts";
import { Form } from "@unform/web";
import { IFormErrorsCustom, TextFieldCustom, useCustomForm } from "../../../Component/forms";
import * as yup from 'yup';


const formValidateSchema: yup.Schema = yup.object().shape({
    name: yup.string().required().min(1),
    phone: yup
        .string()
        .required('Número de telefone é obrigatório')
        .matches(
            /^\(\d{2}\) \d \d{4}-\d{4}$/,
            'Número de telefone inválido. Use o formato (99) 9 9999-9999'
        ),
})


export const TelaRegisterUpdateContactEmergence: React.FC = () => {

    const { id = 'cadastrar' } = useParams<'id'>();
    const parts = id.split('=');
    const theme = useTheme();
    const smDown = useMediaQuery(theme.breakpoints.down('sm'));
    const navigate = useNavigate();
    const [isLoadind, setIsLoading] = useState(false);
    const { emergencyContact, setEmergencyContact, viewEmergencyContact, registerEmergencyContact, updateEmergencyContact } = useContext(EmergencyContactContext);

    const { formRef, save, isSaveAndClose, saveAndClose } = useCustomForm();

    useEffect(() => {

        if (parts[0] !== 'cadastrar') {
            setIsLoading(true);

            viewEmergencyContact(1, id, 'id')
                .then((response) => {
                    if (response instanceof Error) {
                        setIsLoading(false);
                    } else {
                        setEmergencyContact(response[0])
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


    const handSave = (dados: iEmergencyContactObject) => {

        formValidateSchema.
            validate(dados, { abortEarly: false })
            .then((dadosValidados) => {
                setIsLoading(true)

                if (parts[0] === 'cadastrar') {

                    const data: iEmergencyContactObject = {
                        name: dadosValidados.name,
                        phone: dadosValidados.phone,
                        peopleId: Number(parts[1]),
                    }

                    registerEmergencyContact(data)
                        .then((response) => {
                            if (response instanceof Error) {
                                setIsLoading(false);
                            } else {
                                setIsLoading(false);
                                if (isSaveAndClose()) {
                                    navigate('/pessoas/visualizar')
                                } else {
                                    navigate(`/pessoas/visualizar/${response.id}`)
                                }
                            }
                        })
                        .catch((error) => {
                            setIsLoading(false);
                            console.error('Detalhes do erro:', error);
                        });

                } else {

                    const data: iEmergencyContactObject = {
                        id: emergencyContact.id,
                        name: dadosValidados.name,
                        phone: dadosValidados.phone,
                        peopleId: emergencyContact.peopleId,
                    }

                    console.log("data ", data)

                    updateEmergencyContact(data)
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
                        mostrarBotaoNovo={false}
                        mostrarBotaoApagar={false}
                        mostrarBotaoSalvar={parts[0] === 'cadastrar' ? false : true}
                        mostrarBotaoSalvarEFechar={parts[0] !== 'cadastrar' ? false : true}
                        aoClicarEmSalvar={parts[0] !== 'cadastrar' ? save : undefined}
                        aoClicarEmSalvarEFechar={parts[0] === 'cadastrar' ? saveAndClose : undefined}
                        aoClicarEmVoltar={() => { navigate(`/inicio/pessoas/dados/${parts[0] === 'cadastrar' ? parts[1] : emergencyContact.peopleId}`) }}
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
                            )}
                            <Grid item>
                                <Typography variant={smDown ? "h6" : "h5"} >
                                    {(id !== 'cadastrar') ? 'Editar este contato' : 'Cadastrar um contato'}
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

                                <Grid item xs={6}>
                                    <TextFieldCustom
                                        fullWidth
                                        label="Telefone"
                                        name="phone"
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