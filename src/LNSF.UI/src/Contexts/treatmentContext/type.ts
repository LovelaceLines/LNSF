import { iOrderBy, iPage } from "../types";

export interface iTreatmentProvider {
    children: React.ReactNode
}

export enum iTypeTreatment {
    cancer = 0,
    pretransplant = 1,
    posttransplant = 2,
    other = 3,
}

export interface iTreatmentFilter {
    id?: number,
    name?: string,
    type?: iTypeTreatment,
    globalFilter?: string,
    page?: iPage,
    orderBy?: iOrderBy
}

export interface iTreatmentPost {
    name: string,
    type: iTypeTreatment,
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
    // treatment: iTreatment[],
    // setTreatment: React.Dispatch<React.SetStateAction<iTreatment[]>>

    getTreatments(filter?: iTreatmentFilter): Promise<iTreatment[]>;
    getTreatmentById(id: number): Promise<iTreatment>;
    getCount(): Promise<number>;
    postTreatment(data: iTreatmentPost): Promise<iTreatment>;
    putTreatment(data: iTreatment): Promise<iTreatment>;
}