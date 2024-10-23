import { useState } from "react";
import { renderHook } from "@testing-library/react-hooks";
import { afterEach, beforeEach, describe, expect, it, vi } from "vitest";

import { useDebounced } from "./useDebounced";

describe("useDebounced", () => {
  const setup = ({ initialValue = 0, delay = 1000 }: { initialValue?: number; delay?: number }) => {
    const [value, setValue] = useState(initialValue);

    const onClick = useDebounced(
      () => {
        setValue((prev) => prev + 1);
      },
      [value],
      delay
    );

    return { props: { value, onClick } };
  };

  beforeEach(() => {
    vi.useFakeTimers();
  });

  afterEach(() => {
    vi.useRealTimers();
  });

  it("não deve renderizar o valor antes do atraso", () => {
    const { result } = renderHook(() => setup({ initialValue: 0, delay: 100 }));

    expect(result.current.props.value).toBe(0);

    result.current.props.onClick;

    vi.advanceTimersByTime(50);

    expect(result.current.props.value).toBe(0);
  });

  it("deve renderizar o valor após o atraso", () => {
    const { result } = renderHook(() => setup({ initialValue: 0, delay: 1000 }));

    expect(result.current.props.value).toBe(0);

    result.current.props.onClick;

    vi.advanceTimersByTime(1000);

    expect(result.current.props.value).toBe(1);
  });

  it("deve renderizar apenas uma vez após o atraso", () => {
    const { result } = renderHook(() => setup({ initialValue: 0, delay: 1000 }));

    expect(result.current.props.value).toBe(0);

    result.current.props.onClick;

    vi.advanceTimersByTime(1000);

    expect(result.current.props.value).toBe(1);

    result.current.props.onClick;

    vi.advanceTimersByTime(2000);

    expect(result.current.props.value).toBe(1);
  });

  it("deve renderizar apenas uma vez após o atraso, mesmo que o valor seja alterado", () => {
    const { result } = renderHook(() => setup({ initialValue: 0, delay: 1000 }));

    result.current.props.onClick;
    result.current.props.onClick;
    result.current.props.onClick;
    result.current.props.onClick;
    result.current.props.onClick;

    vi.advanceTimersByTime(1000);

    expect(result.current.props.value).toBe(1);

    vi.advanceTimersByTime(5000);

    expect(result.current.props.value).toBe(1);
  });
});
