import { RouterProvider } from "react-router-dom";
import { Provider as ReduxProvider } from "react-redux";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.min.css";

import { store } from "@/redux/store";
import { ThemeProvider } from "@/theme";
import { router } from "./router";
import { SideBarProvider, ModalProvider, SnackbarProvider, FilterProvider, TabProvider, LayersProvider } from "@/contexts";
import { TableProvider } from "./tables";

export const Providers = () => {
	return (
		<ThemeProvider>
			<ReduxProvider store={store}>
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
			</ReduxProvider>
		</ThemeProvider>
	);
};
