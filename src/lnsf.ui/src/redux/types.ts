import { Action, ThunkAction } from "@reduxjs/toolkit";

import { store } from "./store";

export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;
export type AppThunk<ReturnType = void> = ThunkAction<ReturnType, RootState, unknown, Action<string>>;
export type status = "idle" | "loading" | "succeeded" | "failed";
export type error = string | null;
