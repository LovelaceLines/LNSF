import { createBrowserRouter, Navigate, RouteObject } from "react-router-dom";

import { AuthWrapper } from "@/auth-wrapper";
import { MainLayout, LoginLayout } from "@/layouts";
import { SingInPage } from "@/pages/(login)";
import { Loading } from "@/components";
import { PeopleFormPage, PeopleTablePage } from "@/pages/people";
import { TourDailyLogPage, TourTablePage } from "@/pages/tour";
import { RoomFormPage, RoomTablePage } from "@/pages/room";
import { HospitalFormPage, HospitalTablePage } from "@/pages/hospital";
import { TreatmentFormPage, TreatmentTablePage } from "@/pages/treatment";
import { HostingFormPage, HostingTablePage, PeopleRoomHostingTablePage } from "@/pages/hosting";
import { PatientFormPage, PatientTablePage } from "@/pages/patient";
import { EscortFormPage, EscortTablePage } from "@/pages/escort";
import { CurrentUserFormPage, UserFormPage, UserTablePage } from "@/pages/user";
import { ChainDashboardPage } from "@/pages/chain";
import { LogTablePage } from "@/pages/logEntry";
import { NotFoundPage } from "@/pages";
import { SettingsPage } from "@/pages/settings";

const LoginRouters: RouteObject = {
	path: "/",
	element: <LoginLayout />,
	children: [
		{
			path: "/",
			element: <Navigate to="/login" replace />,
		},
		{
			path: "login",
			element: <SingInPage />,
		},
	],
};

const MainRouters: RouteObject = {
	path: "/app",
	element: (
		<AuthWrapper>
			<MainLayout />
		</AuthWrapper>
	),
	loader: () => <Loading />,
	children: [
		{
			path: "",
			element: <ChainDashboardPage />,
		},
		{
			path: "registro-diario",
			element: <TourDailyLogPage />,
		},
		{
			path: "registro-diario/historico",
			element: <TourTablePage />,
		},
		{
			path: "pessoas/pacientes",
			element: <PatientTablePage />,
		},
		{
			path: "pessoas/pacientes/add",
			element: <PatientFormPage />,
		},
		{
			path: "pessoas/pacientes/:id",
			element: <PatientFormPage />,
		},
		{
			path: "pessoas/acompanhantes",
			element: <EscortTablePage />,
		},
		{
			path: "pessoas/acompanhantes/add",
			element: <EscortFormPage />,
		},
		{
			path: "pessoas/acompanhantes/:id",
			element: <EscortFormPage />,
		},
		{
			path: "pessoas",
			element: <PeopleTablePage />,
		},
		{
			path: "pessoas/add",
			element: <PeopleFormPage />,
		},
		{
			path: "pessoas/:id",
			element: <PeopleFormPage />,
		},
		{
			path: "apartamentos",
			element: <RoomTablePage />,
		},
		{
			path: "apartamentos/add",
			element: <RoomFormPage />,
		},
		{
			path: "apartamentos/:id",
			element: <RoomFormPage />,
		},
		{
			path: "hospitais",
			element: <HospitalTablePage />,
		},
		{
			path: "hospitais/add",
			element: <HospitalFormPage />,
		},
		{
			path: "hospitais/:id",
			element: <HospitalFormPage />,
		},
		{
			path: "tratamentos",
			element: <TreatmentTablePage />,
		},
		{
			path: "tratamentos/add",
			element: <TreatmentFormPage />,
		},
		{
			path: "tratamentos/:id",
			element: <TreatmentFormPage />,
		},
		{
			path: "reservas",
			element: <HostingTablePage />,
		},
		{
			path: "reservas/add",
			element: <HostingFormPage />,
		},
		{
			path: "reservas/:id",
			element: <HostingFormPage />,
		},
		{
			path: "Hospedagens",
			element: <PeopleRoomHostingTablePage />,
		},
		{
			path: "usuarios",
			element: <UserTablePage />,
		},
		{
			path: "usuarios/add",
			element: <UserFormPage />,
		},
		{
			path: "usuarios/:id",
			element: <UserFormPage />,
		},
		{
			path: "minha-conta",
			element: <CurrentUserFormPage />,
		},
		{
			path: "logs",
			element: <LogTablePage />,
		},
		{
			path: "configuracoes",
			element: <SettingsPage />,
		},
		{
			path: "*",
			element: <Navigate to="/app" replace />,
		},
	],
};

const NotFoundRouters: RouteObject = {
	path: "*",
	element: <NotFoundPage />,
};

const routerObjects: RouteObject[] = [LoginRouters, MainRouters, NotFoundRouters];

export const router = createBrowserRouter(routerObjects);
