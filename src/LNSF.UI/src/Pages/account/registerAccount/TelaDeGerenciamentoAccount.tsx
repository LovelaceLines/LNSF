import { Box, Button, Chip, Divider, FormControl, FormControlLabel, Grid, InputLabel, LinearProgress, MenuItem, NativeSelect, Select, Switch, TextField, Toolbar, Typography, useMediaQuery, useTheme } from "@mui/material";
import { useNavigate, useParams } from "react-router-dom"
import SupervisedUserCircleRoundedIcon from '@mui/icons-material/SupervisedUserCircleRounded';
import { ButtonAction } from "../../../Component";
import { ChangeEvent, useContext, useEffect, useState, } from "react";
import { AccountContext, RoleContext, iRegisterUser } from "../../../Contexts";
import { iregisterUser, iattPasswordUser, iregisterUserRole } from "../../../Contexts/accountContext/type";
import { RoleString } from "../../../Contexts/roleContext/type";
import { Form } from "@unform/web";
import SaveIcon from '@mui/icons-material/Save';
import { IFormErrorsCustom, TextFieldCustom, useCustomForm } from "../../../Component/forms";
import * as yup from 'yup';
import { TextSelectCustom } from "../../../Component/forms/TextSelectCustom";
import { iUser } from "../../../Contexts/accountContext/type";
 
const putUserFormValidateSchema: yup.Schema = yup.object<iUser>().shape({
  id: yup.string().required(),
  userName: yup.string().required().min(1),
  email: yup.string().required().email(),
  phoneNumber: yup.string().required().min(11),
});

const addUserFormValidateSchema: yup.Schema = yup.object<iRegisterUser>().shape({
  userName: yup.string().required().min(1),
  email: yup.string().required().email(),
  phoneNumber: yup.string().required().min(11),
  password: yup.string().required().min(6),
});

const passwordFormValidateSchema: yup.Schema = yup.object<iattPasswordUser>().shape({
  id: yup.string().required(),
  newPassword: yup.string().required().min(6),
  oldPassword: yup.string().required().min(6),
});

const userRoleFormValidateSchema: yup.Schema = yup.object<iregisterUserRole>().shape({
  userId: yup.string().required(),
  roleName: yup.string().required(),
});

