import { iPatientObject } from "../patientContext/type";
import { iOrderBy, iPage } from "../types";

export interface iFamilyGroupProfileFilter {
  id?: number;
  name?: string;
  kinship?: string;
  age?: number;
  profession?: string;
  income?: number;
  globalFilter?: string;
  getPatient?: boolean;
  page?: iPage;
  orderBy?: iOrderBy;
}

export interface iFamilyGroupProfileObject {
  id: number;
  patientId: number;
  patient: iPatientObject;
  name: string;
  kinship: string;
  age: number;
  profession: string;
  income: number;
}

export interface iFamilyGroupProfilePost {
  patientId: number;
  name: string;
  kinship: string;
  age: number;
  profession: string;
  income: number;
}

export interface iFamilyGroupProfileTypes {
  getFamilyGroupProfiles(filter: iFamilyGroupProfileFilter): Promise<iFamilyGroupProfileObject[]>;
  getFamilyGroupProfileById(data: { id: number, getPatient: boolean }): Promise<iFamilyGroupProfileObject>;
  getCountFamilyGroupProfile(): Promise<number>;
  postFamilyGroupProfile(data: iFamilyGroupProfilePost): Promise<iFamilyGroupProfileObject>;
  putFamilyGroupProfile(data: iFamilyGroupProfileObject): Promise<iFamilyGroupProfileObject>;
  deleteFamilyGroupProfile(id: number): Promise<iFamilyGroupProfileObject>;
}