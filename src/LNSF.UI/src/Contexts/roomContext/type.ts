import { iOrderBy, iPage } from "../types";

export interface iRoomProvider {
    children: React.ReactNode
}

export interface iRoomFilter {
    id?: number,
    number?: string,
    bathroom?: boolean,
    beds?: number,
    storey?: number,
    available?: boolean,
    page?: iPage,
    orderBy?: iOrderBy,
}

export interface iRoomObject {
    id: number,
    number: string,
    bathroom: boolean,
    beds: number,
    storey: number,
    available: boolean
}
export interface iRoomRegister {
    number: string,
    bathroom: boolean,
    beds: number,
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

    getRooms(filter: iRoomFilter): Promise<iRoomObject[]>;
    getRoomById(id: number): Promise<iRoomObject>;
    getCount(): Promise<number>;
    postRoom(data: iRoomRegister): Promise<iRoomObject>;
    putRoom(data: iRoomObject): Promise<iRoomObject>;
}