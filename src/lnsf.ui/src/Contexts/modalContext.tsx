import { createContext, useContext, useState } from "react";

export type key = "default" | "download-export-display";

interface IModalContextProps {
	handleModalClose: (key: key) => void;
	handleModalOpen: (key: key) => void;
	isOpen: (key: key) => boolean;
}

export const ModalContext = createContext({} as IModalContextProps);

export const ModalProvider = ({ children }: Readonly<{ children: React.ReactNode }>) => {
	const [open, setOpen] = useState<{ [key: string]: boolean }>({});

	const handleModalClose = (key: key) => setOpen({ ...open, [key]: false });

	const handleModalOpen = (key: key) => setOpen({ ...open, [key]: true });

	const isOpen = (key: key) => open[key] ?? false;

	return <ModalContext.Provider value={{ handleModalClose, handleModalOpen, isOpen }}>{children}</ModalContext.Provider>;
};

export const useModal = (id: key): { isOpen: boolean; handleModalClose: () => void; handleModalOpen: () => void } => {
	const { handleModalClose, handleModalOpen, isOpen } = useContext(ModalContext);
	return { isOpen: isOpen(id), handleModalClose: () => handleModalClose(id), handleModalOpen: () => handleModalOpen(id) };
};
