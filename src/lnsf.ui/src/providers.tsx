import { RouterProvider } from "react-router-dom";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.min.css";

import { ThemeProvider } from "@/theme";
import { router } from "./router";
import { SideBarProvider, ModalProvider, SnackbarProvider, FilterProvider, TabProvider, LayersProvider } from "@/contexts";
import { TableProvider } from "./tables";
import { LocalizationProvider } from "@mui/x-date-pickers";
import { AdapterDateFns } from "@mui/x-date-pickers/AdapterDateFnsV3";
import { ptBR } from "date-fns/locale";

export const Providers = () => {
	return (
		<ThemeProvider>
			<LocalizationProvider dateAdapter={AdapterDateFns} adapterLocale={ptBR}>
				<ToastContainer position="bottom-right" />
				<SideBarProvider>
					<SnackbarProvider>
						<ModalProvider>
							<FilterProvider>
								<TabProvider>
									<TableProvider>
										<LayersProvider>
											<RouterProvider router={router} />
										</LayersProvider>
									</TableProvider>
								</TabProvider>
							</FilterProvider>
						</ModalProvider>
					</SnackbarProvider>
				</SideBarProvider>
			</LocalizationProvider>
		</ThemeProvider>
	);
};
