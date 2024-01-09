export interface iPage {
    page: number | null,
    pageSize: number | null,
}

export enum iOrderBy {
    ascendent = 1,
    descendent = 0
}