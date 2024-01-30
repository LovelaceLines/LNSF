import { useContext } from 'react'
import { Form } from "@unform/web";
import { PeopleContext, TourContext, iTourRegister, iTourRegisterYupSchema } from '../../../Contexts';
import { Box, Grid, Toolbar, Typography, useMediaQuery, useTheme } from '@mui/material';
import { AutoCompletePeople, ButtonAction } from '../../../Component';
import DescriptionRoundedIcon from '@mui/icons-material/DescriptionRounded';
import { IFormErrorsCustom, TextFieldCustom, useCustomForm } from '../../../Component/forms';
import * as yup from 'yup';
import { toast } from 'react-toastify';
import { useNavigate } from 'react-router-dom';


const formValidateSchema: yup.Schema = yup.object().shape({
    id: yup.number().required().min(1),
    note: yup.string().required().min(1),
})

export const RegisterTour: React.FC = () => {

    const smDown = useMediaQuery(useTheme().breakpoints.down('sm'));
    const { postTour } = useContext(TourContext);
    const { formRef, save } = useCustomForm();
    const navigate = useNavigate();

    const registerTour = async (data: iTourRegisterYupSchema) => {
        try {
            await formValidateSchema.validate(data, { abortEarly: false });

            const idata: iTourRegister = {
                peopleId: data.id,
                note: data.note
            };

            const createdTuor = await postTour(idata);
            if (createdTuor) {
                toast.success('Saída cadastrada!');
                navigate('/registrodiario/visualizar')
            }
        } catch (error) {
            if (error instanceof yup.ValidationError) {
                const ValidationError: IFormErrorsCustom = {};

                error.inner.forEach((error) => {
                    if (!error.path) return;
                    ValidationError[error.path] = error.message;
                });

                console.log(error.errors);
                formRef.current?.setErrors(ValidationError);
            } else {
                console.error(error);
            }
        }
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
                        {!smDown && (<DescriptionRoundedIcon color='primary' sx={{ fontSize: '2.7rem', paddingRight: '10px' }} />)}
                        Criar um registro de saída
                    </Typography>

                    < ButtonAction
                        mostrarBotaoSalvar
                        mostrarBotaoVoltar

                        aoClicarEmSalvar={save}
                        aoClicarEmVoltar={() => { navigate(`/registrodiario/visualizar`) }}
                    />
                </Toolbar>

            </Box>
            <Box >
                <Form ref={formRef} onSubmit={(dados) => registerTour(dados)}>
                    <Box display='flex' justifyContent='center'>
                        <Grid container padding={2} spacing={2}>
                            <Grid item xs={6} >
                                <AutoCompletePeople />
                            </Grid>

                            <Grid item xs={6}>
                                <TextFieldCustom
                                    fullWidth
                                    label="Observação"
                                    name="note"
                                />
                            </Grid>
                        </Grid>
                    </Box>
                </Form>
            </Box>
        </Box >
    )
}

// export const RegisterTour: React.FC = () => {

//     const { viewTourOutput, registerTour, updateTour } = useContext(TourContext);
//     const { viewPeople } = useContext(PeopleContext);
//     const { viewRoom } = useContext(RoomContext);
//     const [dataOutput_, setDataOutput_] = useState<iTourPeopleRoom[]>([]);
//     const [isLoadind, setIsLoading] = useState(true);
//     const theme = useTheme();
//     const smDown = useMediaQuery(theme.breakpoints.down('sm'));
//     const { formRef, save } = useCustomForm();
//     const [modify, setModify] = useState(false);

//     const BuscarPessoa = async (id: number) => {
//         try {
//             const response = await viewPeople(1, String(id), 'id');

//             if (response instanceof Error) {
//                 return '';
//             } else {
//                 return response[0];
//             }
//         } catch (error) {
//             console.error('Detalhes do erro:', error);
//             return '';
//         }
//     };


//     const BuscarQuarto = async (id: number) => {
//         try {
//             const response = await viewRoom(1, String(id), 'id');
//             if (response instanceof Error) {
//                 return 0;
//             } else {
//                 return String(response[0].number);
//             }
//         } catch (error) {
//             console.error('Detalhes do erro:', error);
//             return 0;
//         }
//     };


