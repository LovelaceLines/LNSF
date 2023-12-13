export interface iPatientProvider {
    children: React.ReactNode
}

export interface iPatientObject {
    id: number,
    peopleId: number,
    hospitalId: number,
    socioeconomicRecord: boolean,
    term: boolean,
    treatmentIds: number[];

}

export interface iPatient {
    peopleId: number,
    hospitalId: number,
    socioeconomicRecord: boolean,
    term: boolean,
    treatmentIds: number[];
    
}

export interface iform {
    hospitalId: number,
    type: number;
    name: string;
}


export interface iPatientTypes {
    Patient: iPatientObject,
    setPatient: React.Dispatch<React.SetStateAction<iPatientObject>>
    viewPatient(page: number, filter: string, textFilter: string): Promise<iPatientObject[] | Error>;
    registerPatient(data: iPatient): Promise<iPatientObject | Error>;
    updatePatient(data: iPatientObject): Promise<iPatientObject | Error>;
    countPatient(): Promise<number>;
}