import { BottomNavigation, BottomNavigationAction, Box, Button, Divider, Grid, Icon, LinearProgress, Toolbar, Typography, useMediaQuery, useTheme } from "@mui/material";
import { useNavigate, useParams } from "react-router-dom"
import PersonIcon from '@mui/icons-material/Person';
import { useContext, useEffect, useState, } from "react";
import { EmergencyContactContext, PeopleContext, iEmergencyContactObject, iPeopleFilter } from "../../../Contexts";
import { ButtonAction } from "../../../Component";
import { format, isValid, parseISO } from "date-fns";
import { Stethoscope } from "@phosphor-icons/react";
import HailIcon from '@mui/icons-material/Hail';
import { EscortContext } from "../../../Contexts/escortContext";
import { iEscortObject } from "../../../Contexts/escortContext/type";
import { Form } from "@unform/web";
import { iPatientObject } from "../../../Contexts/patientContext/type";
import * as yup from 'yup';
import { AutoCompleteHospital } from "../../../Component/autoCompletes/AutoCompleteHospital";
import { PatientContext } from "../../../Contexts/patientContext";
import { AutoCompleteTreatament } from "../../../Component/autoCompletes/AutoCompleteTreatament";
import { useCustomForm } from "../../../Component/forms";
import { HospitalContext } from "../../../Contexts/hospitalContext";
import { TreatmentContext } from "../../../Contexts/treatmentContext";
import { iHospitalFilter, iHospitalObject } from "../../../Contexts/hospitalContext/type";


const formValidateSchema: yup.Schema = yup.object().shape({

})

