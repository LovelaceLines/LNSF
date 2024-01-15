import { iPeopleObject } from "..";

export interface iPatientProvider {
    children: React.ReactNode
}

export interface iPatientObject {
    id: number,
    peopleId: number,
    people?: iPeopleObject,
    hospitalId: number,
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

}