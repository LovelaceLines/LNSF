import { describe, it, expect } from "vitest";

import { formatCurrency, dateOnlyToStr, dateTimeToStr, formatPercent } from "./format";

describe("formatCurrency", () => {
	it("deve formatar número como moeda BRL", () => {
		const value = 1234.56;
		const formattedValue = formatCurrency(value);
		expect(formattedValue).toBe("R$ 1.234,56");
	});

	it("deve formatar zero como moeda BRL", () => {
		const value = 0;
		const formattedValue = formatCurrency(value);
		expect(formattedValue).toBe("R$ 0,00");
	});

	it("deve formatar número negativo como moeda BRL", () => {
		const value = -1234.56;
		const formattedValue = formatCurrency(value);
		expect(formattedValue).toBe("-R$ 1.234,56");
	});

	it("deve formatar número grande como moeda BRL", () => {
		const value = 1234567890.12;
		const formattedValue = formatCurrency(value);
		expect(formattedValue).toBe("R$ 1.234.567.890,12");
	});
});

describe("formatCurrency", () => {
	it("deve formatar número como moeda BRL", () => {
		const value = 1234.56;
		const formattedValue = formatCurrency(value);
		expect(formattedValue).toBe("R$ 1.234,56");
	});

	it("deve formatar zero como moeda BRL", () => {
		const value = 0;
		const formattedValue = formatCurrency(value);
		expect(formattedValue).toBe("R$ 0,00");
	});

	it("deve formatar número negativo como moeda BRL", () => {
		const value = -1234.56;
		const formattedValue = formatCurrency(value);
		expect(formattedValue).toBe("-R$ 1.234,56");
	});

	it("deve formatar número grande como moeda BRL", () => {
		const value = 1234567890.12;
		const formattedValue = formatCurrency(value);
		expect(formattedValue).toBe("R$ 1.234.567.890,12");
	});
});

describe("formatPercent", () => {
	it("deve formatar número como percentual", () => {
		const value = 0.1234;
		const formattedValue = formatPercent(value);
		expect(formattedValue).toBe("12,34%");
	});

	it("deve formatar zero como percentual", () => {
		const value = 0;
		const formattedValue = formatPercent(value);
		expect(formattedValue).toBe("0,00%");
	});

	it("deve formatar número negativo como percentual", () => {
		const value = -0.1234;
		const formattedValue = formatPercent(value);
		expect(formattedValue).toBe("-12,34%");
	});

	it("deve formatar número grande como percentual", () => {
		const value = 1234.56;
		const formattedValue = formatPercent(value);
		expect(formattedValue).toBe("123.456,00%");
	});
});

describe("dateOnlyToStr", () => {
	it("deve formatar data no formato ISO", () => {
		const value = "2023-10-05";
		const formattedValue = dateOnlyToStr(value, "ISO");
		expect(formattedValue).toBe("2023-10-05");
	});

	it("deve formatar data no formato ptBr", () => {
		const value = "2023-10-05";
		const formattedValue = dateOnlyToStr(value, "ptBr");
		expect(formattedValue).toBe("05/10/2023");
	});

	it("deve retornar string vazia se valor for undefined", () => {
		const formattedValue = dateOnlyToStr(undefined, "ISO");
		expect(formattedValue).toBe("");
	});

	it("deve formatar objeto Date no formato ISO", () => {
		const value = new Date(Date.UTC(2023, 9, 5));
		const formattedValue = dateOnlyToStr(value, "ISO");
		expect(formattedValue).toBe("2023-10-05");
	});

	it("deve formatar objeto Date no formato ptBr", () => {
		const value = new Date(Date.UTC(2023, 9, 5));
		const formattedValue = dateOnlyToStr(value, "ptBr");
		expect(formattedValue).toBe("05/10/2023");
	});
});

describe("dateTimeToStr", () => {
	it("deve formatar data e hora no formato ISO", () => {
		const value = "2023-10-05T14:30:00";
		const formattedValue = dateTimeToStr(value, "ISO");
		expect(formattedValue).toBe("2023-10-05T14:30:00");
	});

	it("deve formatar data e hora no formato ptBr", () => {
		const value = "2023-10-05T14:30:00";
		const formattedValue = dateTimeToStr(value, "ptBr");
		expect(formattedValue).toBe("05/10/2023 14:30:00");
	});

	it("deve retornar string vazia se valor for undefined", () => {
		const formattedValue = dateTimeToStr(undefined, "ISO");
		expect(formattedValue).toBe("");
	});

	it("deve formatar objeto Date no formato ISO", () => {
		const value = new Date(2023, 9, 5, 14, 30, 0);
		const formattedValue = dateTimeToStr(value, "ISO");
		expect(formattedValue).toBe("2023-10-05T14:30:00");
	});

	it("deve formatar objeto Date no formato ptBr", () => {
		const value = new Date(2023, 9, 5, 14, 30, 0);
		const formattedValue = dateTimeToStr(value, "ptBr");
		expect(formattedValue).toBe("05/10/2023 14:30:00");
	});
});
