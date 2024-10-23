/**
 * Atualiza os valores de um objeto iterando sobre suas chaves e aplicando uma função de definição.
 *
 * @template T - Um tipo genérico que estende um registro com chaves de string e valores de qualquer tipo.
 * @param value - O objeto cujos valores precisam ser atualizados.
 * @param setValue - Uma função que define o valor para uma chave dada no objeto.
 * @example
 * ```typescript
 * const obj = { a: 1, b: 2 };
 * toValue(obj, (key, value) => {
 *   console.log(`Setting ${key} to ${value}`);
 * });
 * // Output:
 * // Setting a to 1
 * // Setting b to 2
 * ```
 */
export const toValue = <T extends Record<string, any>>(value: T, setValue: (key: keyof T, value: T[keyof T]) => void) => {
	Object.keys(value).forEach((key) => setValue(key as keyof T, value[key as keyof T]));
};
