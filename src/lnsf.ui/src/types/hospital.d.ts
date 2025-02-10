import { baseFilter } from "./baseFilter";

export type hospital = {
  id?: number;
  name: string;
  acronym: string;
};

export type hospitalFilter = baseFilter & {
  id?: number;
};
