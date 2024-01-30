import { iEmergencyContactObject, iTourObject } from ".."
import { iOrderBy, iPage } from "../types"

export interface iPeopleProvider {
    children: React.ReactNode
}

export interface iPeopleFilter {
    id?: number,
    name?: string,
    rg?: string,
    issuingBody?: string,
    cpf?: string,
    phone?: string,
    gender?: Gender,
    birthDate?: Date,
    city?: string,
    state?: string,
    neighborhood?: string,
    street?: string,
    houseNumber?: string,
    note?: string,
    patient?: boolean,
    escort?: boolean,
    active?: boolean,
    veteran?: boolean,
    globalFilter?: string,
    page?: iPage,
    orderBy?: iOrderBy,
}

export enum Gender {
    male = 0,
    female = 1,
}

export enum eMaritalStatus {
    single,
    married,
    separate,
    divorced,
    stableUnion,
    widower
}

export enum eRaceColor {
    white, 
    black, 
    brown, 
    yellow, 
    indigenous
}

export interface iPeopleObject {
    id: number,
    name: string,
    gender: number,
    birthDate: Date,
    maritalStatus: eMaritalStatus,
    raceColor: eRaceColor,
    email: string,
    rg: string,
    issuingBody: string,
    cpf: string,
    street: string,
    houseNumber: string,
    neighborhood: string,
    city: string,
    state: string,
    phone: string,
    note: string,
    experience?: string,
    status?: string,
    tours?: iTourObject[],
    contacts?: iEmergencyContactObject[],
}
export interface iAddPeopleRoom {
    peopleId: number,
    roomId: number
}

export interface iAddPeopleRoomDados {
    name: string,
    roomId: number
}

export interface iAddPeopleRoomAutoComplete {
    id: number,
    roomId: number
}
export interface iRemovePeopleRoom {
    peopleId: number
}

export interface iPeopleRegister {
    name: string,
    gender: number,
    birthDate: Date,
    maritalStatus: eMaritalStatus,
    raceColor: eRaceColor,
    email: string,
    rg: string,
    issuingBody: string,
    cpf: string,
    street: string,
    houseNumber: string,
    neighborhood: string,
    city: string,
    state: string,
    phone: string,
    note: string,
}


export interface iAddPeopleToRoom{
    hostingId: number,
    peopleId: number,
    roomId: number,
}

export interface iPeopleTypes {
    people: iPeopleObject,
    setPeople: React.Dispatch<React.SetStateAction<iPeopleObject>>

    getPeoples(filter?: iPeopleFilter): Promise<iPeopleObject[]>;
    getPeopleById(id: number): Promise<iPeopleObject>;
    getCount(): Promise<number>;
    postPeople(data: iPeopleRegister): Promise<iPeopleObject>;
    putPeople(data: iPeopleObject): Promise<iPeopleObject>;
}