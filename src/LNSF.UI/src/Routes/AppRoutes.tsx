import { Routes, Route, Navigate } from 'react-router-dom';
import { RoleContext } from '../Contexts';
import { useContext } from 'react';
import { ViewAccount, Dashboard, LoginPage, PersonalData, PutAllPasseio, RegisterRoom, RegisterTour, TelaDeGerenciamentoAccount, TelaDeGerenciamentoPeople, TelaDeGerenciamentoRoom, TelaRegisterUpdateContactEmergence, ViewPeople, ViewRoom, ViewTour, ViewFamilyGroupProfile } from '../Pages/index';
import { ViewHospital } from '../Pages/hospital/viewHospital/ViewHospital';
import { TelaDeGerenciamentoHospital } from '../Pages/hospital/registerHospital/TelaDeGerenciamentoHospital';
import { TelaDeGerenciamentoTratamentos } from '../Pages/tratamentos/registerTratamentos/TelaDeGerenciamentoTratamento';
import { ViewTratamentos } from '../Pages/tratamentos/viewTratamentos/ViewTratamentos';
import { ViewHosting } from '../Pages/hosting/viewHosting/ViewHosting';
import { Footer } from '../Component/Layouts/footer/Footer';
import { NotFound } from '../Pages/NotFound/NotFound';
import { ProtectedRoutes } from '.';
import { ViewPeopleRoomHosting } from '../Pages/peopleRoomHosting/ViewPeopleRoomHosting';

export const AppRoutes = () => {
  const { isAdministrador, isAssistenteSocial, isDesenvolvedor, isSecretario, isVoluntario } = useContext(RoleContext);

  return (
    <Routes>
      <Route path='/login' element={<LoginPage />} />

      <Route element={<ProtectedRoutes />}>
        {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntario) &&
          <Route path="/apartamentos/visualizar" element={<Dashboard children={<ViewRoom />} />} />}

        {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntario) &&
          <Route path="/apartamentos/gerenciar/:id" element={<Dashboard children={<TelaDeGerenciamentoRoom />} />} />}

        {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntario) &&
          <Route path="/apartamentos/gerenciar" element={<Dashboard children={<RegisterRoom />} />} />}

        {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntario) &&
          <Route path="/hospedagens/visualizar" element={<Dashboard children={<ViewHosting />} />} />}

        {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntario) &&
          <Route path='/hospedagens/pessoashospedadas/visualizar' element={<Dashboard children={<ViewPeopleRoomHosting />} />} />}

        {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntario) &&
          <Route path="/pessoas/visualizar" element={<Dashboard children={<ViewPeople />} />} />}

        {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntario) &&
          <Route path="/inicio/pessoas/dados/contatoEmergencia/:id" element={<Dashboard children={<TelaRegisterUpdateContactEmergence />} />} />}

        {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntario) &&
          <Route path="/inicio/pessoas/dados/:id" element={<Dashboard children={<PersonalData />} />} />}

        {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntario) &&
          <Route path="/pessoas/gerenciar/:id" element={<Dashboard children={<TelaDeGerenciamentoPeople />} />} />}

        {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntario) &&
          <Route path="/usuarios/gerenciar/:id" element={<Dashboard children={<TelaDeGerenciamentoAccount />} />} />}

        {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntario) &&
          <Route path="/usuarios/visualizar" element={<Dashboard children={<ViewAccount />} />} />}

        {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntario) &&
          <Route path="/hospital/visualizar" element={<Dashboard children={<ViewHospital />} />} />}

        {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntario) &&
          <Route path="/hospital/gerenciar/:id" element={<Dashboard children={<TelaDeGerenciamentoHospital />} />} />}

        {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntario) &&
          <Route path="/tratamentos/visualizar" element={<Dashboard children={<ViewTratamentos />} />} />}

        {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntario) &&
          <Route path="/tratamentos/gerenciar/:id" element={<Dashboard children={<TelaDeGerenciamentoTratamentos />} />} />}

        {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntario) &&
          <Route path="/registrodiario/:id" element={<Dashboard children={<PutAllPasseio />} />} />}

        {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntario) &&
          <Route path="/registrodiario/visualizar" element={<Dashboard children={<ViewTour />} />} />}

        {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntario) &&
          <Route path="/registrodiario/cadastrar" element={<Dashboard children={<RegisterTour />} />} />}

        {(isDesenvolvedor || isAdministrador || isSecretario || isAssistenteSocial || isVoluntario) &&
          <Route path='/registrosocioeconomico/perfildogrupofamiliar/visualizar' element={<Dashboard children={<ViewFamilyGroupProfile />} />} />}

        <Route path="/inicio" element={<Dashboard />} />
        <Route path="/home" element={<Dashboard />} />
        <Route path='/' element={<Navigate to='/inicio' />} />
        <Route path="/sobre" element={<Dashboard children={<Footer />} />} />
        <Route path='*' element={<NotFound />} />
      </Route>

      <Route path='*' element={<Navigate to='/login' />} />
    </Routes >
  );
};