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
      initOpen: false,
      id: "test-modal",
    };
    return render(
      <Modal {...defaultProps} {...props}>
        {props.children ? props.children : defaultProps.children}
      </Modal>
    );
  };

  it("deve renderizar o modal", () => {
    const { getByText } = setup({ initOpen: true });
    const expectedText = getByText("Modal Content");
    console.log("expectedText", expectedText);
    expect(expectedText).toBeInTheDocument();
  });

  it("deve chamar handleModalClose quando o botão de fechar é clicado", () => {
    const { getByRole } = setup({ initOpen: true });
    const closeButton = getByRole("button");
    fireEvent.click(closeButton);
    expect(mockHandleModalClose).toHaveBeenCalledWith("test-modal");
  });

  it("deve fechar o modal quando handleClose é chamado", () => {
    mockIsOpen.mockReturnValue(true);
    const { getByRole } = setup();
    const closeButton = getByRole("button");
    fireEvent.click(closeButton);
    expect(mockHandleModalClose).toHaveBeenCalledWith("test-modal");
  });
});
