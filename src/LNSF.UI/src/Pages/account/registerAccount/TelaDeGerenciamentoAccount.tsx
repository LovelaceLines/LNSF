import { Box, Divider, FormControl, FormControlLabel, Grid, LinearProgress, Switch, Toolbar, Typography, useMediaQuery, useTheme } from "@mui/material";
import { useNavigate, useParams } from "react-router-dom"
import SupervisedUserCircleRoundedIcon from '@mui/icons-material/SupervisedUserCircleRounded';
import { ButtonAction } from "../../../Component";
import { ChangeEvent, useContext, useEffect, useState, } from "react";
import { AccountContext, iUser, iattAccount, iregisterAccount } from "../../../Contexts";
import { Form } from "@unform/web";
import { IFormErrorsCustom, TextFieldCustom, useCustomForm } from "../../../Component/forms";
import * as yup from 'yup';
import { TextSelectCustom } from "../../../Component/forms/TextSelectCustom";
import { is } from "date-fns/locale";

export const TelaDeGerenciamentoAccount: React.FC = () => {
    const { id = 'cadastrar' } = useParams<'id'>();
    const theme = useTheme();
    const smDown = useMediaQuery(theme.breakpoints.down('sm'));
    const navigate = useNavigate();
    const [isLoadind, setIsLoading] = useState(false);
    const [valueId, setvalueId] = useState('');
    const { getUserById, postUser, postAddUserToRole, putUser, putPassword, deleteRemoveUserFromRole } = useContext(AccountContext);
    const { formRef, save, isSaveAndClose, saveAndClose } = useCustomForm();
    const [isPasswordChange, setIsPasswordChange] = useState(false);

    const handleSwitchChange = (event: ChangeEvent<HTMLInputElement>) => {
        console.log(event.target.checked)
        setIsPasswordChange(event.target.checked);
    };

    const formValidateSchema: yup.Schema<iattAccount> = yup.object().shape({
        userName: yup.string().required().min(1),
        password: isPasswordChange === false ? yup.string() : yup.string().required().min(6),
        oldPassword: (isPasswordChange === false || id === 'cadastrar') ? yup.string() : yup.string().required().min(6),
        role: yup.number().required(),
    })

    useEffect(() => {

        if (id !== 'cadastrar') {
            setIsLoading(true);

            getUserById(id)
                .then((response) => {
                    if (response instanceof Error) {
                        setIsLoading(false);
                    } else {
                        formRef.current?.setData(response);
                        setvalueId(response.id)
                        setIsLoading(false);
                    }
                })
                .catch((error) => {
                    setIsLoading(false);
                    console.error('Detalhes do erro:', error);
                });
        } else {
            setIsPasswordChange(true)
        }
    }, [id])


    const handSave = (dados: iregisterAccount) => {

        formValidateSchema.
            validate(dados, { abortEarly: false })
            .then((dadosValidados) => {
                setIsLoading(true)

                if (id === 'cadastrar') {

                    const data: iregisterAccount = {
                        userName: dadosValidados.userName || '',
                        password: dadosValidados.password || '',
                        role: Number(dadosValidados.role),
                    }

                    postUser(data)
                        .then((response) => {
                            if (response instanceof Error) {
                                setIsLoading(false);
                            } else {
                                setIsLoading(false);
                                if (isSaveAndClose()) {
                                    navigate('/inicio/usuarios/gerenciar')
                                } else {
                                    navigate(`/inicio/usuarios/gerenciar/${response.id}`)
                                }
                            }
                        })
                        .catch((error) => {
                            setIsLoading(false);
                            console.error('Detalhes do erro:', error);
                        });

                } else {

                    const data: iUser = {
                        id: valueId,
                        userName: dadosValidados.userName,
                        email: dadosValidados.email,
                        phone: dadosValidados.phone,
                        roles: []
                    }
                    const data1: iattAccount = {
                        id: valueId,
                        newPassword: dadosValidados.password,
                        oldPassword: dadosValidados.oldPassword,
                    }

                    isPasswordChange ? putUser(data)
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
                        })
                        :
                        putUser(data1)
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
    <Box display='flex' flexDirection='column'>
      <Box>
        <Toolbar sx={{ flexGrow: 1, display: 'flex', flexDirection: smDown ? 'column' : 'row', alignItems: smDown ? 'left' : 'flex-end' }}>
          <Typography
            variant={smDown ? "h5" : "h4"}
            noWrap
            component="div"
            sx={{ flexGrow: 1, display: 'flex', alignItems: 'flex-end' }}
          >
            {!smDown && (<SupervisedUserCircleRoundedIcon color='primary' sx={{ fontSize: '2.7rem', paddingRight: '10px' }} />)}
            Usuários
          </Typography>

          <ButtonAction
            mostrarBotaoNovo={false}
            mostrarBotaoApagar={false}
            mostrarBotaoSalvar={id === 'cadastrar' ? false : true}
            mostrarBotaoSalvarEFechar={id !== 'cadastrar' ? false : true}
            aoClicarEmSalvar={id !== 'cadastrar' ? save : undefined}
            aoClicarEmSalvarEFechar={id === 'cadastrar' ? saveAndClose : undefined}
            aoClicarEmVoltar={() => window.history.back()}
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
                  {(id !== 'cadastrar') ? 'Editar este usuário' : 'Cadastrar um novo usuário'}
                  <Divider />
                </Typography>
              </Grid>
              <Grid container item direction='row' spacing={2}>
                <Grid item xs={6}>
                  <TextFieldCustom
                    fullWidth
                    label="Email"
                    name="userName"
                    disabled={isLoadind}
                  />
                </Grid>
                <Grid item xs={6}>
                  <TextSelectCustom
                      fullWidth
                      name="role"
                      menu={[
                        {
                          nome: 'Voluntário',
                          id: '0'
                        },
                        {
                          nome: 'Administrador',
                          id: '1'
                        },
                        {
                          nome: 'Assistente Social',
                          id: '2'
                        },
                        {
                          nome: 'Secretária',
                          id: '3'
                        }
                      ]}
                    disabled={isLoadind}
                    label="Função"
                  />
                </Grid>
              </Grid>
              {id !== 'cadastrar' && (<Grid item xs={6}>
                <FormControl >
                  <FormControlLabel
                    value="start"
                    control={
                      <Switch color="primary"
                        checked={isPasswordChange}
                        onChange={handleSwitchChange}
                      />}
                    label="Alterar Senha?"
                    labelPlacement="start"
                  />
                </FormControl>
              </Grid>)}

              <Grid container item direction='row' spacing={2}>
                <Grid item xs={6} >

                  <TextFieldCustom
                    fullWidth
                    label="Senha Nova"
                    name="password"
                    disabled={isLoadind || !isPasswordChange}
                  />
                </Grid>
                {id !== 'cadastrar' && (<Grid item xs={6}>
                  <TextFieldCustom
                    fullWidth
                    label="Senha Antiga"
                    name="oldPassword"
                    disabled={isLoadind || !isPasswordChange}
                  />
                </Grid>)}
              </Grid>
            </Grid>
          </Box>
        </Form>
      </Box>
    </Box>
  )
}