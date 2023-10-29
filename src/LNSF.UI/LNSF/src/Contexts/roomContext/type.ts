export interface iRoomProvider {
    children: React.ReactNode
}

export interface iRoomObject {
    id: number,
    number: string,
    bathroom: boolean,
    beds: number,
    occupation: number,
    storey: number,
    available: boolean
}
export interface iRoomRegister {
    number: string,
    bathroom: boolean,
    beds: number,
    occupation: number,
    storey: number,
    available: boolean
}

export interface iRoomTypes {
    room: iRoomObject,
    setRoom: React.Dispatch<React.SetStateAction<iRoomObject>>
    viewRoom (page: number, filter: string, textFilter: string, qntLine?: number): Promise<iRoomObject[] | Error>;
    registerRoom(data: iRoomRegister): Promise<iRoomObject | Error>;
    updateRoom(data: iRoomObject): Promise<iRoomObject | Error>;
    returnQuantity(): Promise<number>;
}