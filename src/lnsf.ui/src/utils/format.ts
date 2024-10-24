export const formatCurrency = (value: number) => {
	return new Intl.NumberFormat("pt-BR", { style: "currency", currency: "BRL" }).format(value);
};

export const formatPercent = (value: number) => {
	return new Intl.NumberFormat("pt-BR", { style: "percent", minimumFractionDigits: 2 }).format(value);
};

export const dateOnlyToStr = (value?: string | Date, format: "ISO" | "ptBr" = "ISO"): string => {
	if (!value) return "";

	const d = typeof value === "string" ? new Date(value) : value;

	const day = String(d.getUTCDate()).padStart(2, "0");
	const month = String(d.getUTCMonth() + 1).padStart(2, "0");
	const year = d.getUTCFullYear();

	return format === "ISO" ? `${year}-${month}-${day}` : `${day}/${month}/${year}`;
};

export const dateTimeToStr = (value?: string | Date, format: "ISO" | "ptBr" = "ISO") => {
	if (!value) return "";

	const d = typeof value === "string" ? new Date(value) : value;

	const day = String(d.getUTCDate()).padStart(2, "0");
	const month = String(d.getUTCMonth() + 1).padStart(2, "0");
	const year = d.getUTCFullYear();

	const hours = String(d.getHours()).padStart(2, "0");
	const minutes = String(d.getUTCMinutes()).padStart(2, "0");
	const seconds = String(d.getUTCSeconds()).padStart(2, "0");

	return format === "ISO"
		? `${year}-${month}-${day}T${hours}:${minutes}:${seconds}`
		: `${day}/${month}/${year} ${hours}:${minutes}:${seconds}`;
};
