import { Routes, Route, Navigate } from 'react-router-dom';
import { AuthContext, iUser } from '../Contexts';
import { useContext, useEffect, useState } from 'react';
import { Account, Dashboard, LoginPage, PersonalData, PutAllPasseio, RegisterRoom, RegisterTour, TelaDeGerenciamentoAccount, TelaDeGerenciamentoPeople, TelaDeGerenciamentoRoom, TelaRegisterUpdateContactEmergence, ViewPeople, ViewRoom, ViewTour } from '../Pages/index';
import { ProtectedRoutes } from '../ProtectedRoutes';
import { ViewHospital } from '../Pages/hospital/viewHospital/ViewHospital';
import { TelaDeGerenciamentoHospital } from '../Pages/hospital/registerHospital/TelaDeGerenciamentoHospital';
import { TelaDeGerenciamentoTratamentos } from '../Pages/tratamentos/registerTratamentos/TelaDeGerenciamentoTratamento';
import { ViewTratamentos } from '../Pages/tratamentos/viewTratamentos/ViewTratamentos';
import { Hosting } from '../Pages/hosting/viewHosting/ViewHosting_';

export const AppRoutes = () => {
  const { getUser } = useContext(AuthContext);
  const [user, setUser] = useState<iUser>();
  const [ isDesenvolvedor, setIsDesenvolvedor ] = useState<boolean>(false);
  const [ isAdministrador, setIsAdministrador ] = useState<boolean>(false);
  const [ isAssistenteSocial, setIsAssistenteSocial ] = useState<boolean>(false);
  const [ isSecretario, setIsSecretario ] = useState<boolean>(false);
  const [ isVoluntorio, setIsVoluntorio ] = useState<boolean>(false);

  useEffect(() => {
    const loadUser = async () => {
      const user = await getUser();
      setUser(user);
    }
    loadUser();

    if (user?.roles.includes("Desenvolvedor")) {
      setIsDesenvolvedor(true);
    }

    if (user?.roles.includes("Administrador")) {
      setIsAdministrador(true);
    }

    if (user?.roles.includes("Assistente Social")) {
      setIsAssistenteSocial(true);
    }

    if (user?.roles.includes("Secretário")) {
      setIsSecretario(true);
    }

    if (user?.roles.includes("Voluntário")) {
      setIsVoluntorio(true);
    }
  }, [user]);

  return (
    <>
      <Routes>
        <Route path='/login' element={<LoginPage />} />

        <Route element={<ProtectedRoutes />}>
          {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntorio) && 
            <Route path="/inicio" element={<Dashboard />} />}

          {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntorio) &&
            <Route path="/inicio/apartamentos/visualizar" element={<Dashboard children={<ViewRoom />} />} />}

          {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntorio) &&
            <Route path="/inicio/apartamentos/gerenciar" element={<Dashboard children={<RegisterRoom />} />} />}

          {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntorio) &&
            <Route path="/inicio/apartamentos/gerenciar/:id" element={<Dashboard children={<TelaDeGerenciamentoRoom />} />} />}

          {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntorio) &&
            <Route path="/inicio/hospedagens/visualizar" element={<Dashboard children={<Hosting />} />} />}

          {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntorio) &&
            <Route path="/inicio/pessoas/visualizar" element={<Dashboard children={<ViewPeople />} />} />}

          {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntorio) &&
            <Route path="/inicio/pessoas/dados/:id" element={<Dashboard children={<PersonalData />} />} />}

          {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntorio) &&
            <Route path="/inicio/pessoas/dados/contatoEmergencia/:id" element={<Dashboard children={<TelaRegisterUpdateContactEmergence />} />} />}
          
          {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntorio) &&
            <Route path="/inicio/pessoas/gerenciar/:id" element={<Dashboard children={<TelaDeGerenciamentoPeople />} />} />}

          {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntorio) &&
            <Route path="/inicio/usuarios/gerenciar" element={<Dashboard children={<Account />} />} />}

          {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntorio) &&
            <Route path="/inicio/usuarios/gerenciar/:id" element={<Dashboard children={<TelaDeGerenciamentoAccount />} />} />}

          {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntorio) &&
            <Route path="/inicio/hospital/visualizar" element={<Dashboard children={<ViewHospital />} />} />}

          {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntorio) &&
            <Route path="/inicio/hospital/gerenciar/:id" element={<Dashboard children={<TelaDeGerenciamentoHospital />} />} />}

          {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntorio) &&
            <Route path="/inicio/tratamentos/visualizar" element={<Dashboard children={<ViewTratamentos />} />} />}

          {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntorio) &&
            <Route path="/inicio/tratamentos/gerenciar/:id" element={<Dashboard children={<TelaDeGerenciamentoTratamentos />} />} />}

          {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntorio) &&
            <Route path="/inicio/registrodiario/visualizar" element={<Dashboard children={<ViewTour />} />} />}

          {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntorio) &&
            <Route path="/inicio/registrodiario/adicionar" element={<Dashboard children={<RegisterTour />} />} />}

          {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntorio) &&
            <Route path="/inicio/registrodiario/visualizar/:id" element={<Dashboard children={<PutAllPasseio />} />} />}
        </Route>
        <Route path='*' element={<Navigate to='/inicio' />} />
      </Routes >
    </>
  );
};