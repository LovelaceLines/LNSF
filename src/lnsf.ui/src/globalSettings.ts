import env from "./env";
import { defaultRole } from "./types";

export const getAllRoles = (): defaultRole[] =>
  env.NODE_ENV == "development"
    ? ["Desenvolvedor", "Administrador", "Assistente Social", "Secretário", "Voluntário"]
    : ["Administrador", "Assistente Social", "Secretário", "Voluntário"];

export const getAdminRoles = (): defaultRole[] =>
  env.NODE_ENV == "development" ? ["Desenvolvedor", "Administrador"] : ["Administrador"];
