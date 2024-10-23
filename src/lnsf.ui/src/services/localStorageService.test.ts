import { beforeEach, describe, expect, it } from "vitest";

import { getKey, getStorageValue, setStorageValue } from "./localStorageService";

describe("localStorageService", () => {
	beforeEach(() => {
		localStorage.clear();
	});

	it("deve gerar uma chave padronizada", () => {
		const key = "testKey";
		const formattedKey = getKey(key);
		expect(formattedKey).toBe("@LNSF:testKey");
	});

	it("deve retornar o valor armazenado para a chave especificada", () => {
		const key = "testKey";
		const value = "testValue";
		localStorage.setItem("@LNSF:testKey", JSON.stringify(value));

		const storedValue = getStorageValue(key);
		expect(storedValue).toBe(value);
	});

	it("deve retornar o valor padrão se a chave não existir", () => {
		const key = "nonExistentKey";
		const defaultValue = "defaultValue";

		const storedValue = getStorageValue(key, defaultValue);
		expect(storedValue).toBe(defaultValue);
	});

	it("deve armazenar o valor especificado para a chave especificada", () => {
		const key = "testKey";
		const value = "testValue";

		setStorageValue(key, value);

		const storedValue = localStorage.getItem("@LNSF:testKey");
		expect(storedValue).toBe(JSON.stringify(value));
	});
});
