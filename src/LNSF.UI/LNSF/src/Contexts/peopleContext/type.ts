export interface iPeopleProvider {
    children: React.ReactNode
}

export interface iPeopleObject {
    id: number,
    name: string,
    gender: number,
    birthDate: Date,
    rg: string,
    cpf: string,
    street: string,
    houseNumber: string,
    neighborhood: string,
    city: string,
    state: string,
    phone: string,
    note: string,
    roomId: number
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

export interface iPeopleUpdate {
    id: number,
    name: string,
    gender: number,
    birthDate: Date,
    rg: string,
    cpf: string,
    street: string,
    houseNumber: string,
    neighborhood: string,
    city: string,
    state: string,
    phone: string,
    note: string,
}
export interface iPeopleRegister {
    name: string,
    gender: number,
    birthDate: Date,
    rg: string,
    cpf: string,
    street: string,
    houseNumber: string,
    neighborhood: string,
    city: string,
    state: string,
    phone: string,
    note: string,
}



export interface iPeopleTypes {
    people: iPeopleObject,
    setPeople: React.Dispatch<React.SetStateAction<iPeopleObject>>
    viewPeople (page: number, filter: string, textFilter: string): Promise<iPeopleObject[] | Error>;
    registerPeople(data: iPeopleRegister): Promise<iPeopleObject | Error>;
    updatePeople(data: iPeopleUpdate): Promise<iPeopleObject | Error>;
    addPeopleRoom(data: iAddPeopleRoom): Promise<iPeopleObject | Error>;
    removePeopleRoom(data: iRemovePeopleRoom): Promise<iPeopleObject | Error>;
    returnQuantity(): Promise<number>;
}