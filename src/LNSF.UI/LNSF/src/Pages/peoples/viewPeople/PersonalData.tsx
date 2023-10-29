import { Box, Divider, Grid, Toolbar, Typography, useMediaQuery, useTheme } from "@mui/material";
import { useNavigate, useParams } from "react-router-dom"
import PersonIcon from '@mui/icons-material/Person';
import { useContext, useEffect, useState, } from "react";
import { PeopleContext } from "../../../Contexts";
import { ButtonAction } from "../../../Component";
import { format, isValid, parseISO } from "date-fns";





export const PersonalData: React.FC = () => {

    const { id = 'cadastrar' } = useParams<'id'>();
    const theme = useTheme();
    const smDown = useMediaQuery(theme.breakpoints.down('sm'));
    const navigate = useNavigate();
    const [isLoadind, setIsLoading] = useState(false);
    const { viewPeople, people, setPeople } = useContext(PeopleContext);

    useEffect(() => {

        setIsLoading(true);

        viewPeople(1, id, 'id')
            .then((response) => {
                if (response instanceof Error) {
                    setIsLoading(false);
                } else {
                    setPeople(response[0])
                    setIsLoading(false);
                }
            })
            .catch((error) => {
                setIsLoading(false);
                console.error('Detalhes do erro:', error);
            });

    }, [id])



    let formattedDate = '';

    if (isValid(parseISO(String(people.birthDate)))) {
        formattedDate = format(parseISO(String(people.birthDate)), 'dd/MM/yyyy');
    } else {
        formattedDate = '-';
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

                <Box sx={{ border: '1px solid #EEEEEE', padding: '10px' }}>
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
                        <Grid item xs={8}>
                            <Typography
                                color={'gray'}
                            >
                                Observação
                            </Typography>
                            {people.note}
                        </Grid>
                        <Grid item xs={4}>
                            <Typography
                                color={'gray'}
                            >
                                Contato de emergência
                            </Typography>
                           
                        </Grid>
                    </Grid>

                </Box>
                <Box sx={{ border: '1px solid #EEEEEE', padding: '10px' }}
                  
                >
                    <Typography
                        variant="subtitle2"
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
            </Box>



        </Box>
    )
}