import { baseFilter } from "./baseFilter";

export type emergencyContact = {
  id?: number;
  name: string;
  phone: string;
  peopleId: number;
};

export type emergencyContactFilter = baseFilter & {
  id?: number;
  name?: string;
  phone?: string;
  peopleId?: number;
};
