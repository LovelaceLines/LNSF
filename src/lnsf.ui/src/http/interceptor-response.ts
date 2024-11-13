import { useAuthStore } from "@/store";
import { AxiosResponse } from "axios";
import { toast } from "react-toastify";

export const useResponse = (response: AxiosResponse<any, any>) => {
	console.debug("API/UI Response:", response);

	if (
		response.config.method === "post" ||
		response.config.method === "put" ||
		response.config.method === "delete"
	) {
		toast.success(response.data.message ?? "Operação realizada com sucesso");
	}

	return response;
};

export const useResponseError = (error: any) => {
	console.debug("API/UI Error:", error);

	if (error.code === "ECONNABORTED") {
		toast.error("Tempo de requisição excedido");
	} else if (error.code == "ERR_NETWORK") {
		toast.error("Erro de conexão com o servidor");
	} else if (error.response.status === 401) {
		toast.error("Sessão expirada, faça login novamente");
		const { logoutUser } = useAuthStore();
		logoutUser();
	} else {
		toast.error(error.response.data.message ?? "Erro desconhecido");
	}

	return Promise.reject(error.response.data);
};
