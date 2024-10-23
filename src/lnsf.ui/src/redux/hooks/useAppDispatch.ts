import { useDispatch } from "react-redux";

import { AppDispatch } from "@/redux/types";

export const useAppDispatch = () => useDispatch<AppDispatch>();
