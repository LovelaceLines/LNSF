import { Box, Divider, Grid, LinearProgress, Toolbar, Typography, useMediaQuery, useTheme } from "@mui/material";
import { useNavigate, useParams } from "react-router-dom"
import VaccinesIcon from '@mui/icons-material/Vaccines';
import { ButtonAction } from "../../../Component";
import { useContext, useEffect, useState, } from "react";
import { iRoomRegister } from "../../../Contexts";
import { Form } from "@unform/web";
import { IFormErrorsCustom, TextFieldCustom, useCustomForm } from "../../../Component/forms";
import * as yup from 'yup';
import { iTreatment, iTreatmentObject, iTreatmentPost } from "../../../Contexts/treatmentContext/type";
import { TreatmentContext } from "../../../Contexts/treatmentContext";
import { TextSelectCustom } from "../../../Component/forms/TextSelectCustom";
import { toast } from "react-toastify";

const formValidateSchema: yup.Schema<iTreatmentPost> = yup.object().shape({
    name: yup.string().required(),
    type: yup.number().required(),
})

export const TelaDeGerenciamentoTratamentos: React.FC = () => {

    const { id = 'cadastrar' } = useParams<'id'>();
    const theme = useTheme();
    const smDown = useMediaQuery(theme.breakpoints.down('sm'));
    const navigate = useNavigate();
    const [isLoadind, setIsLoading] = useState(false);
    const { getTreatments, postTreatment, putTreatment } = useContext(TreatmentContext);
    const [treatments, setTreatments] = useState<iTreatment[]>([]);
    const { formRef, save, isSaveAndClose, saveAndClose } = useCustomForm();

    const fetchTreatments = async () => {
        const treatments = await getTreatments();
        setTreatments(treatments);
    };

    const registerTreatments = async (data: iTreatmentPost) => {
        try {
            formValidateSchema
                .validate(data, { abortEarly: false });
            const idata: iTreatmentPost = {
                name: data.name,
                type: data.type,
            };

            const createdTreatments = await postTreatment(idata);
            console.log("created", createdTreatments)
            if (createdTreatments) {
                toast.success('Tratamento cadastrado!');
                navigate('/inicio/tratamentos/visualizar');
            }
        } catch (error) {
            console.error(error);
        }

    };

    useEffect(() => {

        if (id !== 'cadastrar') {
            setIsLoading(true);
            fetchTreatments()
            setIsLoading(false);
        }
    }, [id])

    console.log("/id", id)
    const handSave = (dados: iTreatmentPost) => {

        console.log("dados: ", dados)
        registerTreatments(dados)
        // formValidateSchema.
        //     validate(dados, { abortEarly: false })
        //     .then((dadosValidados) => {
        //         setIsLoading(true)

        //         if (id === 'cadastrar') {

        //             const data: iTreatmentObject = {
        //                 name: dadosValidados.name,
        //                 type: Number(dadosValidados.type)
        //             }

        //             registerTreatment(data)
        //                 .then((response) => {
        //                     if (response instanceof Error) {
        //                         setIsLoading(false);
        //                     } else {
        //                         setIsLoading(false);
        //                         if (isSaveAndClose()) {
        //                             navigate('/inicio/tratamentos/visualizar')
        //                         } else {
        //                             navigate(`/inicio/tratamentos/gerenciar/${response.id}`)
        //                         }
        //                     }
        //                 })
        //                 .catch((error) => {
        //                     setIsLoading(false);
        //                     console.error('Detalhes do erro:', error);
        //                 });
        //         } else {

        //             const data: iTreatmentObject = {
        //                 id: Number(id),
        //                 name: dadosValidados.name,
        //                 type: Number(dadosValidados.type)
        //             }

        //             updateTreatment(data)
        //                 .then((response) => {
        //                     if (response instanceof Error) {
        //                         setIsLoading(false);
        //                     } else {
        //                         setIsLoading(false);
        //                     }
        //                 })
        //                 .catch((error) => {
        //                     setIsLoading(false);
        //                     console.error('Detalhes do erro:', error);
        //                 });
        //         }
        //     })
        //     .catch((errors: yup.ValidationError) => {
        //         const ValidationError: IFormErrorsCustom = {}

        //         errors.inner.forEach(error => {
        //             if (!error.path) return;
        //             ValidationError[error.path] = error.message;
        //         });
        //         console.log(errors.errors);
        //         formRef.current?.setErrors(ValidationError)
        //     })
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
                        {!smDown && (<VaccinesIcon color='primary' sx={{ fontSize: '2.7rem', paddingRight: '10px' }} />)}
                        Tratamento
                    </Typography>

                    < ButtonAction
                        mostrarBotaoNovo={false}
                        mostrarBotaoApagar={false}
                        mostrarBotaoSalvar={id === 'cadastrar' ? false : true}
                        mostrarBotaoSalvarEFechar={id !== 'cadastrar' ? false : true}
                        aoClicarEmSalvar={id !== 'cadastrar' ? save : undefined}
                        aoClicarEmSalvarEFechar={id === 'cadastrar' ? saveAndClose : undefined}
                        aoClicarEmVoltar={() => { navigate('/inicio/tratamentos/visualizar') }}
                    />
                </Toolbar>
            </Box>
            <Divider />
            <Box style={{ maxHeight: '350px', overflowY: 'auto' }}>
                <Form ref={formRef} onSubmit={(dados) => {handSave(dados)
                console.log("dados: ", dados)}
                }>
                    <Box margin={1} display='flex' flexDirection='column' >
                        <Grid container direction='column' padding={2} spacing={2}>
                            {isLoadind && (
                                <Grid item>
                                    <LinearProgress variant="indeterminate" />
                                </Grid>
                            )}
                            <Grid item>
                                <Typography variant={smDown ? "h6" : "h5"} >
                                    {(id !== 'cadastrar') ? 'Editar este tratamento' : 'Cadastrar um novo tratamento'}
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
                                        name="type"
                                        menu={[
                                            {
                                                nome: 'Cancer',
                                                id: '0'
                                            },
                                            {
                                                nome: 'Pré-Transplante',
                                                id: '1'
                                            },
                                            {
                                                nome: 'Pós-Transplante',
                                                id: '2'
                                            },
                                            {
                                                nome: 'Outro',
                                                id: '3'
                                            }
                                        ]}
                                        disabled={isLoadind}
                                        label="Tipo"

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