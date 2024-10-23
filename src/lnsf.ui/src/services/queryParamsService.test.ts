import { describe, it, expect } from "vitest";
import { createQueryParams, parseQueryParams } from "./queryParamsService";

describe("createQueryParams", () => {
  it("deve criar uma string de query params a partir de um objeto", () => {
    const obj = { name: "John", age: 30 };
    const result = createQueryParams(obj);
    expect(result).toBe("name=John&age=30");
  });

  it("deve ignorar valores nulos ou indefinidos", () => {
    const obj = { name: "John", age: null, city: undefined };
    const result = createQueryParams(obj);
    expect(result).toBe("name=John");
  });

  it("deve lidar com arrays", () => {
    const obj = { name: "John", hobbies: ["reading", "swimming"] };
    const result = createQueryParams(obj);
    expect(result).toBe("name=John&hobbies=reading&hobbies=swimming");
  });

  it("deve retornar uma string vazia se o objeto estiver vazio", () => {
    const obj = {};
    const result = createQueryParams(obj);
    expect(result).toBe("");
  });

  it("deve lidar com diferentes tipos de valores", () => {
    const obj = { name: "John", age: 30, hobbies: ["reading", "swimming"], active: true };
    const result = createQueryParams(obj);
    expect(result).toBe("name=John&age=30&hobbies=reading&hobbies=swimming&active=true");
  });
});

describe("parseQueryParams", () => {
  it("deve criar um objeto a partir de uma string de query params", () => {
    const search = "?name=John&age=30";
    const result = parseQueryParams(search);
    expect(result).toEqual({ name: "John", age: "30" });
  });

  it("deve lidar com arrays", () => {
    const search = "?name=John&hobbies=reading&hobbies=swimming";
    const result = parseQueryParams(search);
    expect(result).toEqual({ name: "John", hobbies: ["reading", "swimming"] });
  });

  it("deve lidar com valores duplicados", () => {
    const search = "?name=John&age=30&hobbies=reading&hobbies=swimming&hobbies=swimming";
    const result = parseQueryParams(search);
    expect(result).toEqual({ name: "John", age: "30", hobbies: ["reading", "swimming", "swimming"] });
  });

  it("deve lidar com valores nulos ou indefinidos", () => {
    const search = "?name=John&age=30&city=&country=null";
    const result = parseQueryParams(search);
    expect(result).toEqual({ name: "John", age: "30", city: "", country: "null" });
  });

  it("deve retornar um objeto vazio se a string estiver vazia", () => {
    const search = "";
    const result = parseQueryParams(search);
    expect(result).toEqual({});
  });
});
