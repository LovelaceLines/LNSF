import { Box, Divider, Grid, LinearProgress, Toolbar, Typography, useMediaQuery, useTheme } from "@mui/material";
import { useNavigate, useParams } from "react-router-dom"
import DomainIcon from '@mui/icons-material/Domain';
import { ButtonAction } from "../../../Component";
import { useContext, useEffect, useState, } from "react";
import { iRoomRegister } from "../../../Contexts";
import { Form } from "@unform/web";
import { IFormErrorsCustom, TextFieldCustom, useCustomForm } from "../../../Component/forms";
import * as yup from 'yup';
import { iHospital, iHospitalObject } from "../../../Contexts/hospitalContext/type";
import { HospitalContext } from "../../../Contexts/hospitalContext";

const formValidateSchema: yup.Schema<iHospital> = yup.object().shape({
    name: yup.string().required(),
    acronym: yup.string().required(),
})

export const TelaDeGerenciamentoHospital: React.FC = () => {

    const { id = 'cadastrar' } = useParams<'id'>();
    const theme = useTheme();
    const smDown = useMediaQuery(theme.breakpoints.down('sm'));
    const navigate = useNavigate();
    const [isLoadind, setIsLoading] = useState(false);
    const { viewHospital, registerHospital, updateHospital } = useContext(HospitalContext);

    const { formRef, save, isSaveAndClose, saveAndClose } = useCustomForm();

    useEffect(() => {

        if (id !== 'cadastrar') {
            setIsLoading(true);

            viewHospital(1, id, 'id')
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


    const handSave = (dados: iRoomRegister) => {

        formValidateSchema.
            validate(dados, { abortEarly: false })
            .then((dadosValidados) => {
                setIsLoading(true)

                if (id === 'cadastrar') {

                    const data: iHospital = {
                        name: dadosValidados.name,
                        acronym: dadosValidados.acronym
                        
                    }

                    registerHospital(data)
                        .then((response) => {
                            if (response instanceof Error) {
                                setIsLoading(false);
                            } else {
                                setIsLoading(false);
                                if (isSaveAndClose()) {
                                    navigate('/inicio/hospital/visualizar')
                                } else {
                                    navigate(`/inicio/hospital/gerenciar/${response.id}`)
                                }
                            }
                        })
                        .catch((error) => {
                            setIsLoading(false);
                            console.error('Detalhes do erro:', error);
                        });
                } else {
                    
                    const data: iHospitalObject = {
                        id: Number(id),
                        name: dadosValidados.name,
                        acronym: dadosValidados.acronym
                        
                    }

                    updateHospital(data)
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
                <Toolbar sx={{ flexGrow: 1, display: 'flex', flexDirection: smDown ? 'column' : 'row', alignItems: smDown ? 'left' : 'flex-end' }}>
                    <Typography
                        variant={smDown ? "h5" : "h4"}
                        noWrap
                        component="div"
                        sx={{ flexGrow: 1, display: 'flex', alignItems: 'flex-end' }}
                    >
                        {!smDown && (<DomainIcon color='primary' sx={{ fontSize: '2.7rem', paddingRight: '10px' }} />)}
                        Hospital
                    </Typography>

                    < ButtonAction
                        mostrarBotaoNovo={false}
                        mostrarBotaoApagar={false}
                        mostrarBotaoSalvar={id === 'cadastrar' ? false : true}
                        mostrarBotaoSalvarEFechar={id !== 'cadastrar' ? false : true}
                        aoClicarEmSalvar={id !== 'cadastrar' ? save : undefined}
                        aoClicarEmSalvarEFechar={id === 'cadastrar' ? saveAndClose : undefined}
                        aoClicarEmVoltar={() => { navigate('/inicio/hospital/visualizar') }}
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
                                    {(id !== 'cadastrar') ? 'Editar este hospital' : 'Cadastrar um novo hospital'}
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
                                <Grid item xs={6} >

                                    <TextFieldCustom
                                        fullWidth
                                        label="Sigla"
                                        name="acronym"
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