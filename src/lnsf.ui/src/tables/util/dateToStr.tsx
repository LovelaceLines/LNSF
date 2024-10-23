export const dateTimeToStr = (value?: string | Date): string => {
	if (!value) return "";

	if (value instanceof Date) return value.toLocaleString();

	value = new Date(value);
	return value.toLocaleString();
};

export const dateOnlyToStr = (value?: string | Date): string => {
	if (!value) return "";

	if (value instanceof Date) return value.toLocaleDateString();

	value = new Date(value);
	return value.toLocaleDateString();
};

export const timeOnlyToStr = (value?: string | Date): string => {
	if (!value) return "";

	if (value instanceof Date) return value.toLocaleTimeString();

	value = new Date(value);
	return value.toLocaleTimeString();
};