export const PersonalData: React.FC = () => {

    const { id = 'cadastrar' } = useParams<'id'>();
    const theme = useTheme();
    const smDown = useMediaQuery(theme.breakpoints.down('sm'));
    const navigate = useNavigate();
    const [isLoadind, setIsLoading] = useState(false);
    const { getPeoples, getPeopleById, people, setPeople } = useContext(PeopleContext);
    const { registerEscort, viewEscort } = useContext(EscortContext);
    const { getTreatmentById } = useContext(TreatmentContext);
    const { getHospitals } = useContext(HospitalContext);
    const { viewPatient, Patient, setPatient, updatePatient, registerPatient } = useContext(PatientContext);
    const { viewEmergencyContact, emergencyContact, setEmergencyContact, deleteEmergencyContact } = useContext(EmergencyContactContext);
    const [isEscort, setIsEscort] = useState(false)
    const [isPatient, setIsPatient] = useState(false)
    const [activeForm, setActiveForm] = useState(false)
    const [activeFormUpdate, setActiveFormUpdate] = useState(false)
    const [modify, setModify] = useState(false);
    const [modify1, setModify1] = useState(false);
    const [modify2, setModify2] = useState(false);
    const { formRef, save } = useCustomForm();
    const [hospital, setHospital] = useState<iHospitalObject>();


    const fetchPeoples = async () => {
        console.log("Peoples: ", id)
        const filtro: iPeopleFilter = {
            id: Number(id)
        }
        const Peoples = await getPeoples(filtro);
        setPeople(Peoples[0])
    };


    useEffect(() => {

        if (id !== 'cadastrar') {
            setIsLoading(true);
            fetchPeoples()
            setIsLoading(false);
        }
    }, [id])




    useEffect(() => {

        setIsLoading(true);
        viewPatient(1, String(people.id), 'PeopleId')
            .then((response) => {
                console.log("buscar : ", response)
                if (response instanceof Error) {
                    setIsLoading(false);
                    setIsPatient(false)
                } else if (response.length > 0) {
                    setIsPatient(true)
                    setActiveForm(false)
                    setIsEscort(false)
                    setPatient(response[0])
                    console.log("patiente: ", response[0])
                    // const filtro: iHospitalFilter = {
                    //     id: Number(response[0].hospitalId)
                    // }

                    // getHospitals(filtro)
                    //     .then((responsee) => {
                    //         if (responsee instanceof Error) {
                    //             setIsLoading(false);
                    //         } else {
                    //             setHospital(responsee[0])
                    //             setIsLoading(false);
                    //             console.log("aaaaaaaaaaaaaa", responsee[0])
                    //         }
                    //     })
                    //     .catch((error) => {
                    //         setIsLoading(false);
                    //         console.error('Detalhes do erro:', error);
                    //     });


                    // setTreatment([])
                    // response[0].treatmentIds.map((item) => {

                    //     getTreatmentById(item.)
                    //         .then((responsee) => {
                    //             if (responsee instanceof Error) {
                    //                 setIsLoading(false);
                    //             } else {

                    //                 setTreatment(prevTreatment => [...prevTreatment, ...responsee]);
                    //                 setIsLoading(false);
                    //             }
                    //         })
                    //         .catch((error) => {
                    //             setIsLoading(false);
                    //             console.error('Detalhes do erro:', error);
                    //         });
                    // });

                } else {
                    setHospital({} as iHospitalObject)
                    setIsPatient(false)
                }

                setIsLoading(false);
            })
            .catch((error) => {
                setIsLoading(false);
                console.error('Detalhes do erro:', error);
            });

    }, [people.id, modify1])


    useEffect(() => {

        if (people.id) {
            setIsLoading(true);
            viewEscort(1, String(people.id), 'PeopleId')
                .then((response) => {
                    if (response instanceof Error) {
                        setIsLoading(false);
                        setIsEscort(false)
                    } else if (response.length > 0) {
                        setIsEscort(true)
                        setIsPatient(false)
                    } else {
                        setIsEscort(false)
                    }

                    setIsLoading(false);
                })
                .catch((error) => {
                    setIsLoading(false);
                    console.error('Detalhes do erro:', error);
                });
        }
    }, [people.id, modify2])


    useEffect(() => {

        if (people.id) {
            setIsLoading(true);
            viewEmergencyContact(1, String(people.id), 'peopleId')
                .then((response) => {
                    if (response instanceof Error) {
                        setIsLoading(false);
                    } else {
                        setEmergencyContact(response[0]);
                        setIsLoading(false);
                    }
                })
                .catch((error) => {
                    setIsLoading(false);
                    console.error('Detalhes do erro:', error);
                });
        }
    }, [people.id, modify]);

    let formattedDate = '';

    if (isValid(parseISO(String(people.birthDate)))) {
        formattedDate = format(parseISO(String(people.birthDate)), 'dd/MM/yyyy');
    } else {
        formattedDate = '-';
    }

    const deleteContato = (id_: number) => {

        if (confirm('Realmente deseja remover esse contato?')) {
            const data: iEmergencyContactObject = {
                id: id_
            }
            setIsLoading(true);
            deleteEmergencyContact(data)
                .then((response) => {
                    if (response instanceof Error) {
                        setIsLoading(false);
                    } else {
                        setModify(!modify)
                        setIsLoading(false);
                    }
                })
                .catch((error) => {
                    setIsLoading(false);
                    console.error('Detalhes do erro:', error);
                });
        }
    }

    const handSave = (dados: iPatientObject) => {
        formValidateSchema.
            validate(dados, { abortEarly: false })
            .then((dadosValidados) => {
                setIsLoading(true)

                if (activeFormUpdate === false) {
                    const data = {
                        peopleId: people.id,
                        hospitalId: dadosValidados.id,
                        socioeconomicRecord: true,
                        term: true,
                        treatmentIds: [dadosValidados.id_]
                    }

                    registerPatient(data)
                        .then((response) => {
                            if (response instanceof Error) {
                                setIsLoading(false);
                            } else {
                                setIsPatient(true)
                                setActiveForm(false)
                                setModify1(!modify1)
                                setIsLoading(false);
                            }
                        })
                        .catch((error) => {
                            setIsLoading(false);
                            console.error('Detalhes do erro:', error);
                        });
                    setIsLoading(false);
                }
                else {
                    const data = {
                        id: Patient.id,
                        peopleId: Patient.peopleId,
                        hospitalId: dadosValidados.id,
                        socioeconomicRecord: true,
                        term: true,
                        treatmentIds: [dadosValidados.id_]
                    }

                    updatePatient(data)
                        .then((response) => {
                            if (response instanceof Error) {
                                setIsLoading(false);
                            } else {
                                setIsPatient(true)
                                setActiveForm(false)
                                setModify1(!modify1)
                                setIsLoading(false);
                            }
                        })
                        .catch((error) => {
                            setIsLoading(false);
                            console.error('Detalhes do erro:', error);
                        });
                    setIsLoading(false);
                }
            })
    }

    // const removePatient = (id_: number) => {

    //     const data: iEscortObject = {   // editar tudo isso
    //         peopleId: id_
    //     }
    //     setIsLoading(true);
    //     registerEscort(data)
    //         .then((response) => {
    //             if (response instanceof Error) {
    //                 setIsLoading(false);
    //             } else {
    //                 setModify2(!modify2)
    //                 setIsLoading(false);
    //             }
    //         })
    //         .catch((error) => {
    //             setIsLoading(false);
    //             console.error('Detalhes do erro:', error);
    //         });

    // }


    const addEscort = (id_: number) => {

        const data: iEscortObject = {
            peopleId: id_
        }
        setIsLoading(true);
        registerEscort(data)
            .then((response) => {
                if (response instanceof Error) {
                    setIsLoading(false);
                } else {
                    setModify2(!modify2)
                    setIsLoading(false);
                }
            })
            .catch((error) => {
                setIsLoading(false);
                console.error('Detalhes do erro:', error);
            });
    }

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
                        Perfil
                    </Typography>
                    < ButtonAction
                        mostrarBotaoVoltar

                        aoClicarEmVoltar={() => { navigate('/pessoas/visualizar') }}
                    />
                </Toolbar>
            </Box>
            <Divider />
            <Box
                style={{
                    display: 'flex',
                    flexDirection: 'column',
                    gap: '20px',
                    marginTop: '15px'
                }}
            >
                <Box
                    display='flex'
                    sx={{ display: 'flex', flexDirection: 'column', border: '1px solid #EEEEEE', padding: '10px', justifyContent: 'space-between' }}
                >
                    <Typography
                        variant="h6"
                    >
                        Esta pessoa é: {(isPatient && 'Paciente') || (isEscort && 'Acompanhante')}
                    </Typography>
                    {(!isPatient && !isEscort) && <BottomNavigation
                        sx={{ width: '100%', display: 'flex', justifyContent: 'space-around' }}
                        showLabels
                    //value={value}

                    >

                        {(!isPatient && !isEscort) ? (
                            <BottomNavigationAction
                                sx={{
                                    border: isPatient ? '0.1px solid #03a9f4' : '0.1px solid #bdbdbd',
                                    borderRadius: '5px',
                                    color: isPatient ? "#03a9f4" : '#9e9e9e',

                                    "&:hover": {
                                        backgroundColor: '#82b1f',
                                        border: '0.1px solid #2196f3',
                                    }
                                }}
                                onClick={() => {
                                    if (!isPatient) {
                                        setActiveForm(!activeForm);
                                    }
                                }}
                                label="Paciente"
                                icon={<Stethoscope size={24} color={isPatient ? "#03a9f4" : "#9e9e9e"} />} />
                        ) : null}

                        {(!isPatient && !isEscort) ? (
                            <BottomNavigationAction
                                sx={{
                                    border: '0.1px solid #bdbdbd',
                                    borderRadius: '5px',
                                    "&:hover": {
                                        backgroundColor: '#82b1f',
                                        border: '0.1px solid #2196f3',
                                    },
                                    color: isEscort ? "#03a9f4" : '#9e9e9e'
                                }}
                                onClick={() => addEscort(people.id)}
                                label="Acompanhante"
                                icon={<HailIcon sx={{ color: isEscort ? "#03a9f4" : '#9e9e9e' }} />}
                            />
                        ) : null}

                        {/* {isPatient ? (
                            <BottomNavigationAction
                                sx={{
                                    border: '0.1px solid #bdbdbd',
                                    borderRadius: '5px',
                                    color: "#03a9f4",

                                    "&:hover": {
                                        backgroundColor: '#82b1f',
                                        border: '0.1px solid #2196f3',
                                    }
                                }}
                                onClick={() => {
                                    // removePatient()
                                }}
                                label="Remover Paciente"
                                icon={<Stethoscope size={24} color={isPatient ? "#03a9f4" : "#9e9e9e"} />} />
                        ) : null} */}

                    </BottomNavigation>}

                    {activeForm && <Box
                        display='flex'
                        sx={{ border: '1px solid #EEEEEE', marginTop: '15px', padding: '10px', justifyContent: 'space-between', alignItems: 'center' }}
                    >
                        <Box width={'100%'}>
                            <Form ref={formRef} onSubmit={(dados) => handSave(dados)}>
                                <Grid container item direction='row' spacing={2} >
                                    <Grid item xs={4}>
                                        <AutoCompleteHospital />
                                    </Grid>
                                    <Grid item xs={4}>
                                        <AutoCompleteTreatament />
                                    </Grid>
                                </Grid>
                            </Form>
                        </Box>

                        < ButtonAction

                            mostrarBotaoSalvar={true}

                            aoClicarEmSalvar={save}

                        />
                    </Box>}

                </Box>

                <Box
                    display='flex'
                    sx={{ border: '1px solid #EEEEEE', padding: '10px', justifyContent: 'space-between' }}>
                    <Box
                        width='100%'
                    >
                        {isLoadind && <LinearProgress />}
                        <Typography
                            variant="subtitle2"
                            marginBottom={3}
                        >
                            Informações pessoais
                        </Typography>

                        <Grid container spacing={3}>
                            <Grid item md={4} xs={6}>
                                <Typography
                                    color={'gray'}
                                >
                                    Nome
                                </Typography>
                                {people.name}
                            </Grid>

                            <Grid item md={4} xs={6}>
                                <Typography
                                    color={'gray'}
                                >
                                    RG
                                </Typography>
                                {people.rg}
                            </Grid>

                            <Grid item md={4} xs={6}>
                                <Typography
                                    color={'gray'}
                                >
                                    CPF
                                </Typography>
                                {people.cpf}
                            </Grid>

                            <Grid item md={4} xs={6}>
                                <Typography
                                    color={'gray'}
                                >
                                    Data de Aniversário
                                </Typography>
                                {formattedDate}
                            </Grid>

                            <Grid item md={4} xs={6}>
                                <Typography
                                    color={'gray'}
                                >
                                    Gênero
                                </Typography>
                                {people.gender === 0 ? 'Masculino' : 'Femenino'}
                            </Grid>

                            <Grid item md={4} xs={6}>
                                <Typography
                                    color={'gray'}
                                >
                                    Telefone
                                </Typography>
                                {people.phone}
                            </Grid>
                            <Grid item xs={12}>
                                <Typography
                                    color={'gray'}
                                >
                                    Observação
                                </Typography>
                                {people.note}
                            </Grid>

                        </Grid>

                        <Typography
                            variant="subtitle2"
                            marginTop={3}
                            marginBottom={3}
                        >
                            Endereço
                        </Typography>

                        <Grid container spacing={3} >
                            <Grid item md={4} xs={6}>
                                <Typography
                                    color={'gray'}
                                >
                                    Estado
                                </Typography>
                                {people.state}

                            </Grid>
                            <Grid item md={4} xs={6}>
                                <Typography
                                    color={'gray'}
                                >
                                    Cidade
                                </Typography>
                                {people.city}
                            </Grid>

                            <Grid item md={4} xs={6}>
                                <Typography
                                    color={'gray'}
                                >
                                    Rua
                                </Typography>
                                {people.street}
                            </Grid>

                            <Grid item md={4} xs={6}>
                                <Typography
                                    color={'gray'}
                                >
                                    Bairro
                                </Typography>
                                {people.neighborhood}
                            </Grid>

                            <Grid item md={4} xs={6}>
                                <Typography
                                    color={'gray'}
                                >
                                    N°
                                </Typography>
                                {people.houseNumber}
                            </Grid>
                        </Grid>
                    </Box>
                    <Box>
                        <Button
                            size='small'
                            color='primary'
                            disableElevation
                            variant='outlined'
                            onClick={() => navigate(`/pessoas/gerenciar/${people.id}`)}
                            startIcon={<Icon>edit</Icon>}
                        >
                            <Typography variant='button' whiteSpace="nowrap" textOverflow="ellipsis" overflow="hidden">
                                Editar
                            </Typography>
                        </Button>
                    </Box>

                </Box>

                {isPatient && <Box
                    display='flex'
                    sx={{ border: '1px solid #EEEEEE', padding: '10px', justifyContent: 'space-between' }}>
                    <Box
                        width='100%'
                    >
                        {isLoadind && <LinearProgress />}
                        <Typography
                            variant="subtitle2"
                            marginBottom={3}
                        >
                            Dados Hospitalares
                        </Typography>

                        <Grid container spacing={3}>
                            <Grid item md={4} xs={6}>
                                <Typography
                                    color={'gray'}
                                >
                                    Hospital
                                </Typography>

                                {hospital?.name !== undefined ? hospital.name : '-'}
                            </Grid>

                            {/* {treatment.map((treatmentItem) => (

                                <Grid item md={4} xs={6} key={treatmentItem.id}>
                                    <Typography
                                        color={'gray'}
                                    >
                                        Nome
                                    </Typography>
                                    {treatmentItem.name}
                                    <Typography
                                        color={'gray'}
                                    >
                                        Tipo
                                    </Typography>
                                    {treatmentItem.type}
                                </Grid>


                            ))}
 */}

                        </Grid>
                    </Box>
                    <Box>
                        <Button
                            size='small'
                            color='primary'
                            disableElevation
                            variant='outlined'
                            onClick={() => {
                                setActiveForm(true)
                                setActiveFormUpdate(true)
                            }}
                            startIcon={<Icon>edit</Icon>}
                        >
                            <Typography variant='button' whiteSpace="nowrap" textOverflow="ellipsis" overflow="hidden">
                                Editar
                            </Typography>
                        </Button>
                    </Box>
                </Box>}

                <Box
                    display='flex'
                    sx={{ border: '1px solid #EEEEEE', marginBottom: '15px', padding: '10px', justifyContent: 'space-between' }}>
                    <Box
                        width='100%'
                    >
                        <Typography
                            variant="subtitle2"
                            marginBottom={3}
                        >
                            Contato de emergência
                        </Typography>

                        <Grid container spacing={3} >
                            <Grid item xs={6}>
                                <Typography
                                    color={'gray'}
                                >
                                    Nome
                                </Typography>
                                {emergencyContact !== undefined ? emergencyContact.name : '-'}
                            </Grid>
                            <Grid item xs={6}>
                                <Typography
                                    color={'gray'}
                                >
                                    Telefone
                                </Typography>
                                {emergencyContact !== undefined ? emergencyContact.phone : '-'}

                            </Grid>
                        </Grid>
                    </Box>
                    <Box
                        display='flex'
                        flexDirection={smDown ? 'column' : 'row'}
                        alignContent='end'
                        justifyContent='end'
                        gap={1}
                    >
                        <Box>
                            <Button
                                size='small'
                                color='primary'
                                disableElevation
                                variant='outlined'
                                onClick={() => navigate(`/pessoas/dados/contatoEmergencia/${emergencyContact !== undefined ? emergencyContact.id : 'cadastrar=' + people.id}`)}
                                startIcon={<Icon>edit</Icon>}
                            >
                                <Typography variant='button' whiteSpace="nowrap" textOverflow="ellipsis" overflow="hidden">
                                    {emergencyContact !== undefined ? 'Editar' : 'Cadastrar'}
                                </Typography>
                            </Button>
                        </Box>
                        {emergencyContact !== undefined && <Box>
                            <Button
                                size='small'
                                color='primary'
                                disableElevation
                                variant='outlined'
                                onClick={() => {
                                    if (emergencyContact && emergencyContact.id) {
                                        deleteContato(emergencyContact.id);
                                    }
                                }}
                                startIcon={<Icon>delete</Icon>}
                            >
                                <Typography variant='button' whiteSpace="nowrap" textOverflow="ellipsis" overflow="hidden">
                                    Deletar
                                </Typography>
                            </Button>
                        </Box>
                        }
                    </Box>
                </Box>
            </Box>
        </Box>
    )
}