import { Routes, Route, Navigate } from 'react-router-dom';
import { AuthContext } from '../Contexts';
import { useContext } from 'react';
import { Account, Dashboard, LoginPage, PersonalData, PutAllPasseio, RegisterRoom, RegisterTour, TelaDeGerenciamentoAccount, TelaDeGerenciamentoPeople, TelaDeGerenciamentoRoom, TelaRegisterUpdateContactEmergence, ViewPeople, ViewRoom, ViewTour } from '../Pages/index';
import { ProtectedRoutes } from '../ProtectedRoutes';
import { ViewHospital } from '../Pages/hospital/viewHospital/ViewHospital';
import { TelaDeGerenciamentoHospital } from '../Pages/hospital/registerHospital/TelaDeGerenciamentoHospital';
import { TelaDeGerenciamentoTratamentos } from '../Pages/tratamentos/registerTratamentos/TelaDeGerenciamentoTratamento';
import { ViewTratamentos } from '../Pages/tratamentos/viewTratamentos/ViewTratamentos';

export const AppRoutes = () => {

    const { user } = useContext(AuthContext);

    return (
        <>
            <Routes>
                <Route path='/' element={<LoginPage />} />

                <Route element={<ProtectedRoutes />}>
                    <Route path="/inicio" element={<Dashboard />} />

                    <Route path="/inicio/apartamentos/visualizar" element={<Dashboard children={<ViewRoom />} />} />

                    {user.role !== 0 && <Route path="/inicio/apartamentos/gerenciar" element={<Dashboard children={<RegisterRoom />} />} />}
                    {user.role !== 0 && <Route path="/inicio/apartamentos/gerenciar/:id" element={<Dashboard children={<TelaDeGerenciamentoRoom />} />} />}

                    {user.role !== 0 && <Route path="/inicio/pessoas/visualizar" element={<Dashboard children={<ViewPeople />} />} />}
                    {user.role !== 0 && <Route path="/inicio/pessoas/dados/:id" element={<Dashboard children={<PersonalData />} />} />}
                    {user.role !== 0 && <Route path="/inicio/pessoas/dados/contatoEmergencia/:id" element={<Dashboard children={<TelaRegisterUpdateContactEmergence />} />} />}
                    {user.role !== 0 && <Route path="/inicio/pessoas/gerenciar/:id" element={<Dashboard children={<TelaDeGerenciamentoPeople />} />} />}

                    {user.role === 1 && <Route path="/inicio/usuarios/gerenciar" element={<Dashboard children={<Account />} />} />}
                    {user.role !== 0 && <Route path="/inicio/usuarios/gerenciar/:id" element={<Dashboard children={<TelaDeGerenciamentoAccount />} />} />}

                    {user.role !== 0 && <Route path="/inicio/hospital/visualizar" element={<Dashboard children={<ViewHospital />} />} />}
                    {user.role !== 0 && <Route path="/inicio/hospital/gerenciar/:id" element={<Dashboard children={<TelaDeGerenciamentoHospital />} />} />}

                    {user.role !== 0 && <Route path="/inicio/tratamentos/visualizar" element={<Dashboard children={<ViewTratamentos />} />} />}
                    {user.role !== 0 && <Route path="/inicio/tratamentos/gerenciar/:id" element={<Dashboard children={<TelaDeGerenciamentoTratamentos />} />} />}


                    <Route path="/inicio/registrodiario/visualizar" element={<Dashboard children={<ViewTour />} />} />
                    <Route path="/inicio/registrodiario/adicionar" element={<Dashboard children={<RegisterTour />} />} />
                    <Route path="/inicio/registrodiario/visualizar/:id" element={<Dashboard children={<PutAllPasseio />} />} />
                </Route>
                <Route path='*' element={<Navigate to='/' />} />
            </Routes >
        </>
    );
};

