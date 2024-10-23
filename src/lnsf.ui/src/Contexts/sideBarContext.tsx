import { createContext, useContext } from "react";

import { useLocalStorage } from "@/hooks";

interface ISideBarContextProps {
  open: boolean;
  toggleSideBar: () => void;
}

export const SideBarContext = createContext({} as ISideBarContextProps);

export const SideBarProvider = ({ children }: Readonly<{ children: React.ReactNode }>) => {
  const [open, setOpen] = useLocalStorage("sideBarOpen", true);

  const toggleSideBar = () => setOpen(!open);

  return <SideBarContext.Provider value={{ open, toggleSideBar }}>{children}</SideBarContext.Provider>;
};

export const useSideBar = () => {
  const { ...props } = useContext(SideBarContext);
  return { ...props };
};
