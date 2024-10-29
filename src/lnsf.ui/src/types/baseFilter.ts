export type baseFilter = {
	page?: number;
	perPage?: number;
	sortBy?: sortOrder;
	sort?: string;
};

export enum sortOrder {
	asc,
	desc,
}

export type range<T> = {
	min?: T;
	max?: T;
};
