import { Box, Divider, Grid, LinearProgress, Toolbar, Typography, useMediaQuery, useTheme } from "@mui/material";
import { useNavigate } from "react-router-dom"
import PersonIcon from '@mui/icons-material/Person';
import { AutoCompleteRoom, ButtonAction } from "../../../Component";
import { useContext, useEffect, useState, } from "react";
import { PeopleContext, iAddPeopleRoom, iAddPeopleRoomAutoComplete } from "../../../Contexts";
import { Form } from "@unform/web";
import { IFormErrorsCustom, useCustomForm } from "../../../Component/forms";
import * as yup from 'yup';



const formValidateSchema: yup.Schema = yup.object().shape({
    roomId: yup.number().required(),
})

interface iTelaAddRemovePeopleRoomProps {
    idPeople: number;
}

export const TelaAddRemovePeopleRoom: React.FC<iTelaAddRemovePeopleRoomProps> = ({ idPeople }) => {

    const theme = useTheme();
    const smDown = useMediaQuery(theme.breakpoints.down('sm'));
    const navigate = useNavigate();
    const [isLoadind, setIsLoading] = useState(false);
    const [nome, setNome] = useState('');
    const { viewPeople, addPeopleRoom } = useContext(PeopleContext);

    const { formRef, save } = useCustomForm();

    useEffect(() => {

        setIsLoading(true);

        viewPeople(1, String(idPeople), 'id')
            .then((response) => {
                if (response instanceof Error) {
                    setIsLoading(false);
                } else {
                    formRef.current?.setData(response[0]);
                    setNome(response[0].name)
                    setIsLoading(false);
                }
            })
            .catch((error) => {
                setIsLoading(false);
                console.error('Detalhes do erro:', error);
            });
    }, [idPeople])


    const handSave = (dados: iAddPeopleRoomAutoComplete) => {

        formValidateSchema.
            validate(dados, { abortEarly: false })
            .then((dadosValidados) => {
                setIsLoading(true)

                const data: iAddPeopleRoom = {
                    peopleId: Number(idPeople),
                    roomId: dadosValidados.roomId,
                }

                addPeopleRoom(data)
                    .then((response) => {
                        if (response instanceof Error) {
                            setIsLoading(false);
                        } else {
                            setIsLoading(false);
                            navigate(`/inicio/pessoas/gerenciar`)
                        }
                    })
                    .catch((error) => {
                        setIsLoading(false);
                        console.error('Detalhes do erro:', error);
                    });
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
                        variant="h4"
                        noWrap
                        component="div"
                        sx={{ flexGrow: 1, display: 'flex', alignItems: 'flex-end' }}
                    >
                        <PersonIcon color='primary' sx={{ fontSize: '2.7rem', paddingRight: '10px' }} />
                        <Typography variant="h6" >
                            {nome}
                            <Divider />
                        </Typography>
                    </Typography>
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
                            <Grid container item direction='row' spacing={2}>
                                <Grid item xs={12}>

                                    <AutoCompleteRoom
                                        isExternalLoading={isLoadind}
                                    />
                                </Grid>
                            </Grid>
                        </Grid>
                    </Box>
                </Form>
            </Box>
            <Box
                
                display='flex'
                alignItems='center'
                justifyContent='center'
            >
                < ButtonAction
                    mostrarBotaoNovo={false}
                    mostrarBotaoApagar={false}

                    mostrarBotaoSalvarEFechar={false}
                    mostrarBotaoVoltar={false}
                    aoClicarEmSalvar={save}
                />
            </Box>
        </Box>
    )
}