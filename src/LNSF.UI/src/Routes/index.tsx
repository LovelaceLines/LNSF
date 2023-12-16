import { Routes, Route, Navigate } from 'react-router-dom';
import { AuthContext, iUser } from '../Contexts';
import { useContext, useState } from 'react';
import { Account, Dashboard, LoginPage, PersonalData, PutAllPasseio, RegisterRoom, RegisterTour, TelaDeGerenciamentoAccount, TelaDeGerenciamentoPeople, TelaDeGerenciamentoRoom, TelaRegisterUpdateContactEmergence, ViewPeople, ViewRoom, ViewTour } from '../Pages/index';
import { ProtectedRoutes } from '../ProtectedRoutes';
import { ViewHospital } from '../Pages/hospital/viewHospital/ViewHospital';
import { TelaDeGerenciamentoHospital } from '../Pages/hospital/registerHospital/TelaDeGerenciamentoHospital';
import { TelaDeGerenciamentoTratamentos } from '../Pages/tratamentos/registerTratamentos/TelaDeGerenciamentoTratamento';
import { ViewTratamentos } from '../Pages/tratamentos/viewTratamentos/ViewTratamentos';
import { Hosting } from '../Pages/hosting/viewHosting/ViewHosting_';


export const AppRoutes = () => {

    

    return (
        <>
            <Routes>
                <Route path='/' element={<LoginPage />} />

                {/* <Route element={<ProtectedRoutes />}> */}
                    <Route path="/inicio" element={<Dashboard />} />

                    <Route path="/inicio/apartamentos/visualizar" element={<Dashboard children={<ViewRoom />} />} />

                    <Route path="/inicio/hospedagens/visualizar" element={<Dashboard children={<Hosting />} />} />

                    <Route path="/inicio/apartamentos/gerenciar" element={<Dashboard children={<RegisterRoom />} />} />
                    <Route path="/inicio/apartamentos/gerenciar/:id" element={<Dashboard children={<TelaDeGerenciamentoRoom />} />} />

                    <Route path="/inicio/pessoas/visualizar" element={<Dashboard children={<ViewPeople />} />} />
                    <Route path="/inicio/pessoas/dados/:id" element={<Dashboard children={<PersonalData />} />} />
                    <Route path="/inicio/pessoas/dados/contatoEmergencia/:id" element={<Dashboard children={<TelaRegisterUpdateContactEmergence />} />} />
                    <Route path="/inicio/pessoas/gerenciar/:id" element={<Dashboard children={<TelaDeGerenciamentoPeople />} />} />

                    <Route path="/inicio/usuarios/gerenciar" element={<Dashboard children={<Account />} />} />
                    <Route path="/inicio/usuarios/gerenciar/:id" element={<Dashboard children={<TelaDeGerenciamentoAccount />} />} />

                    <Route path="/inicio/hospital/visualizar" element={<Dashboard children={<ViewHospital />} />} />
                    <Route path="/inicio/hospital/gerenciar/:id" element={<Dashboard children={<TelaDeGerenciamentoHospital />} />} />

                    <Route path="/inicio/tratamentos/visualizar" element={<Dashboard children={<ViewTratamentos />} />} />
                    <Route path="/inicio/tratamentos/gerenciar/:id" element={<Dashboard children={<TelaDeGerenciamentoTratamentos />} />} />


                    <Route path="/inicio/registrodiario/visualizar" element={<Dashboard children={<ViewTour />} />} />
                    <Route path="/inicio/registrodiario/adicionar" element={<Dashboard children={<RegisterTour />} />} />
                    <Route path="/inicio/registrodiario/visualizar/:id" element={<Dashboard children={<PutAllPasseio />} />} />
                {/* </Route> */}
                <Route path='*' element={<Navigate to='/' />} />
            </Routes >
        </>
    );
};

