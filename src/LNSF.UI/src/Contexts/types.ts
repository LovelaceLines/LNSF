export interface iPage {
    page: number | null,
    pageSize: number | null,
}

export enum iOrderBy {
    ascendent = 0,
    descendent = 1
}