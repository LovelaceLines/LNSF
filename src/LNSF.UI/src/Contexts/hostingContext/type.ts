import { iEscortObject } from "../escortContext/type";
import { iPatientObject } from "../patientContext/type";
import { iOrderBy, iPage } from "../types";

export interface iHostingProvider {
    children: React.ReactNode
}

export interface iHostingFilter {
    id?: number,
    checkIn?: Date,
    checkOut?: Date,
    patientId?: number,
    getPatient?: boolean,
    getPatientPeople?: boolean,
    escortId?: number,
    getEscort?: boolean,
    getEscortPeople?: boolean,
    active?: boolean,
    globalFilter?: string,
    page?: iPage,
    orderBy?: iOrderBy,
}

export interface iHostingObject {
    id: number,
    checkIn: Date,
    checkOut: Date,
    patientId: number,
    patient?: iPatientObject,
    escorts?: iEscortObject[],
}

export interface iHostingRegister {
    checkIn: Date,
    checkOut?: Date,
    patientId: number,
}

export interface iHosting_ {
    hostingId: number,
    escortId: number,
}


export interface iHostingTypes {
    Hosting: iHostingObject,
    setHosting: React.Dispatch<React.SetStateAction<iHostingObject>>
    viewHosting(page: number, filter: string, textFilter: string): Promise<iHostingObject[] | Error>;
    registerHosting(data: iHostingRegister): Promise<iHostingObject | Error>;
    updateHosting(data: iHostingObject): Promise<iHostingObject | Error>;
    countHosting(): Promise<number>;

    registerEscortToFromHosting(data: iHosting_): Promise<iHosting_ | Error>;
    deleteEscortHosting(data: iHosting_): Promise<iHostingObject | Error>


    //HostingEscort
    viewHostingEscort(): Promise<iHosting_[] | Error>;
    countHostingEscort(): Promise<number>;

    getHostings(filter: iHostingFilter): Promise<iHostingObject[]>;
    getHostingById(id: number): Promise<iHostingObject>;
    getCount(): Promise<number>;
    postHosting(data: iHostingRegister): Promise<iHostingObject>;
    putHosting(data: iHostingObject): Promise<iHostingObject>;
}