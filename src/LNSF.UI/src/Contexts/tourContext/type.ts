import { iPeopleObject } from "..";
import { iOrderBy, iPage } from "../types";

export interface iTourProvider {
    children: React.ReactNode
}

export interface iTourObject {
    id: number,
    output: Date,
    input: Date,
    note: string,
    peopleId: number,
    people: iPeopleObject | null,
}

export interface iAttObject {
 
    output: Date,
    input: Date,
    note: string,
    peopleId: number,  
}

export interface iTourPeopleRoom {
    id: number,
    output: Date,
    input: Date,
    note: string,
    peopleId: number, 
    name: string,
    number: string, 
}
export interface iTourRegister {
    note: string,
    peopleId: number, 
}

export interface iTourUpdate {
    id: number,
    note: string,
    peopleId: number, 
}

export interface iTourFilter {
    id?: number,
    output?: Date, 
    input?: Date, 
    note?: string, 
    inOpen?: boolean, 
    peopleId?: number, 
    getPeople?: boolean, 
    peopleName?: string,
    peopleRG?: string,
    peopleCPF?: string,
    globalFilter?: string,
    page?: iPage,
    orderBy?: iOrderBy,
}

export interface iTourTypes {
    getTours(filter?: iTourFilter): Promise<iTourObject[]>;
    getTourById(data: { id: number, getPeople: true }): Promise<iTourObject>;
    getCount(): Promise<number>;
    postTour(data: iTourRegister): Promise<iTourObject>;
    putTour(data: iTourUpdate): Promise<iTourObject>;
    putAllTour(data: iTourObject): Promise<iTourObject>;
}