import { describe, expect, it } from "vitest";

import { includes } from "./arrayUtil";

describe("includes", () => {
	it("deve retornar true se todos os elementos de busca estiverem incluídos no array", () => {
		const array = [1, 2, 3, 4, 5];
		const searchElements = [2, 4];
		const result = includes(array, searchElements);
		expect(result).toBe(true);
	});

	it("deve retornar true se pelo menos um elemento de busca estiver incluído no array", () => {
		const array = [1, 2, 3, 4, 5];
		const searchElements = [2, 6];
		const result = includes(array, searchElements);
		expect(result).toBe(true);
	});

	it("deve retornar false se nenhum elemento de busca estiver incluído no array", () => {
		const array: number[] = [];
		const searchElements: number[] = [];
		const result = includes(array, searchElements);
		expect(result).toBe(false);
	});

	it("deve retornar false se pelo menos um elemento de busca não estiver incluído no array", () => {
		const array: number[] = [];
		const searchElements = [1, 2, 3];
		const result = includes(array, searchElements);
		expect(result).toBe(false);
	});

	it("deve retornar false se todos os elementos de busca não estiverem incluídos no array", () => {
		const array = [1, 2, 3, 4, 5];
		const searchElements: number[] = [];
		const result = includes(array, searchElements);
		expect(result).toBe(false);
	});
});
