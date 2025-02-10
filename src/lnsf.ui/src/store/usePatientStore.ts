import { create } from "zustand";

import { patient, patientFilter, patientTreatment, queryResult } from "@/types";
import { Axios } from "@/http";

type state = {
  patients: patient[];
  queryResult: queryResult<patient>;

  getPatient: (id: string) => Promise<patient>;
  getPatients: (filters?: patientFilter) => Promise<void>;
  postPatient: (patient: patient) => Promise<patient>;
  putPatient: (patient: patient) => Promise<patient>;

  addTreatmentToPatient: (patientTreatment: patientTreatment) => Promise<patientTreatment>;
  removeTreatmentFromPatient: (patientTreatment: patientTreatment) => Promise<patientTreatment>;
};

export const usePatientStore = create<state>((set) => ({
  patients: [] as patient[],
  queryResult: {} as queryResult<patient>,

  getPatient: async (id) => {
    const res = await Axios.get<queryResult<patient>>("/Patient", { params: { id: id } });
    return res.data.items[0];
  },

  getPatients: async (filters?: patientFilter) => {
    const res = await Axios.get<queryResult<patient>>("/Patient", { params: filters });
    set({ patients: res.data.items });
    set({ queryResult: res.data });
  },

  postPatient: async (patient) => {
    const res = await Axios.post<patient>("/Patient", patient);
    set((state) => ({ patients: [...state.patients, res.data] }));
    set((state) => ({
      queryResult: { ...state.queryResult, totalCount: state.queryResult.totalCount + 1 },
    }));
    return res.data;
  },

  putPatient: async (patient) => {
    const res = await Axios.put<patient>("/Patient", patient);
    set((state) => ({ patients: state.patients.map((p) => (p.id === patient.id ? res.data : p)) }));
    return res.data;
  },

  addTreatmentToPatient: async (patientTreatment: patientTreatment): Promise<patientTreatment> => {
    const res = await Axios.post<patientTreatment>("/Patient/add-treatment-to-patient", patientTreatment);
    return res.data;
  },

  removeTreatmentFromPatient: async (patientTreatment: patientTreatment): Promise<patientTreatment> => {
    const res = await Axios.delete<patientTreatment>("/Patient/remove-treatment-from-patient", {
      data: patientTreatment,
    });
    return res.data;
  },
}));
