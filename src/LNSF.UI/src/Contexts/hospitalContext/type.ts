import { iOrderBy, iPage } from "../types";

export interface iHospitalProvider {
    children: React.ReactNode
}

export interface iHospitalObject {
    id: number,
    name: string,
    acronym: string,
}

export interface iHospital {
    name: string,
    acronym: string,
}

export interface iHospitalFilter {
    id?: number,
    name?: string,
    acronym?: string,
    globalFilter?: string,
    page?: iPage,
    orderBy?: iOrderBy
}

export interface iHospitalTypes {
    getHospitals(filter?: iHospitalFilter): Promise<iHospitalObject[]>;
    getHospitalById(id: number): Promise<iHospitalObject>;
    getCount(): Promise<number>;
    postHospital(data: iHospital): Promise<iHospitalObject>;
    putHospital(data: iHospitalObject): Promise<iHospitalObject>;
    deleteHospitalById(id: string): Promise<iHospitalObject>
}