export interface iTreatmentProvider {
    children: React.ReactNode
}

export interface iTreatmentObject {
    id?: number,
    name?: string,
    type?: number,
}

export interface iTreatment {
    id: number,
    name: string,
    type: number,
}

// tipo:
// CANCER, 0
// PRETRANSPLANT, 1
// POSTTRANSPLANT, 2
// OTHER, 3

export interface iTreatmentTypes {
    treatment: iTreatment[],
    setTreatment: React.Dispatch<React.SetStateAction<iTreatment[]>>
    viewTreatment(page: number, filter: string, textFilter: string): Promise<iTreatment[] | Error>;
    registerTreatment(data: iTreatmentObject): Promise<iTreatment | Error>;
    updateTreatment(data: iTreatmentObject): Promise<iTreatmentObject | Error>;
    countTreatment(): Promise<number>;
    deleteTreatment(data: string): Promise<iTreatment | Error>;
}