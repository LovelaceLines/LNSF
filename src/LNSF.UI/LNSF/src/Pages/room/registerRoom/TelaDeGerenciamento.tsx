import { Box, Divider, Grid, LinearProgress, Toolbar, Typography, useMediaQuery, useTheme } from "@mui/material";
import { useNavigate, useParams } from "react-router-dom"
import HotelRoundedIcon from '@mui/icons-material/HotelRounded';
import { ButtonAction } from "../../../Component";
import { useContext, useEffect, useState, } from "react";
import { RoomContext, iRoomObject, iRoomRegister } from "../../../Contexts";
import { Form } from "@unform/web";
import { IFormErrorsCustom, TextFieldCustom, useCustomForm } from "../../../Component/forms";
import * as yup from 'yup';
import { TextSelectCustom } from "../../../Component/forms/TextSelectCustom";



const formValidateSchema: yup.Schema<iRoomRegister> = yup.object().shape({
    number: yup.string().required().min(1),
    bathroom: yup.boolean().required(),
    beds: yup.number().required().min(1),
    occupation: yup.number().required(),
    storey: yup.number().required().min(1),
    available: yup.boolean().required(),
})


export const TelaDeGerenciamentoRoom: React.FC = () => {

    const { id = 'cadastrar' } = useParams<'id'>();
    const theme = useTheme();
    const smDown = useMediaQuery(theme.breakpoints.down('sm'));
    const navigate = useNavigate();
    const [isLoadind, setIsLoading] = useState(false);
    const { viewRoom, registerRoom, updateRoom } = useContext(RoomContext);

    const { formRef, save, isSaveAndClose, saveAndClose } = useCustomForm();

    useEffect(() => {

        if (id !== 'cadastrar') {
            setIsLoading(true);

            viewRoom(1, id, 'id')
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

                    const data: iRoomRegister = {
                        number: dadosValidados.number,
                        bathroom: Boolean(dadosValidados.bathroom),
                        beds: Number(dadosValidados.beds),
                        occupation: Number(dadosValidados.occupation),
                        storey: Number(dadosValidados.storey),
                        available: Boolean(dadosValidados.available)
                    }

                    registerRoom(data)
                        .then((response) => {
                            if (response instanceof Error) {
                                setIsLoading(false);
                            } else {
                                setIsLoading(false);
                                if (isSaveAndClose()) {
                                    navigate('/inicio/apartamentos/gerenciar')
                                } else {
                                    navigate(`/inicio/apartamentos/gerenciar/${response.id}`)
                                }
                            }
                        })
                        .catch((error) => {
                            setIsLoading(false);
                            console.error('Detalhes do erro:', error);
                        });

                } else {

                    const data: iRoomObject = {
                        id: Number(id),
                        number: dadosValidados.number,
                        bathroom: Boolean(dadosValidados.bathroom),
                        beds: Number(dadosValidados.beds),
                        occupation: Number(dadosValidados.occupation),
                        storey: Number(dadosValidados.storey),
                        available: Boolean(dadosValidados.available)
                    }

                    updateRoom(data)
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
                         variant= {smDown ? "h5" : "h4"}
                        noWrap
                        component="div"
                        sx={{ flexGrow: 1, display: 'flex', alignItems: 'flex-end' }}
                    >
                        {!smDown && (<HotelRoundedIcon color='primary' sx={{ fontSize: '2.7rem', paddingRight: '10px' }} />)}
                        Apartamentos
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
                        aoClicarEmVoltar={() => { navigate('/inicio/apartamentos/gerenciar') }}
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
                                <Typography  variant= {smDown ? "h6" : "h5"} >
                                    {(id !== 'cadastrar') ? 'Editar este quarto' : 'Cadastrar um novo quarto'}
                                    <Divider />
                                </Typography>
                            </Grid>
                            <Grid container item direction='row' spacing={2}>
                                <Grid item xs={6}>
                                    <TextFieldCustom
                                        fullWidth
                                        label="Número"
                                        name="number"
                                        disabled={isLoadind}
                                    />
                                </Grid>
                                <Grid item xs={6}>
                                    <TextSelectCustom
                                        fullWidth
                                        name="bathroom"
                                        menu={[
                                            {
                                                nome: 'Coletivo',
                                                id: 'false'
                                            },
                                            {
                                                nome: 'Individual',
                                                id: 'true'
                                            }
                                        ]}
                                        disabled={isLoadind}
                                        label="Banheiro"

                                    />
                                </Grid>
                            </Grid>


                            <Grid container item direction='row' spacing={2}>
                                <Grid item xs={6} >

                                    <TextFieldCustom
                                        fullWidth
                                        label="N° de camas"
                                        name="beds"
                                        disabled={isLoadind}
                                    />
                                </Grid>
                                <Grid item xs={6}>
                                    <TextFieldCustom
                                        fullWidth
                                        label="N° de Ocupação"
                                        name="occupation"
                                        disabled={isLoadind}
                                    />
                                </Grid>
                            </Grid>

                            <Grid container item direction='row' spacing={2}>
                                <Grid item xs={6}>
                                    <TextFieldCustom
                                        fullWidth
                                        label="N° do Andar"
                                        name="storey"
                                        disabled={isLoadind}
                                    />
                                </Grid>
                                <Grid item xs={6}>

                                    <TextSelectCustom
                                        fullWidth
                                        name="available"
                                        menu={[
                                            {
                                                nome: 'Indisponível',
                                                id: 'false'
                                            },
                                            {
                                                nome: 'Disponível',
                                                id: 'true'
                                            }
                                        ]}
                                        disabled={isLoadind}
                                        label="Disponível"
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