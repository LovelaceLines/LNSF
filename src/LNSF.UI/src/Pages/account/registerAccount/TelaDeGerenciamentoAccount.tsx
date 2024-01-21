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
import DeleteIcon from '@mui/icons-material/Delete';
import { IFormErrorsCustom, TextFieldCustom, useCustomForm } from "../../../Component/forms";
import * as yup from 'yup';
import { TextSelectCustom } from "../../../Component/forms/TextSelectCustom";
import { iUser } from "../../../Contexts/accountContext/type";
import { toast } from "react-toastify";
import { set } from "date-fns";

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
  const [savePassword, setSavePassword] = useState<{ password: string, confirmPassword: string }>({ password: '', confirmPassword: '' });

  useEffect(() => {
    if (id !== 'cadastrar') {
      setIsLoading(true);

      getUserById(id)
        .then(user => {
          setUser(user);
          setUpdatedUser(user);
          setUpdatedPassword({ ...updatedPassword, id: user.id })
        })
        .finally(() => setIsLoading(false))
    }
  }, [id])

  useEffect(() => {
    console.log('user', user);
    setUpdatedUser(user);
  }, [user]);

  useEffect(() => console.log('updatedUser', updatedUser), [updatedUser])
  useEffect(() => console.log('updatedPassword', updatedPassword), [updatedPassword])
  useEffect(() => console.log('isPasswordChange', isPasswordChange), [isPasswordChange])

  const handlerSaveUSer = () => {
    if (savePassword.password !== savePassword.confirmPassword) {
      toast.error('As senhas não coincidem');
      return;
    }

    setIsLoading(true);
    postUser({ userName: updatedUser.userName, email: updatedUser.email, password: savePassword.password, phoneNumber: updatedUser.phoneNumber })
      .then(user => setUser(user))
      .finally(() => setIsLoading(false))
  };

  const handlerUpdateUser = () => {
    setIsLoading(true);
    putUser(updatedUser)
      .then(user => setUser(user))
      .finally(() => setIsLoading(false))
  }

  const handlerSaveUserRole = () => {
    setIsLoading(true);

    const role = updatedUser.roles![0] ?? '';

    postAddUserToRole({ userId: user.id, roleName: role })
      .then(user => setUser(user))
      .finally(() => setIsLoading(false))
  }

  const handlerRemoveUserRole = () => {
    setIsLoading(true);

    const role = updatedUser.roles![0] ?? '';

    deleteRemoveUserFromRole({ userId: updatedUser.id, roleName: role })
      .then(user => setUser(user))
      .finally(() => setIsLoading(false))
  }

  const handlerUpdatePassword = () => {
    setIsLoading(true);

    putPassword(updatedPassword)
      .then(user => setUser(user))
      .finally(() => setIsLoading(false))
  }

  const changePassword = () => (
    <Grid container item direction='row' spacing={2}>
      <Grid container item direction='row' spacing={2}>
        <Grid item xs={smDown ? 12 : mdDown ? 6 : 4}>
          <TextFieldCustom
            value={updatedPassword.newPassword ?? ''}
            onChange={(e) => setUpdatedPassword({ ...updatedPassword, newPassword: e.target.value })}
            fullWidth
            label="Senha Nova"
            name="password"
          />
        </Grid>
        <Grid item xs={smDown ? 12 : mdDown ? 6 : 4}>
          <TextFieldCustom
            value={updatedPassword.oldPassword ?? ''}
            onChange={(e) => setUpdatedPassword({ ...updatedPassword, oldPassword: e.target.value })}
            fullWidth
            label="Senha Antiga"
            name="oldPassword"
          />
        </Grid>
      </ Grid>
    </Grid >
  );

  const registerOrChangeUserRole = () => (
    <Grid item xs={smDown ? 12 : mdDown ? 6 : 4}>
      <TextSelectCustom
        value={updatedUser?.roles ? updatedUser.roles[0] : RoleString.voluntario}
        onChange={(e) => setUpdatedUser({ ...updatedUser, roles: [e.target.value] })}
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
  );

  const registerOrChangeUser = () => (
    <Grid container spacing={2}>
      <Grid item xs={smDown ? 12 : mdDown ? 6 : 4}>
        <TextFieldCustom
          value={updatedUser?.userName ?? ''}
          onChange={(e) => setUpdatedUser({ ...updatedUser, userName: e.target.value })}
          fullWidth
          label="Nome de Usuário"
          name="userName"
          disabled={isLoadind}
        />
      </Grid>
      <Grid item xs={smDown ? 12 : mdDown ? 6 : 4}>
        <TextFieldCustom
          value={updatedUser?.email ?? ''}
          onChange={(e) => setUpdatedUser({ ...updatedUser, email: e.target.value })}
          fullWidth
          label="Email"
          name="userName"
          disabled={isLoadind}
        />
      </Grid>
      <Grid item xs={smDown ? 12 : mdDown ? 6 : 4}>
        <TextFieldCustom
          value={updatedUser?.phoneNumber ?? ''}
          onChange={(e) => setUpdatedUser({ ...updatedUser, phoneNumber: e.target.value })}
          fullWidth
          label="Número de Telefone"
          name="phoneNumber"
          disabled={isLoadind}
        />
      </Grid>

      {id === 'cadastrar' && (<>
        <Grid item xs={smDown ? 12 : mdDown ? 6 : 4}>
          <TextFieldCustom
            value={savePassword.password ?? ''}
            onChange={(e) => setSavePassword({ ...savePassword, password: e.target.value })}
            fullWidth
            label="Senha"
            name="password"
            disabled={isLoadind}
          />
        </Grid>
        <Grid item xs={smDown ? 12 : mdDown ? 6 : 4}>
          <TextFieldCustom
            value={savePassword.confirmPassword ?? ''}
            onChange={(e) => setSavePassword({ ...savePassword, confirmPassword: e.target.value })}
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
        {!smDown && (<SupervisedUserCircleRoundedIcon color='primary' fontSize="large" />)}
        Usuário
      </Typography>
    </Box>
  );

  const saveButtonRegisterOrChangeUser = () => (
    <Button
      variant={smDown ? 'contained' : 'outlined'}
      startIcon={<SaveIcon />}
      onClick={_ => id === 'cadastrar' ? handlerSaveUSer() : handlerUpdateUser()}
    >
      {user.id ? 'Alterar Usuário' : 'Adicionar Usuário'}
    </Button>
  );

  const saveButtonUserRole = () => (
    <Button
      variant={smDown ? 'contained' : 'outlined'}
      startIcon={user.roles ? <DeleteIcon /> : <SaveIcon />}
      onClick={_ => user.roles ? handlerRemoveUserRole() : handlerSaveUserRole()}
      color={user.roles ? 'error' : 'primary'}
    >
      {user.roles ? 'Remover Função' : 'Adicionar Função'}
    </Button>
  );

  const saveButtonChangePassword = () => (
    <Button
      variant={smDown ? 'contained' : 'outlined'}
      startIcon={<SaveIcon />}
      onClick={_ => handlerUpdatePassword()}
    >
      Alterar Senha
    </Button>
  );

  return (
    <Box>
      {header()}
      <Divider />
      <Box>
        <Form ref={formRef} onSubmit={_ => console.log('submit')}>
          <Grid container direction='column' padding={2} gap={2}>
            <Box display='flex' alignItems='center' justifyContent='space-between'>
              <Typography variant="subtitle2" >
                Informações Básicas
              </Typography>
              {!smDown && saveButtonRegisterOrChangeUser()}
            </Box>

            {registerOrChangeUser()}

            {smDown && saveButtonRegisterOrChangeUser()}
          </Grid>
        </Form>
        <Form ref={formRef} onSubmit={_ => console.log('submit')}>
          <Grid container direction='column' padding={2} gap={2}>
            <Box display='flex' alignItems='center' justifyContent='space-between'>
              <Typography variant="subtitle2" >
                Configurar Função
              </Typography>
              {!smDown && saveButtonUserRole()}
            </Box>

            {registerOrChangeUserRole()}

            {smDown && saveButtonUserRole()}
          </Grid>
        </Form>
        {id !== 'cadastrar' && (<>
          <Form ref={formRef} onSubmit={_ => console.log('submit')}>
            <Grid container direction='column' padding={2} gap={2}>
              <Box display='flex' alignItems='center' justifyContent='space-between'>
                <Typography variant='subtitle2' >
                  Alteração de Senha
                </Typography>
                {!smDown && saveButtonChangePassword()}
              </Box>

              {changePassword()}

              {smDown && saveButtonChangePassword()}
            </Grid>
          </Form>
        </>)}
      </Box>
    </Box>
  )
}