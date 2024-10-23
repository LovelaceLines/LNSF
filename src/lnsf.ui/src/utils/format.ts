export const formatCurrency = (value: number) => {
	return new Intl.NumberFormat("pt-BR", { style: "currency", currency: "BRL" }).format(value);
};

export const formatPercent = (value: number) => {
	return new Intl.NumberFormat("pt-BR", { style: "percent", minimumFractionDigits: 2 }).format(value);
};

export const formatDate = (value?: string | Date) => {
	if (!value) return undefined;

	if (value instanceof Date) return value.toISOString().split("T")[0];

	const regexDotNet = /^\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}(\.\d{0,7})?$/;
	if (regexDotNet.test(value)) {
		return value.split(" ")[0];
	}

	const date = new Date(value);

	return date.toISOString().split("T")[0];
};

export const formatDateTime = (value?: string | Date) => {
	if (!value) return undefined;

	if (value instanceof Date) return value.toISOString().split(".")[0];

	const regexDotNet = /^\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}(\.\d{0,7})?$/;
	if (regexDotNet.test(value)) {
		value = value.split(".")[0];
		return value.replace(" ", "T");
	}

	const date = new Date(value);
	return date.toISOString().split(".")[0].replace(" ", "T");
};
