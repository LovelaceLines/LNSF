import { baseFilter } from "./baseFilter";
import { user } from "./user";

export type log = {
  id: number;
  userId: number;
  user?: user;
  entityName: string;
  entityId: number;
  action: string;
  valuesChanges: string;
  logDateTime: Date;
};

export type logFilter = baseFilter & {
  id?: number;
  userId?: number;
  entityName?: string;
  entityId?: number;
  action?: string;
  valuesChanges?: object;
  logDateTime?: Date;
};
