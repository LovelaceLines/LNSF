import { describe, it, expect } from "vitest";

import { formatCurrency, formatDate, formatDateTime, formatPercent } from "./format";

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

		describe("formatDateTime", () => {
			it("deve formatar data e hora no formato correto", () => {
				const value = "2023-10-05 14:30:00.123456";
				const formattedValue = formatDateTime(value);
				expect(formattedValue).toBe("2023-10-05T14:30:00");
			});

			it("deve retornar a string original se não corresponder ao regex", () => {
				const value = "2023-10-05T14:30:00.123456";
				const formattedValue = formatDateTime(value);
				expect(formattedValue).toBe("2023-10-05T14:30:00.123456");
			});

			it("deve retornar a string original se o formato for inválido", () => {
				const value = "invalid-date-time";
				const formattedValue = formatDateTime(value);
				expect(formattedValue).toBe("invalid-date-time");
			});
		});

		describe("formatDate", () => {
			it("deve formatar data no formato correto", () => {
				const value = "2023-10-05 14:30:00.123456";
				const formattedValue = formatDate(value);
				expect(formattedValue).toBe("2023-10-05");
			});

			it("deve retornar a string original se não corresponder ao regex", () => {
				const value = "2023-10-05T14:30:00.123456";
				const formattedValue = formatDate(value);
				expect(formattedValue).toBe("2023-10-05T14:30:00.123456");
			});

			it("deve retornar a string original se o formato for inválido", () => {
				const value = "invalid-date";
				const formattedValue = formatDate(value);
				expect(formattedValue).toBe("invalid-date");
			});
		});
	});
});
