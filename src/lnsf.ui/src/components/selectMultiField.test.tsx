import "@testing-library/jest-dom/vitest";
import { fireEvent, render } from "@testing-library/react";
import { describe, it, expect, vi } from "vitest";

import { SelectMultiField, SelectMultiFieldProps } from "./selectMultiField";

describe("SelectMultiField", () => {
  const mockOnClick = vi.fn();
  const defaultProps: SelectMultiFieldProps = {
    label: "Test Label",
    options: [
      { id: 1, name: "Option 1" },
      { id: 2, name: "Option 2" },
    ],
    defaultValue: [{ id: 1, name: "Option 1" }],
    labelKey: "name",
    onChance: mockOnClick,
  };

  it("deve renderizar sem falhas - defaultValues = undefined", () => {
    const { getByLabelText } = render(<SelectMultiField {...defaultProps} defaultValue={undefined} />);
    const expectedLabel = getByLabelText("Test Label");
    expect(expectedLabel).toBeInTheDocument();
  });

  it("deve renderizar sem falhas", () => {
    const { getByText, getByLabelText } = render(<SelectMultiField {...defaultProps} />);
    const expectedLabel = getByLabelText("Test Label");
    expect(expectedLabel).toBeInTheDocument();
    const expectedDefaultValue = getByText("Option 1");
    expect(expectedDefaultValue).toBeInTheDocument();
  });

  it("deve renderizar as opções corretamente", () => {
    const { getByText, getByRole } = render(<SelectMultiField {...defaultProps} />);
    fireEvent.click(getByRole("button", { name: "Open" }));
    const expectedOption1 = getByText("Option 1", { selector: "li" });
    const expectedOption2 = getByText("Option 2", { selector: "li" });
    expect(expectedOption1).toBeInTheDocument();
    expect(expectedOption2).toBeInTheDocument();
  });

  it("deve chamar onChance quando uma opção é selecionada", () => {
    const { getByText, getByRole } = render(<SelectMultiField {...defaultProps} />);
    fireEvent.click(getByRole("button", { name: "Open" }));
    fireEvent.click(getByText("Option 2", { selector: "li" }));
    expect(defaultProps.onChance).toHaveBeenCalledWith([
      { id: 1, name: "Option 1" },
      { id: 2, name: "Option 2" },
    ]);
  });

  it("deve renderizar as opções selecionadas como chips", () => {
    const { getByText } = render(<SelectMultiField {...defaultProps} />);
    const expectedChip = getByText("Option 1");
    expect(expectedChip).toBeInTheDocument();
  });
});