//     useEffect(() => {
//         const fetchData = async () => {
//             setIsLoading(true);

//             try {
//                 const response = await viewTourOutput(true);

//                 if (response instanceof Error) {
//                     setIsLoading(false);
//                 } else {
//                     const updatedData = await Promise.all(
//                         response.map(async row => {
//                             const namePromise = BuscarPessoa(row.peopleId);
//                             const name = await namePromise.then((result) => { if (result) { return result.name } })
//                             const idRoom = await namePromise.then((result) => { if (result) { return result.roomId } })

//                             if (typeof idRoom === 'number' && typeof name === 'string') {
//                                 const roomPromise = BuscarQuarto(idRoom);
//                                 const room = await roomPromise;

//                                 return {
//                                     id: row.id,
//                                     output: row.output,
//                                     input: row.input,
//                                     note: row.note,
//                                     peopleId: row.peopleId,
//                                     name: name,
//                                     number: String(room),
//                                 };
//                             } else {
//                                 return {
//                                     id: row.id,
//                                     output: row.output,
//                                     input: row.input,
//                                     note: row.note,
//                                     peopleId: row.peopleId,
//                                     name: name === undefined ? '' : name,
//                                     number: '',
//                                 };
//                             }
//                         })
//                     );

//                     setDataOutput_(updatedData);
//                     setIsLoading(false);
//                 }
//             } catch (error) {
//                 setIsLoading(false);
//                 console.error('Detalhes do erro:', error);
//             }
//         };

//         fetchData();
//     }, [modify]);


//     const handSave = (dados: iTourRegister) => {

//         formValidateSchema.
//             validate(dados, { abortEarly: false })
//             .then((dadosValidados) => {
//                 setIsLoading(true)

//                 const data: iTourRegister = {
//                     peopleId: dadosValidados.id,
//                     note: dadosValidados.note
//                 }

//                 registerTour(data)
//                     .then((response) => {
//                         if (response instanceof Error) {
//                             setIsLoading(false);
//                         } else {
//                             setModify(!modify)
//                             setIsLoading(false);
//                         }
//                     })
//                     .catch((error) => {
//                         setIsLoading(false);
//                         console.error('Detalhes do erro:', error);
//                     });
//             })
//             .catch((errors: yup.ValidationError) => {
//                 const ValidationError: IFormErrorsCustom = {}

//                 errors.inner.forEach(error => {
//                     if (!error.path) return;
//                     ValidationError[error.path] = error.message;
//                 });
//                 console.log(errors.errors);
//                 formRef.current?.setErrors(ValidationError)
//             })
//     };

//     const confirmReturn = (dados: iTourUpdate) => {
//         updateTour(dados)
//             .then((response) => {
//                 if (response instanceof Error) {
//                     setIsLoading(false);
//                 } else {
//                     setModify(!modify)
//                     setIsLoading(false);
//                 }
//             })
//             .catch((error) => {
//                 setIsLoading(false);
//                 console.error('Detalhes do erro:', error);
//             });
//     }

//     return (

//         <Box
//             display='flex'
//             flexDirection='column'
//             width='100%'
//         >
//             <Box>
//                 <Toolbar sx={{ margin: 0 }}>
//                     <Typography
//                         variant={smDown ? "h5" : "h4"}
//                         noWrap
//                         component="div"
//                         sx={{ flexGrow: 1, display: 'flex', alignItems: 'flex-end' }}
//                     >
//                         {!smDown && (<DescriptionRoundedIcon color='primary' sx={{ fontSize: '2.7rem', paddingRight: '10px' }} />)}
//                         Registro de saídas
//                     </Typography>
//                 </Toolbar>
//             </Box>

//             <Box
//                 display='flex'
//                 flexDirection={smDown ? 'column' : 'row'}
//                 width='100%'
//                 gap={smDown ? 5 : 0}
//             >
//                 <Box
//                     width={smDown ? '100%' : '40%'}
//                     component={Paper}
//                     bgcolor={theme.palette.background.default}
//                     margin={smDown ? 0 : 1}>

