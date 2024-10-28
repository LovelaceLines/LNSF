import "@testing-library/jest-dom/vitest";
import { render, fireEvent } from "@testing-library/react";
import { describe, it, expect, vi, beforeEach, Mock } from "vitest";

import { useModal } from "@/contexts";
import { Modal, ModalProps } from "./modal";
import { useThemeContext } from "@/theme";

vi.mock("@/contexts", () => ({
	useModal: vi.fn(),
}));

vi.mock("@/theme", () => ({
	useThemeContext: vi.fn(),
	colors: {
		black: "#000000",
		white: "#FFFFFF",
	},
}));

describe("Modal Component", () => {
	const mockHandleModalClose = vi.fn();
	const mockIsOpen = vi.fn();
	const mockUseModal = {
		handleModalClose: mockHandleModalClose,
		isOpen: mockIsOpen,
	};

	const mockUseThemeContext = {
		themeName: "light",
	};

	beforeEach(() => {
		vi.clearAllMocks();
		(useModal as Mock).mockReturnValue(mockUseModal);
		(useThemeContext as Mock).mockReturnValue(mockUseThemeContext);
	});

	const setup = (props: Partial<ModalProps> = {}) => {
		const defaultProps: ModalProps = {
			children: <div>Modal Content</div>,
			id: "default",
		};
		return render(
			<>
				<button id="outside-button">Outside</button>
				<Modal {...defaultProps} {...props}>
					{props.children ? props.children : defaultProps.children}
				</Modal>
			</>
		);
	};

	it("deve renderizar o modal", () => {
		const { getByText } = setup();
		const expectedText = getByText("Modal Content");
		expect(expectedText).toBeInTheDocument();
	});

	it("deve fechar o modal quando o fundo é clicado", () => {
		const { getByRole, getByText } = setup();
		const outsideButton = getByRole("button", { name: "Outside" });
		fireEvent.click(outsideButton);
		const notExpectedText = getByText("Modal Content");
		expect(notExpectedText).not.toBeInTheDocument();
	});

	it("deve fechar o modal quando handleClose é chamado", () => {
		mockIsOpen.mockReturnValue(true);
		const { getByRole } = setup();
		const closeButton = getByRole("button");
		fireEvent.click(closeButton);
		expect(mockHandleModalClose).toHaveBeenCalled();
	});
});
