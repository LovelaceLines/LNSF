export const formatCurrency = (value: number) => {
  return new Intl.NumberFormat("pt-BR", { style: "currency", currency: "BRL" }).format(value);
};

export const formatPercent = (value: number) => {
  return new Intl.NumberFormat("pt-BR", { style: "percent", minimumFractionDigits: 2 }).format(value);
};

export const dateOnlyToStr = (value?: string, format: "ISO" | "ptBr" | ".Net" = "ISO"): string => {
  if (!value) return "";

  // yyyy-MM-dd or yyyy/MM/dd
  const regex = /^\d{4}-\d{2}-\d{2}$/.test(value) || /^\d{2}\/\d{2}\/\d{4}$/.test(value);
  if (!regex) throw new Error("Invalid date format: " + value);

  const year = value.slice(0, 4);
  const month = value.slice(5, 7);
  const day = value.slice(8, 10);

  return format === "ISO"
    ? `${year}-${month}-${day}`
    : format === ".Net"
    ? `${year}-${month}-${day}`
    : `${day}/${month}/${year}`;
};

export const strIsDateOnly = (value: string): boolean => {
  return /^\d{4}-\d{2}-\d{2}$/.test(value) || /^\d{2}\/\d{2}\/\d{4}$/.test(value);
};

export const dateTimeToStr = (value?: string, format: "ISO" | "ptBr" | ".Net" = "ISO") => {
  if (!value) return "";

  // yyyy-MM-ddTHH:mm:ss.SSSSSSS*
  let regex = /^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}(:\d{2}(.\d{1,7})?)?.*$/;
  if (regex.test(value)) value = value.replace("T", " ");

  // yyyy-MM-dd HH:mm:ss.SSSSSSS*
  regex = /^\d{4}-\d{2}-\d{2} \d{2}:\d{2}(:\d{2}(.\d{1,7})?)?.*$/;
  if (!regex.test(value)) throw new Error("Invalid datetime format: " + value);

  const year = value.slice(0, 4);
  const month = value.slice(5, 7);
  const day = value.slice(8, 10);

  const hours = value.slice(11, 13);
  const minutes = value.slice(14, 16);
  const seconds = value.slice(17, 19);

  let result =
    format === "ISO"
      ? `${year}-${month}-${day}T${hours}:${minutes}`
      : format === ".Net"
      ? `${year}-${month}-${day} ${hours}:${minutes}`
      : `${day}/${month}/${year} ${hours}:${minutes}`;

  result = seconds ? `${result}:${seconds}` : result;

  return result;
};

export const strIsDateTime = (value: string): boolean => {
  return (
    /^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}(:\d{2}(.\d{1,7})?)?.*$/.test(value) ||
    /^\d{2}\/\d{2}\/\d{4}T\d{2}:\d{2}(:\d{2}(.\d{1,7})?)?.*$/.test(value) ||
    /^\d{4}-\d{2}-\d{2} \d{2}:\d{2}(:\d{2}(.\d{1,7})?)?.*$/.test(value) ||
    /^\d{2}\/\d{2}\/\d{4} \d{2}:\d{2}(:\d{2}(.\d{1,7})?)?.*$/.test(value)
  );
};
