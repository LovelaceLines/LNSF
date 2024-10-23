import { RouterProvider } from "react-router-dom";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.min.css";

import { ThemeProvider } from "@/theme";
import { router } from "./router";
import { SideBarProvider, ModalProvider, SnackbarProvider, FilterProvider, TabProvider, LayersProvider } from "@/contexts";
import { TableProvider } from "./tables";

export const Providers = () => {
	return (
		<ThemeProvider>
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
		</ThemeProvider>
	);
};
