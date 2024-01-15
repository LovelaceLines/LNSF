import { iPeopleObject } from "..";

export interface iEscortProvider {
    children: React.ReactNode
}

export interface iEscortObject {
    id?: number,
    peopleId?: number,
    people?: iPeopleObject,
}

export interface iEscortTypes {
    Escort: iEscortObject,
    setEscort: React.Dispatch<React.SetStateAction<iEscortObject>>
    viewEscort(page: number, filter: string, textFilter: string): Promise<iEscortObject[] | Error>;
    registerEscort(data: iEscortObject): Promise<iEscortObject | Error>;
    updateEscort(data: iEscortObject): Promise<iEscortObject | Error>;
    deleteEscort(data: iEscortObject): Promise<iEscortObject | Error>;
    countEscort(): Promise<number>;

    getEscortById(filter: { id: number, getPeople: boolean }): Promise<iEscortObject>,
}