import { Box, Divider, Grid, InputLabel, LinearProgress, OutlinedInput, Toolbar, Typography, useMediaQuery, useTheme } from "@mui/material";
import { useNavigate, useParams } from "react-router-dom"
import DescriptionRoundedIcon from '@mui/icons-material/DescriptionRounded';
import { ButtonAction } from "../../../Component";
import { useContext, useEffect, useState, } from "react";
import { TourContext, iAttObject, iTourFilter, iTourObject } from "../../../Contexts";
import { Form } from "@unform/web";
import { IFormErrorsCustom, TextFieldCustom, useCustomForm } from "../../../Component/forms";
import * as yup from 'yup';
import { MRT_PaginationState } from "material-react-table";
import { iOrderBy } from "../../../Contexts/types";
import { LocalStorage } from '../../../Global/LocalStorage';

const formValidateSchema: yup.Schema<iAttObject> = yup.object().shape({
    output: yup.date().required('Campo de data é obrigatório'),
    input: yup.date().required('Campo de data é obrigatório'),
    note: yup.string().required().min(1),
    peopleId: yup.number().required().min(1),
})

export const PutAllPasseio: React.FC = () => {

    const { id = 'false' } = useParams<'id'>();
    const navigate = useNavigate();
    const smDown = useMediaQuery(useTheme().breakpoints.down('sm'));
    const { getTours, putAllTour } = useContext(TourContext);
    const [tours, setTours] = useState<iTourObject[]>([]);
    const [globalFilter, setGlobalFilter] = useState<string>('');
    const [pagination, setPagination] = useState<MRT_PaginationState>({ pageIndex: 0, pageSize: LocalStorage.getPageSize() });
    const [inOpenFilter, setInOpenFilter] = useState<boolean>(true);
    const [filters, setFilters] = useState<iTourFilter>({
        page: { page: pagination.pageIndex, pageSize: pagination.pageSize },
        orderBy: iOrderBy.descendent,
        inOpen: inOpenFilter,
        id: Number(id),
        getPeople: true
    });
    const [isLoadind, setIsLoading] = useState(true);
    const { formRef, save } = useCustomForm();

    const [outputDate, setOutputDate] = useState<Date | null>(null);
    const [outputTime, setOutputTime] = useState<Date | null>(null);

    const [inputDate, setInputDate] = useState<Date | null>(null);
    const [inputTime, setInputTime] = useState<Date | null>(null);


    const combineDateAndTime = (date: Date | null, time: Date | null): Date | null => {
        if (!date || !time) return null;
        const combinedDateTime = new Date(date);
        combinedDateTime.setHours(time.getHours(), time.getMinutes(), time.getSeconds());
        return combinedDateTime;
    };

    useEffect(() => {
        setFilters({ ...filters, globalFilter: globalFilter });
    }, [globalFilter]);

    const fetchTours = async () => {
        setIsLoading(true)
        const tours = await getTours(filters);

        const outputDateTime = new Date(tours[0].output);

        setOutputDate(outputDateTime);
        setOutputTime(outputDateTime);
        const inputDateTime = new Date(tours[0].input);
        setInputDate(inputDateTime);
        setInputTime(inputDateTime);

        tours[0].output = outputDateTime.toISOString().split('T')[0]

        formRef.current?.setData(tours[0]);
        setTours(tours);
        setIsLoading(false)
    };

    useEffect(() => {
        fetchTours()
    }, [])


    const handSave = (dados: iAttObject) => {
        console.log("oiiiii")

        // formValidateSchema.
        //     validate(dados, { abortEarly: false })
        //     .then((dadosValidados) => {


        //         if (id !== 'false') {

        //             const data: iTourObject = {
        //                 id: Number(id),
        //                 output: dadosValidados.output,
        //                 input: dadosValidados.input,
        //                 note: dadosValidados.note,
        //                 peopleId: dadosValidados.peopleId,
        //             }


        //             putAllTour(data)
        //                 .then((response) => {
        //                     if (response instanceof Error) {
        //                         toast.error(response.message);
        //                     } else {
        //                         setModify(!modify)

        //                         navigate(`/registrodiario/visualizar`)
        //                     }
        //                 })
        //                 .catch((error) => {

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
                        {!smDown && (<DescriptionRoundedIcon color='primary' sx={{ fontSize: '2.7rem', paddingRight: '10px' }} />)}
                        Registro de entrada e saída
                    </Typography>

                    < ButtonAction
                        mostrarBotaoSalvar
                        mostrarBotaoVoltar

                        aoClicarEmSalvar={save}

                        aoClicarEmVoltar={() => { navigate('/registrodiario/visualizar') }}
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
                                        id="date"
                                        fullWidth
                                        type="date"
                                        label="Horário de Saída"
                                        name="output"
                                        disabled={isLoadind}
                                    />

                                </Grid>
                                <Grid item xs={4}>
                                    <TextFieldCustom
                                        id="date"
                                        fullWidth
                                        type="date"
                                        label="Horário de chegada"
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