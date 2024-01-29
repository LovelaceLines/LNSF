import { iPeopleObject } from "..";
import { iHospitalObject } from "../hospitalContext/type";
import { iTreatmentObject } from "../treatmentContext/type";
import { iOrderBy, iPage } from "../types";

export interface iPatientProvider {
  children: React.ReactNode
}

export interface iPatientFilter {
  id?: number,
  peopleId?: number,
  hospitalId?: number,
  socioeconomicRecord?: boolean,
  term?: boolean,
  treatmentId?: number,
  active?: boolean,
  isVeteran?: boolean,
  getPeople?: boolean,
  getHospital?: boolean,
  getTreatments?: boolean,
  globalFilter?: boolean,

  page?: iPage,
  orderBy?: iOrderBy,
}

export interface iPatientObject {
  id: number,
  peopleId: number,
  people?: iPeopleObject,
  hospitalId: number,
  hospital?: iHospitalObject,
  treatment?: iTreatmentObject[],
  socioeconomicRecord: boolean,
  term: boolean,
}

export interface iPatient {
  peopleId: number,
  hospitalId: number,
  socioeconomicRecord: boolean,
  term: boolean,
}

export interface iaddTreatmentToPatient {
  patientId: number,
  treatmentId: number;
}


export interface iPatientTypes {
  Patient: iPatientObject,
  setPatient: React.Dispatch<React.SetStateAction<iPatientObject>>
  viewPatient(page: number, filter: string, textFilter: string): Promise<iPatientObject[] | Error>;
  registerPatient(data: iPatient): Promise<iPatientObject | Error>;
  updatePatient(data: iPatientObject): Promise<iPatientObject | Error>;
  countPatient(): Promise<number>;

  addTreatmentToPatient(id_: number): Promise<iaddTreatmentToPatient | Error>;
  deletePatientTreatmentFromPatient(data: iaddTreatmentToPatient): Promise<iaddTreatmentToPatient | Error>;

  //PatientTreatment
  patientTreatment(): Promise<iaddTreatmentToPatient[] | Error>
  patientTreatmentCount(): Promise<number>

  getPatients(filter: iPatientFilter): Promise<iPatientObject[]>;
  getPatientById(id: number): Promise<iPatientObject>;
  getCount(): Promise<number>;
  postPatient(data: iPatient): Promise<iPatientObject>;
  putPatient(data: iPatientObject): Promise<iPatientObject>;
}