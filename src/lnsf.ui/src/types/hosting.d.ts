import { baseFilter } from "./baseFilter";
import { escort } from "./escort";
import { patient } from "./patient";

export type hosting = {
  id?: number;
  checkIn?: Date;
  checkOut?: Date;
  patientId: number;
  patient?: patient;
  escorts: escort[];
};

export type hostingEscort = {
  id?: number;
  hostingId: number;
  hosting?: hosting;
  escortId: number;
  escort?: escort;
};

export type hostingFilter = baseFilter & {
  id?: number;
  patientId?: number;
};
