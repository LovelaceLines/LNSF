import { ToastContainer } from "react-toastify";
import { AppRoutes } from "./Routes";
import 'react-toastify/dist/ReactToastify.min.css';
import './Component/forms/traducoesYup'
import { Box, CssBaseline } from "@mui/material";

export const App = () => {
  return (
    <>
      <CssBaseline />
      <Box 
        width='100vw'
        height='100vh'
        overflow='hidden'
      >
        <AppRoutes />
        <ToastContainer />
      </Box>
    </>
  );
};