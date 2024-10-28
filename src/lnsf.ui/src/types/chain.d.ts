export type peopleHosted = {
	count: number;
};

export type chainCountPeopleHostedFilter = {
	date: string; // dateonly
};

export type chainDayFilter = {
	days: number;
};

export type chainIntervalCheckFilter = {
	checkIn: string; // dateonly
	checkOut: string; // dateonly
};
