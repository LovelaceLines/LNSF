import { ToastContainer } from "react-toastify";
import { AppRoutes } from "./Routes";
import 'react-toastify/dist/ReactToastify.min.css';
import './Component/forms/traducoesYup'


export const App = () => {

  return (
    <>
        <AppRoutes />
        <ToastContainer />
    </>
  );
};

export default App