export const TelaDeGerenciamentoAccount: React.FC = () => {
  const { id = 'cadastrar' } = useParams<'id'>();
  const theme = useTheme();
  const smDown = useMediaQuery(theme.breakpoints.down('sm'));
  const mdDown = useMediaQuery(theme.breakpoints.down('md'));
  const navigate = useNavigate();
  const [isLoadind, setIsLoading] = useState(false);
  const [valueId, setvalueId] = useState('');
  const { getUserById, postUser, postAddUserToRole, putUser, putPassword, deleteRemoveUserFromRole } = useContext(AccountContext);
  const { formRef, save, isSaveAndClose, saveAndClose } = useCustomForm();
  const [isPasswordChange, setIsPasswordChange] = useState(false);
  const [user, setUser] = useState<iUser>({} as iUser);
  const [updatedUser, setUpdatedUser] = useState<iUser>({} as iUser);
  const [updatedPassword, setUpdatedPassword] = useState<iattPasswordUser>({} as iattPasswordUser);
 
  useEffect(() => {
    if (id !== 'cadastrar') {
      setIsLoading(true);
 
      getUserById(id)
        .then(user => {
          setUser(user);
          setUpdatedUser(user);
          setUpdatedPassword({...updatedPassword, id: user.id})
        })
        .finally(() => setIsLoading(false))
    }
  }, [id])
 
  useEffect(() => console.log('user', user), [user])
  useEffect(() => console.log('updatedUser', updatedUser), [updatedUser])
  useEffect(() => console.log('updatedPassword', updatedPassword), [updatedPassword])
  useEffect(() => console.log('isPasswordChange', isPasswordChange), [isPasswordChange])
 
  const handlerSave = () => {
    setIsLoading(true);
    if (id === 'cadastrar') {
      addUserFormValidateSchema.validate(user, { abortEarly: false })
      .then(validData => {
        postUser(validData)
        .then(user => {
          setUser(user);
          setUpdatedUser(user);
        })
        .then(_ => {
          userRoleFormValidateSchema.validate({userId: updatedUser.id, roleName: updatedUser.roles ? updatedUser.roles[0] : ''}, { abortEarly: false })
          .then(validData => {
            postAddUserToRole(validData)
            .then(user => {
              setUser(user);
              setUpdatedUser(user);
            })
          })
        })
        .finally(() => setIsLoading(false))
      })
    } else {
      if (user.roles !== updatedUser.roles) {
        userRoleFormValidateSchema.validate({userId: updatedUser.id, roleName: updatedUser.roles ? updatedUser.roles[0] : ''}, { abortEarly: false })
        .then(validData => {
          deleteRemoveUserFromRole(validData)
          .then(user => {
            setUser(user);
            setUpdatedUser(user);
          })
        })
        .then(_ => {
          putUserFormValidateSchema.validate(updatedUser, { abortEarly: false })
          .then(validData => {
            putUser(validData)
            .then(user => {
              setUser(user);
              setUpdatedUser(user);
            })
            .then(_ => {
              userRoleFormValidateSchema.validate({userId: updatedUser.id, roleName: updatedUser.roles ? updatedUser.roles[0] : ''}, { abortEarly: false })
              .then(validData => {
                postAddUserToRole(validData)
                .then(user => {
                  setUser(user);
                  setUpdatedUser(user);
                })
              })
            })
            .finally(() => setIsLoading(false))
          }) 
        })
        // .catch((errors: yup.ValidationError) => {
        //     const ValidationError: IFormErrorsCustom = {}

        //     errors.inner.forEach(error => {
        //         if (!error.path) return;
        //         ValidationError[error.path] = error.message;
        //     });
        //     console.log(errors.errors);
        //     formRef.current?.setErrors(ValidationError)
        // })
      }
    }
  };
 
  const changePassword = () => (
    <Grid container item direction='row' spacing={2} paddingTop={2}>
      <FormControlLabel
        value="start"
        control={ <Switch checked={isPasswordChange} onChange={_ => setIsPasswordChange(!isPasswordChange)} /> }
        label="Alterar Senha?"
        labelPlacement="start"
      />
      <Grid container item direction='row' spacing={2}>
        <Grid item xs={smDown ? 12 : mdDown ? 6 : 4}>
          <TextFieldCustom
            value={updatedPassword.newPassword ?? ''}
            onChange={(e) => setUpdatedPassword({ ...updatedPassword, newPassword: e.target.value})}
            fullWidth
            label="Senha Nova"
            name="password"
            disabled={isLoadind || !isPasswordChange}
          />
        </Grid>
        <Grid item xs={smDown ? 12 : mdDown ? 6 : 4}>
          <TextFieldCustom
            value={updatedPassword.oldPassword ?? ''}
            onChange={(e) => setUpdatedPassword({ ...updatedPassword, oldPassword: e.target.value})}
            fullWidth
            label="Senha Antiga"
            name="oldPassword"
            disabled={isLoadind || !isPasswordChange}
          />
        </Grid>
      </ Grid>
    </Grid>
  );

  const registerOrChangeUser = () => (
    <Grid container spacing={2}>
      <Grid item xs={smDown ? 12 : mdDown ? 6 : 4}>
        <TextFieldCustom
          value={updatedUser?.userName ?? ''}
          onChange={(e) => setUpdatedUser({ ...updatedUser, userName: e.target.value})}
          fullWidth
          label="Nome de Usuário"
          name="userName"
          disabled={isLoadind}
        />
      </Grid>
      <Grid item xs={smDown ? 12 : mdDown ? 6 : 4}>
        <TextFieldCustom
          value={updatedUser?.email ?? ''}
          onChange={(e) => setUpdatedUser({ ...updatedUser, email: e.target.value})}
          fullWidth
          label="Email"
          name="userName"
          disabled={isLoadind}
        />
      </Grid>
      <Grid item xs={smDown ? 12 : mdDown ? 6 : 4}>
        <TextFieldCustom
          value={updatedUser?.phoneNumber ?? ''}
          onChange={(e) => setUpdatedUser({ ...updatedUser, phoneNumber: e.target.value})}
          fullWidth
          label="Número de Telefone"
          name="phoneNumber"
          disabled={isLoadind}
        />
      </Grid>
      <Grid item xs={smDown ? 12 : mdDown ? 6 : 4}>
        <TextSelectCustom
          value={updatedUser?.roles ? updatedUser.roles[0] : RoleString.voluntario}
          onChange={(e) => setUpdatedUser({ ...updatedUser, roles: [e.target.value]})}
          fullWidth
          name="role"
          menu={[
            {
              nome: RoleString.administrador,
              id: RoleString.administrador
            },
            {
              nome: RoleString.assistenteSocial,
              id: RoleString.assistenteSocial
            },
            {
              nome: RoleString.secretario,
              id: RoleString.secretario
            },
            {
              nome: RoleString.voluntario,
              id: RoleString.voluntario
            },
          ]}
          disabled={isLoadind}
          label="Função"
        />
      </Grid>
      {id === 'cadastrar' && (<>
      <Grid item xs={smDown ? 12 : mdDown ? 6 : 4}>
        <TextFieldCustom
          fullWidth
          label="Senha"
          name="password"
          disabled={isLoadind}
        />
      </Grid>
      <Grid item xs={smDown ? 12 : mdDown ? 6 : 4}>
        <TextFieldCustom
          fullWidth
          label="Confirme a Senha"
          name="password"
          disabled={isLoadind}
        />
      </Grid>
      </>)}
    </Grid>
  );

  const header = () => (
    <Box component={Toolbar} display='flex' flexDirection={smDown ? 'column' : 'row'}>
      <Typography variant={smDown ? "h5" : "h4"} display='flex' alignItems='center' gap={2} flexGrow={1}>
        {!smDown && (<SupervisedUserCircleRoundedIcon color='primary' fontSize="large"/>)}
        Usuário
      </Typography>

      {!smDown && saveButton()}
    </Box>
  );

  const saveButton = () => (
    <Button 
      variant={smDown ? 'contained' : 'outlined'}
      startIcon={<SaveIcon />}
      onClick={_ => handlerSave()}
    >
      Salvar
    </Button>
  );

  return (
    <Box>
      {header()}

      <Divider />

      <Box>
        <Form ref={formRef} onSubmit={_ => handlerSave()}>
          <Grid container direction='column' padding={2} gap={2}>

            {isLoadind && <LinearProgress variant="indeterminate" />}

            <Typography variant={smDown ? "h6" : "h5"} >
              {id === 'cadastrar' ? 'Cadastrar um novo usuário' : 'Editar este usuário'}
            </Typography>

            {registerOrChangeUser()}

            {id !== 'cadastrar' && changePassword()}

            {smDown && saveButton()}
          </Grid>
        </Form>
      </Box>
    </Box>
  )
}