import { iPeopleObject, iRoomObject } from "..";
import { iHostingObject } from "../hostingContext/type";
import { iOrderBy, iPage } from "../types";

export interface iPeopleRoomHostingFilter {
  peopleId?: number;
  roomId?: number;
  hostingId?: number;
  hasVacancy?: boolean;
  available?: boolean;
  checkIn?: Date;
  checkOut?: Date;
  globalFilter?: string;
  getPeople?: boolean;
  getRoom?: boolean;
  getHosting?: boolean;

  page?: iPage;
  orderBy?: iOrderBy;
}

export interface iPeopleRoomHostingObject {
  peopleId: number;
  people: iPeopleObject;
  roomId: number;
  room: iRoomObject;
  hostingId: number;
  hosting: iHostingObject;
}

export interface iPeopleRoomHostingTypes {
  getPeoplesRoomsHostings(filter?: iPeopleRoomHostingFilter): Promise<iPeopleRoomHostingObject[]>;
  getPeopleRoomHostingById({ peopleId, roomId, hostingId }: iPeopleRoomHostingFilter): Promise<iPeopleRoomHostingObject>;
  getCount(): Promise<number>;
}