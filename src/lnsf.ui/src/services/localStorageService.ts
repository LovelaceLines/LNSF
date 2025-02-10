/**
 * Gera uma chave padronizada
 *
 * @param key - A chave a ser formatada.
 * @returns A chave formatada.
 */
export const getKey = (key: string): string => `@LNSF:${key}`;

/**
 * Retorna o valor armazenado no localstorage para a chave especificada.
 *
 * @param key - A chave do valor a ser retornado.
 * @param defaultValue - O valor padrão a ser retornado caso a chave não exista.
 * @returns O valor armazenado no localstorage, ou o valor padrão caso a chave não exista.
 */
export const getStorageValue = (key: string, defaultValue?: unknown) => {
  const item = localStorage.getItem(getKey(key));
  return item ? JSON.parse(item) : defaultValue || null;
};

/**
 * Armazena o valor especificado no localstorage para a chave especificada.
 *
 * @param key - A chave do valor a ser armazenado.
 * @param value - O valor a ser armazenado.
 * @returns O valor armazenado.
 */
export const setStorageValue = (key: string, value: unknown) => {
  localStorage.setItem(getKey(key), JSON.stringify(value));
  return value;
};
