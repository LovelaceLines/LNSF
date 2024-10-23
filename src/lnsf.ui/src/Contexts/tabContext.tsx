import { createContext, useContext, useState } from "react";

interface ITabContextProps {
  tabValue: number;
  tabHandleChange: (event: React.SyntheticEvent, newValue: number) => void;
}

export const TabContext = createContext({} as ITabContextProps);

export const TabProvider = ({ children }: Readonly<{ children: React.ReactNode }>) => {
  const [tabValue, setTabValue] = useState(0);
  const tabHandleChange = (event: React.SyntheticEvent, newValue: number) => setTabValue(newValue);

  return <TabContext.Provider value={{ tabValue, tabHandleChange }}>{children}</TabContext.Provider>;
};

export const useTab = () => {
  const { ...props } = useContext(TabContext);
  return { ...props };
};
