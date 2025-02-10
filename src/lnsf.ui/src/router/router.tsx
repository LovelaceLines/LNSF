import { createBrowserRouter, Navigate, RouteObject } from "react-router-dom";

import { AuthWrapper } from "@/auth-wrapper";
import { Loading } from "@/components";
import { MainLayout, LoginLayout } from "@/layouts";
import { NotFoundPage, UnauthorizedPage } from "@/pages";
import { EscortFormPage, EscortTablePage } from "@/pages/escort";
import { ChainDashboardPage } from "@/pages/chain";
import { HospitalFormPage, HospitalTablePage } from "@/pages/hospital";
import { HostingFormPage, HostingTablePage, PeopleRoomHostingTablePage } from "@/pages/hosting";
import { LogTablePage } from "@/pages/logEntry";
import { SingInPage } from "@/pages/(login)";
import { PeopleFormPage, PeopleTablePage } from "@/pages/people";
import { RoomFormPage, RoomTablePage } from "@/pages/room";
import { NotificationFormPage, NotificationTablePage } from "@/pages/notification";
import { SettingsPage } from "@/pages/settings";
import { TreatmentFormPage, TreatmentTablePage } from "@/pages/treatment";
import { TourDailyLogPage, TourTablePage } from "@/pages/tour";
import { PatientFormPage, PatientTablePage } from "@/pages/patient";
import { CurrentUserFormPage, UserFormPage, UserTablePage } from "@/pages/user";

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
			element: (
				<AuthWrapper authorizedRoles={["Desenvolvedor", "Administrador"]}>
					<UserTablePage />
				</AuthWrapper>
			),
		},
		{
			path: "usuarios/add",
			element: (
				<AuthWrapper authorizedRoles={["Desenvolvedor", "Administrador"]}>
					<UserFormPage />
				</AuthWrapper>
			),
		},
		{
			path: "usuarios/:id",
			element: (
				<AuthWrapper authorizedRoles={["Desenvolvedor", "Administrador"]}>
					<UserFormPage />
				</AuthWrapper>
			),
		},
		{
			path: "minha-conta",
			element: <CurrentUserFormPage />,
		},
		{
			path: "logs",
			element: (
				<AuthWrapper authorizedRoles={["Desenvolvedor", "Administrador"]}>
					<LogTablePage />
				</AuthWrapper>
			),
		},
		{
			path: "notificacoes",
			element: (
				<AuthWrapper authorizedRoles={["Desenvolvedor", "Administrador"]}>
					<NotificationTablePage />
				</AuthWrapper>
			),
		},
		{
			path: "notificacoes/add",
			element: (
				<AuthWrapper authorizedRoles={["Desenvolvedor", "Administrador"]}>
					<NotificationFormPage />
				</AuthWrapper>
			),
		},
		{
			path: "notificacoes/:id",
			element: (
				<AuthWrapper authorizedRoles={["Desenvolvedor", "Administrador"]}>
					<NotificationFormPage />
				</AuthWrapper>
			),
		},
		{
			path: "configuracoes",
			element: <SettingsPage />,
		},
		{
			path: "unauthorized",
			element: <UnauthorizedPage />,
		},
	],
};

const OtherRouters: RouteObject[] = [
	{
		path: "*",
		element: <NotFoundPage />,
	},
];

const routerObjects: RouteObject[] = [LoginRouters, MainRouters, ...OtherRouters];

export const router = createBrowserRouter(routerObjects);
