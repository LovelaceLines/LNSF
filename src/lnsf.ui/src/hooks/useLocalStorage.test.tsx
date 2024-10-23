import { renderHook, act } from "@testing-library/react-hooks";
import { beforeEach, describe, expect, it } from "vitest";

import { useLocalStorage } from "./useLocalStorage";
import { getStorageValue, setStorageValue } from "@/services";

describe("useLocalStorage", () => {
  beforeEach(() => {
    localStorage.clear();
  });

  it("deve retornar o valor inicial se a chave não existir", () => {
    const { result } = renderHook(() => useLocalStorage("myKey", "initialValue"));
    expect(result.current[0]).toBe("initialValue");
  });

  it("deve retornar o valor armazenado no local storage se a chave existir", () => {
    setStorageValue("myKey", "storedValue");
    const { result } = renderHook(() => useLocalStorage("myKey", "initialValue"));
    expect(result.current[0]).toBe("storedValue");
  });

  it("deve armazenar o valor no local storage ao atualizá-lo", () => {
    const { result } = renderHook(() => useLocalStorage("myKey", "initialValue"));
    act(() => {
      result.current[1]("newValue");
    });
    expect(getStorageValue("myKey")).toBe("newValue");
  });
});
