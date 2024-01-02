import { Routes, Route, Navigate } from 'react-router-dom';
import { AuthContext, RoleContext, iUser } from '../Contexts';
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

  // const { isAdministrador, isAssistenteSocial, isDesenvolvedor, isSecretario, isVoluntario } = useContext(RoleContext);
  const [isAdministrador, setIsAdministrador] = useState(true);
  const [isAssistenteSocial, setIsAssistenteSocial] = useState(true);
  const [isDesenvolvedor, setIsDesenvolvedor] = useState(true);
  const [isSecretario, setIsSecretario] = useState(true);
  const [isVoluntario, setIsVoluntario] = useState(true);

  // useEffect(() => {
  //   console.log("useEffect AppRoutes")
  //   const loadUser = async () => {
  //     setUser(await getUser());
  //   }

  //   loadUser();

  //   if (!!user && user.roles.includes("Desenvolvedor")) {
  //     setIsDesenvolvedor(true);
  //   }

  //   if (!!user && user.roles.includes("Administrador")) {
  //     setIsAdministrador(true);
  //   }

  //   if (!!user && user.roles.includes("Assistente Social")) {
  //     setIsAssistenteSocial(true);
  //   }

  //   if (!!user && user.roles.includes("Secretário")) {
  //     setIsSecretario(true);
  //   }

  //   if (!!user && user.roles.includes("Voluntário")) {
  //     setIsVoluntario(true);
  //   }
  // }, []);

  return (
    <>
      <Routes>
        <Route path='/login' element={<LoginPage />} />

        <Route element={<ProtectedRoutes />}>
          {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntario) && 
            <Route path="/inicio" element={<Dashboard />} />}

          {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntario) &&
            <Route path="/inicio/apartamentos/visualizar" element={<Dashboard children={<ViewRoom />} />} />}

          {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntario) &&
            <Route path="/inicio/apartamentos/gerenciar" element={<Dashboard children={<RegisterRoom />} />} />}

          {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntario) &&
            <Route path="/inicio/apartamentos/gerenciar/:id" element={<Dashboard children={<TelaDeGerenciamentoRoom />} />} />}

          {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntario) &&
            <Route path="/inicio/hospedagens/visualizar" element={<Dashboard children={<Hosting />} />} />}

          {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntario) &&
            <Route path="/inicio/pessoas/visualizar" element={<Dashboard children={<ViewPeople />} />} />}

          {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntario) &&
            <Route path="/inicio/pessoas/dados/:id" element={<Dashboard children={<PersonalData />} />} />}

          {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntario) &&
            <Route path="/inicio/pessoas/dados/contatoEmergencia/:id" element={<Dashboard children={<TelaRegisterUpdateContactEmergence />} />} />}
          
          {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntario) &&
            <Route path="/inicio/pessoas/gerenciar/:id" element={<Dashboard children={<TelaDeGerenciamentoPeople />} />} />}

          {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntario) &&
            <Route path="/inicio/usuarios/gerenciar" element={<Dashboard children={<Account />} />} />}

          {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntario) &&
            <Route path="/inicio/usuarios/gerenciar/:id" element={<Dashboard children={<TelaDeGerenciamentoAccount />} />} />}

          {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntario) &&
            <Route path="/inicio/hospital/visualizar" element={<Dashboard children={<ViewHospital />} />} />}

          {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntario) &&
            <Route path="/inicio/hospital/gerenciar/:id" element={<Dashboard children={<TelaDeGerenciamentoHospital />} />} />}

          {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntario) &&
            <Route path="/inicio/tratamentos/visualizar" element={<Dashboard children={<ViewTratamentos />} />} />}

          {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntario) &&
            <Route path="/inicio/tratamentos/gerenciar/:id" element={<Dashboard children={<TelaDeGerenciamentoTratamentos />} />} />}

          {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntario) &&
            <Route path="/inicio/registrodiario/visualizar" element={<Dashboard children={<ViewTour />} />} />}

          {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntario) &&
            <Route path="/inicio/registrodiario/adicionar" element={<Dashboard children={<RegisterTour />} />} />}

          {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntario) &&
            <Route path="/inicio/registrodiario/visualizar/:id" element={<Dashboard children={<PutAllPasseio />} />} />}
        </Route>
        <Route path='*' element={<Navigate to='/login' />} />
      </Routes >
    </>
  );
};