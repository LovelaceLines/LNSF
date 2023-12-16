export interface iHostingProvider {
    children: React.ReactNode
}


export interface iHostingObject {
    id: number,
    checkIn: Date,
    checkOut: Date,
    patientId: number,
}

export interface iHostingRegister {
    checkIn: Date,
    checkOut: Date,
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

}