//                     <Typography variant="h6" padding={1} >
//                         Criar uma anotação
//                         <Divider />
//                     </Typography>

//                     <Form ref={formRef} onSubmit={(dados) => handSave(dados)}>
//                         <Box display='flex' flexDirection='column' justifyContent='center'>
//                             <Grid container direction='column' padding={2} spacing={2}>
//                                 <Grid item  >
//                                     <AutoCompletePeople />
//                                 </Grid>

//                                 <Grid item>
//                                     <TextFieldCustom
//                                         fullWidth
//                                         label="Observação"
//                                         name="note"
//                                         disabled={isLoadind}
//                                         multiline
//                                         rows={10}
//                                     />
//                                 </Grid>
//                             </Grid>

//                             <Box
//                                 display='flex'
//                                 justifyContent='center'>
//                                 < ButtonAction
//                                     mostrarBotaoNovo={false}
//                                     mostrarBotaoSalvar={true}
//                                     mostrarBotaoVoltar={false}
//                                     mostrarBotaoApagar={false}
//                                     aoClicarEmSalvar={save}
//                                 />
//                             </Box>
//                         </Box>
//                     </Form>
//                 </Box>

//                 <Box
//                     width='100%'
//                     component={Paper}
//                     bgcolor={theme.palette.background.default}
//                     margin={smDown ? 0 : 1}
//                     height={488}
//                     overflow='scroll'
//                 >

//                     <Typography variant="h6" padding={1} >
//                         Confirmar retorno
//                         <Divider />
//                     </Typography>
//                     <Box padding={2} >
//                         <TableContainer component={Paper} variant='outlined'>

//                             <Table>
//                                 <TableBody >
//                                     {dataOutput_.map(row => (
//                                         <TableRow key={row.id}>
//                                             <TableCell sx={{ textAlign: 'left' }}>
//                                                 <Box display='flex'
//                                                     flexDirection='row'
//                                                 >
//                                                     <Box
//                                                         width='90%'
//                                                     >
//                                                         <Box display='flex'
//                                                             flexDirection='row'
//                                                             justifyContent='space-between'
//                                                             gap={2}
//                                                         >
//                                                             {row.number !== '' && (<span > <strong>Apt:</strong> {row.number}</span>)}
//                                                             <Box>
//                                                                 <strong>Saída:  </strong>  {format(parseISO(String(row.output)), 'HH:mm')} - {format(parseISO(String(row.output)), 'dd/MM/yyyy')}
//                                                             </Box>

//                                                         </Box>
//                                                         <Box>
//                                                             <p>
//                                                                 <strong>Nome:</strong> {row.name}
//                                                             </p>
//                                                             <p>
//                                                                 <strong>Obs:</strong> {row.note}
//                                                             </p>
//                                                         </Box>
//                                                     </Box>
//                                                     <Box
//                                                         display='flex'
//                                                         justifyContent='center'
//                                                         alignItems='center'
//                                                         width='10%'
//                                                     >
//                                                         <Button
//                                                             size='small'
//                                                             color='primary'
//                                                             disableElevation
//                                                             variant='outlined'
//                                                             onClick={() => {
//                                                                 const data = {
//                                                                     id: row.id,
//                                                                     note: row.note,
//                                                                     peopleId: row.peopleId,
//                                                                 }
//                                                                 confirmReturn(data)
//                                                             }}>
//                                                             <CheckRoundedIcon color='primary' fontSize='medium' />
//                                                         </Button>
//                                                     </Box>
//                                                 </Box>
//                                             </TableCell>
//                                         </TableRow>
//                                     ))}
//                                 </TableBody>

//                                 <TableFooter>
//                                     {isLoadind && (
//                                         <TableRow>
//                                             <TableCell colSpan={5}>
//                                                 <LinearProgress />
//                                             </TableCell>
//                                         </TableRow>
//                                     )}
//                                 </TableFooter>
//                             </Table>
//                         </TableContainer>
//                     </Box>
//                 </Box>
//             </Box>
//         </Box>
//     )
// }