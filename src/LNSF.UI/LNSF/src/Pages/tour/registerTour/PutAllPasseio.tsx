import { Box, Divider, Grid, LinearProgress, Toolbar, Typography, useMediaQuery, useTheme } from "@mui/material";
import { useNavigate, useParams } from "react-router-dom"
import DescriptionRoundedIcon from '@mui/icons-material/DescriptionRounded';
import { ButtonAction } from "../../../Component";
import { useContext, useEffect, useState, } from "react";
import { TourContext, iAttObject, iTourObject } from "../../../Contexts";
import { Form } from "@unform/web";
import { IFormErrorsCustom, TextFieldCustom, useCustomForm } from "../../../Component/forms";
import * as yup from 'yup';


const formValidateSchema: yup.Schema<iAttObject> = yup.object().shape({
    output: yup.date().required('Campo de data é obrigatório'),
    input: yup.date().required('Campo de data é obrigatório'),
    note: yup.string().required().min(1),
    peopleId: yup.number().required().min(1),

})


export const PutAllPasseio: React.FC = () => {

    const { id = 'false' } = useParams<'id'>();
    const theme = useTheme();
    const smDown = useMediaQuery(theme.breakpoints.down('sm'));
    const navigate = useNavigate();
    const [isLoadind, setIsLoading] = useState(false);
    const { viewTour, updateAllTour } = useContext(TourContext);
    const [modify, setModify] = useState(false);

    const { formRef, save } = useCustomForm();

    useEffect(() => {

        if (id !== 'false') {
            setIsLoading(true);

            viewTour(1, id, 'id')
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
    }, [id, modify])


    const handSave = (dados: iAttObject) => {

        formValidateSchema.
            validate(dados, { abortEarly: false })
            .then((dadosValidados) => {
                setIsLoading(true)

                if (id !== 'false') {

                    const data: iTourObject = {

                        id: Number(id),
                        output: dadosValidados.output,
                        input: dadosValidados.input,
                        note: dadosValidados.note,
                        peopleId: dadosValidados.peopleId,
                    }

                    updateAllTour(data)
                        .then((response) => {
                            if (response instanceof Error) {
                                setIsLoading(false);
                            } else {
                                setModify(!modify)
                                setIsLoading(false);
                                navigate(`/inicio/registrodiario/visualizar`)
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
                        {!smDown && (<DescriptionRoundedIcon color='primary' sx={{ fontSize: '2.7rem', paddingRight: '10px' }} />)}
                        Registro de entrada e saída
                    </Typography>

                    < ButtonAction
                        // mostrarBotaoNovo={id !== 'cadastrar'}
                        mostrarBotaoNovo={false}
                        mostrarBotaoApagar={false}
                        mostrarBotaoSalvar={true}
                        mostrarBotaoSalvarEFechar={false}
                        aoClicarEmSalvar={save}

                        //aoClicarEmNovo={() => { navigate('/inicio/apartamentos/gerenciar/cadastrar') }}
                        aoClicarEmVoltar={() => { navigate('/inicio/registrodiario/visualizar') }}
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
                                <Typography variant="h5" >
                                    Editar
                                    <Divider />
                                </Typography>
                            </Grid>
                            <Grid container item direction='row' spacing={2}>
                                <Grid item xs={4}>
                                    <TextFieldCustom
                                        fullWidth
                                        type="datetime-local"
                                        label="Horário de Saída"
                                        name="output"
                                        disabled={isLoadind}
                                    />
                                </Grid>
                                <Grid item xs={4}>
                                    <TextFieldCustom
                                        fullWidth
                                        type="datetime-local"
                                        label="Horário de Chegada"
                                        name="input"
                                        disabled={isLoadind}
                                    />
                                </Grid>

                                <Grid item xs={4}>
                                    <TextFieldCustom
                                        fullWidth
                                        label="nota"
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