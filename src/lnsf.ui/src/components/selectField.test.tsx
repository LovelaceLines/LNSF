import "@testing-library/jest-dom/vitest";
import { fireEvent, render } from "@testing-library/react";
import { describe, it, expect, vi } from "vitest";

import { SelectField, SelectFieldProps } from "./selectField";

describe("SelectField Component", () => {
  const mockOnClick = vi.fn();
  const defaultProps: SelectFieldProps = {
    label: "Test Label",
    labelId: "test-label-id",
    options: [
      { id: "1", name: "Option 1" },
      { id: "2", name: "Option 2" },
      { id: "3", name: "Option 3" },
    ],
    valueKey: "id",
    labelKey: "name",
    onClick: mockOnClick,
  };

  it("deve renderizar normalmente com as props padrão", () => {
    const { getByLabelText } = render(<SelectField {...defaultProps} />);
    const expectedLabel = getByLabelText("Test Label");
    expect(expectedLabel).toBeInTheDocument();
  });

  it("deve renderizar as opções ao clicar no select", () => {
    const { getByText, getByLabelText } = render(<SelectField {...defaultProps} />);
    fireEvent.mouseDown(getByLabelText("Test Label"));
    const expectedOption1 = getByText("Option 1");
    const expectedOption2 = getByText("Option 2");
    expect(expectedOption1).toBeInTheDocument();
    expect(expectedOption2).toBeInTheDocument();
  });

  it("deve chamar a função onClick ao selecionar uma opção", () => {
    const { getByLabelText, getByText } = render(<SelectField {...defaultProps} />);
    fireEvent.mouseDown(getByLabelText("Test Label"));
    fireEvent.click(getByText("Option 1"));
    expect(mockOnClick).toHaveBeenCalledWith("1");
  });

  it("deve exibir o ícone de limpar quando uma opção é selecionada", () => {
    const { getByRole } = render(<SelectField {...defaultProps} defaultValue="1" />);
    const expectedClearButton = getByRole("button");
    expect(expectedClearButton.id).toBe("clear-button");
  });

  it("deve limpar a seleção ao clicar no botão de limpar", () => {
    const { getByRole } = render(<SelectField {...defaultProps} defaultValue="1" />);
    const expectedClearButton = getByRole("button");
    fireEvent.click(expectedClearButton);
    expect(mockOnClick).toHaveBeenCalledWith(null);
  });

  it("não deve exibir o ícone de limpar quando nenhuma opção é selecionada", () => {
    const { queryByRole } = render(<SelectField {...defaultProps} />);
    const expectedClearButton = queryByRole("button");
    expect(expectedClearButton).toBeNull();
  });
});
