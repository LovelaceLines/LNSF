import { InternalAxiosRequestConfig } from "axios";

import { getAuthToken } from "@/services";

export const useConfig = (config: InternalAxiosRequestConfig<any>) => {
  config.headers.Authorization = `Bearer ${getAuthToken().accessToken || ""}`;
  console.debug("API/UI Request:", config);
  return config;
};

export const useRequestError = (error: any) => {
  console.debug("API/UI Request Error:", error);
  return Promise.reject(error);
};
