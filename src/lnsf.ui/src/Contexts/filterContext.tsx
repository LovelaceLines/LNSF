import { createContext, useContext } from "react";

import { useLocalStorage } from "@/hooks";

interface IfilterContextProps {
  open: boolean;
  toggleOpen: () => void;
}

export const FilterContext = createContext({} as IfilterContextProps);

export const FilterProvider = ({ children }: Readonly<{ children: React.ReactNode }>) => {
  const [open, setOpen] = useLocalStorage("filtersOpen", true);

  const toggleOpen = () => setOpen(!open);

  return <FilterContext.Provider value={{ open, toggleOpen }}>{children}</FilterContext.Provider>;
};

export const useFilter = () => {
  const { ...props } = useContext(FilterContext);
  return { ...props };
};
