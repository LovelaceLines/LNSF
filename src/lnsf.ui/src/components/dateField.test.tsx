import "@testing-library/jest-dom/vitest";
import { render, RenderResult } from "@testing-library/react";
import { useForm } from "react-hook-form";
import { describe, expect, it, vi } from "vitest";

import { DateField, DateFieldProps } from "./dateField";

describe("DateField Component", () => {
  vi.mock("react-hook-form", () => ({
    useForm: () => ({
      register: vi.fn(),
      formState: { errors: {} },
    }),
  }));

  const setup = (props?: DateFieldProps): RenderResult => {
    const { register } = useForm({ values: { testField: "2022-01-01" } });
    const defaultProps: DateFieldProps = {
      label: "Test Label",
      register: register("testField"),
      error: false,
      helperText: "",
      sx: {},
    };
    return render(<DateField {...defaultProps} {...props} />);
  };

  it("deve renderizar normalmente com as props padrão", () => {
    const { getByLabelText } = setup();
    const expectedLabel = getByLabelText("Test Label");
    expect(expectedLabel).toBeInTheDocument();
  });

  it("deve renderizar com o label correto", () => {
    const { getByLabelText } = setup({ label: "Date of Birth" } as DateFieldProps);
    const expectedLabel = getByLabelText("Date of Birth");
    expect(expectedLabel).toBeInTheDocument();
  });

  it("deve mostrar um erro quando error é true", () => {
    const { getByLabelText } = setup({ error: true } as DateFieldProps);
    const input = getByLabelText("Test Label");
    expect(input).toHaveAttribute("aria-invalid", "true");
  });

  it("deve mostrar o texto de ajuda quando fornecido", () => {
    const { getByText } = setup({ helperText: "This is a required field" } as DateFieldProps);
    const helperText = getByText("This is a required field");
    console.log("helperText", helperText);
    expect(helperText).toBeInTheDocument();
  });
});
