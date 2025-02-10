import { useState, useEffect } from "react";

import { getKey } from "@/services";

/**
 * Hook customizado para armazenar e recuperar valores do local storage.
 *
 * @param key - A chave a ser usada para armazenar o valor no local storage.
 * @param initialValue - O valor inicial a ser usado caso a chave não exista.
 * @returns Um array contendo o valor armazenado e uma função para atualizá-lo.
 */
export const useLocalStorage = (key: string, initialValue?: unknown): [any, React.Dispatch<any>] => {
  key = getKey(key);

  const storageValue = localStorage.getItem(key);
  const [value, setValue] = useState(() => (storageValue ? JSON.parse(storageValue) : initialValue));

  useEffect(() => {
    localStorage.setItem(key, JSON.stringify(value));
  }, [key, value]);

  return [value, setValue];
};
