import { createContext, useCallback, useState, useContext } from "react";


interface IAppDrawerProviderProps {
    children: React.ReactNode;
}
interface IDrawerOption {
    index: number;
    icon: string;
    path: string;
    label: string;
    options: IDrawerListOption[];
}
interface IDrawerListOption {
    pathOption: string;
    labelOption: string;
}
interface IDrawerContextData {
    isDrawerOpen: boolean;
    setIsDrawerOpen: (newIsDrawerOpen: boolean) => void;
    toggleDrawerOpen: () => void;

    drawerOptions: IDrawerOption[];
    setDrawerOptions: (newDrawerOptions: IDrawerOption[]) => void;
}

export const DrawerContext = createContext({} as IDrawerContextData);

export const useDrawerContext = () => {
    return useContext(DrawerContext);
}

export const DrawerProvider: React.FC<IAppDrawerProviderProps> = ({children}) => {

    const [isDrawerOpen, setIsDrawerOpen] = useState(false);
    const [drawerOptions, setDrawerOptions] = useState<IDrawerOption[]>([]);

    const toggleDrawerOpen = useCallback(() => {
        setIsDrawerOpen(oldDrawerOpen => !oldDrawerOpen);
    }, []);

    const handleSetDrawerOptions = useCallback((newDrawerOptions: IDrawerOption[]) => {
        setDrawerOptions(newDrawerOptions);
    }, []);

    return (
        <DrawerContext.Provider value={{ isDrawerOpen, setIsDrawerOpen, toggleDrawerOpen, drawerOptions, setDrawerOptions: handleSetDrawerOptions }}>
            {children}
        </DrawerContext.Provider>
    );
};