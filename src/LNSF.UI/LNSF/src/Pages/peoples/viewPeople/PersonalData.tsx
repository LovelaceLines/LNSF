import { Box, Button, Divider, Grid, Icon, LinearProgress, Toolbar, Typography, useMediaQuery, useTheme } from "@mui/material";
import { useNavigate, useParams } from "react-router-dom"
import PersonIcon from '@mui/icons-material/Person';
import { useContext, useEffect, useState, } from "react";
import { EmergencyContactContext, PeopleContext, iEmergencyContactObject } from "../../../Contexts";
import { ButtonAction } from "../../../Component";
import { format, isValid, parseISO } from "date-fns";


export const PersonalData: React.FC = () => {

    const { id = 'cadastrar' } = useParams<'id'>();
    const theme = useTheme();
    const smDown = useMediaQuery(theme.breakpoints.down('sm'));
    const navigate = useNavigate();
    const [isLoadind, setIsLoading] = useState(false);
    const { viewPeople, people, setPeople } = useContext(PeopleContext);
    const { viewEmergencyContact, emergencyContact, setEmergencyContact, deleteEmergencyContact } = useContext(EmergencyContactContext);
    const [peopleId, setPeopleId] = useState<number>();
    const [modify, setModify] = useState(false);

    useEffect(() => {

        setIsLoading(true);
        viewPeople(1, id, 'id')
            .then((response) => {
                if (response instanceof Error) {
                    setIsLoading(false);
                } else {
                    setPeopleId(response[0].id)
                    setPeople(response[0])
                    setIsLoading(false);
                }
            })
            .catch((error) => {
                setIsLoading(false);
                console.error('Detalhes do erro:', error);
            });
    }, [id])

    useEffect(() => {
        if (peopleId) {
            setIsLoading(true);
            viewEmergencyContact(1, String(peopleId), 'peopleId')
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
    }, [peopleId, modify]);

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
                        mostrarBotaoNovo={false}
                        mostrarBotaoApagar={false}
                        mostrarBotaoSalvar={false}
                        mostrarBotaoSalvarEFechar={false}

                        aoClicarEmVoltar={() => { navigate('/inicio/pessoas/visualizar') }}
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
                            onClick={() => navigate(`/inicio/pessoas/gerenciar/${people.id}`)}
                            startIcon={<Icon>edit</Icon>}
                        >
                            <Typography variant='button' whiteSpace="nowrap" textOverflow="ellipsis" overflow="hidden">
                                Editar
                            </Typography>
                        </Button>
                    </Box>

                </Box>

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
                                onClick={() => navigate(`/inicio/pessoas/dados/contatoEmergencia/${emergencyContact !== undefined ? emergencyContact.id : 'cadastrar=' + people.id}`)}
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
                                }
                                }
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