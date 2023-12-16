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


export interface iAddPeopleToRoom{
    hostingId: number,
    peopleId: number,
    roomId: number,
}

export interface iPeopleTypes {
    people: iPeopleObject,
    setPeople: React.Dispatch<React.SetStateAction<iPeopleObject>>
    viewPeople (page: number, filter: string, textFilter: string): Promise<iPeopleObject[] | Error>;
    registerPeople(data: iPeopleRegister): Promise<iPeopleObject | Error>;
    registerPeopleToRoom(data: iAddPeopleToRoom): Promise<iAddPeopleToRoom | Error>;
    updatePeople(data: iPeopleObject): Promise<iPeopleObject | Error>;
    addPeopleRoom(data: iAddPeopleRoom): Promise<iPeopleObject | Error>;
    removePeopleRoom(data: iAddPeopleToRoom): Promise<iAddPeopleToRoom | Error>;
    returnQuantity(): Promise<number>;

    viewPeopleRoom(page: number): Promise<iAddPeopleToRoom[] | Error>;
    returnQuantityPeople(): Promise<number>;
}