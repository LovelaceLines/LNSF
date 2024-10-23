export const createQueryParams = <T extends Record<string, any>>(obj: T): string => {
  const params = new URLSearchParams();

  Object.keys(obj).forEach((key) => {
    const value = obj[key];
    if (value !== undefined && value !== null) {
      if (Array.isArray(value)) {
        value.forEach((item) => params.append(key, item));
      } else {
        params.append(key, value);
      }
    }
  });

  return params.toString();
};

export const parseQueryParams = <T extends Record<string, any>>(search: string): T => {
  const params = new URLSearchParams(search);
  const obj: Record<string, any> = {};

  params.forEach((value, key) => {
    if (obj[key] === undefined) {
      obj[key] = value;
    } else if (Array.isArray(obj[key])) {
      obj[key].push(value);
    } else {
      obj[key] = [obj[key], value];
    }
  });

  return obj as T;
};
