import { combineReducers } from "redux";

import authReducer from "./features/auth/slice";

export const rootReducer = combineReducers({
	auth: authReducer,
});
