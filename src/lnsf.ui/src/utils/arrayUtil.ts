/**
 * Determina se um array inclui algum dos elementos de busca.
 * @param searchElements Os elementos a serem pesquisados no array.
 */
export const includes = <T>(array: T[], searchElements: T[]): boolean =>
  searchElements.some((searchElement) => array.includes(searchElement));
