export interface iTourProvider {
    children: React.ReactNode
}

export interface iTourObject {
    id: number,
    output: Date,
    input: Date,
    note: string,
    peopleId: number,  
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


export interface iTourTypes {
    tour: iTourObject,
    setTour: React.Dispatch<React.SetStateAction<iTourObject>>
    viewTour (page: number, filter: string, textFilter: string, qntLine?: number): Promise<iTourObject[] | Error>;
    viewTourOutput (input: boolean): Promise<iTourObject[] | Error>;
    registerTour(data: iTourRegister): Promise<iTourObject | Error>;
    updateTour(data: iTourUpdate): Promise<iTourObject | Error>;
    returnQuantity(): Promise<number>;
    updateAllTour(data: iTourObject): Promise<iTourObject | Error>;
}