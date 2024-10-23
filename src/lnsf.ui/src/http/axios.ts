import axios, { AxiosInstance } from "axios";

import env from "@/env";
import { getAuthToken } from "@/services";
import { useConfig, useRequestError } from "./interceptor-request";
import { useResponse, useResponseError } from "./interceptor-response";

export const Axios: AxiosInstance = axios.create({
	baseURL: env.API_URL,
	timeout: 30000,
	headers: {
		"Content-Type": "application/json",
		Authorization: `Bearer ${getAuthToken().accessToken || ""}`,
	},
});

Axios.interceptors.request.use(useConfig, useRequestError);
Axios.interceptors.response.use(useResponse, useResponseError);
