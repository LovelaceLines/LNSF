import { createContext, useCallback } from "react";
import { Api } from "../../services/api/axios";
import { iTourFilter, iTourObject, iTourRegister, iTourTypes, iTourUpdate } from "./type";
import { apiUrl } from "../../environment/environment.temp";

export const TourContext = createContext({} as iTourTypes);

export const TourProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
	const getTours = async (filter?: iTourFilter): Promise<iTourObject[]> => {
		const res = await Api.get<iTourObject[]>(`${apiUrl}/Tour`, { params: filter });
		const tours = res.data;
		return tours;
	}

	const getTourById = async (data: { id: number, getPeople: true }): Promise<iTourObject> => {
		const res = await Api.get<iTourObject[]>(`${apiUrl}/Tour/`, { params: data });
		const tour = res.data[0];
		return tour;
	}

	const getCount = async (): Promise<number> => {
		const res = await Api.get<number>('/Tour/count');
		const count = res.data;
		return count;
	};

	const postTour = useCallback(async (data: iTourRegister): Promise<iTourObject> => {
		const res = await Api.post<iTourObject>('/Tour', data);
		const tour = res.data;
		return tour;
	}, []);

	const putTour = useCallback(async (data: iTourUpdate): Promise<iTourObject> => {
		const res = await Api.put<iTourObject>(`${apiUrl}/Tour`, data);
		const tour = res.data;
		return tour;
	}, []);


	const putAllTour = useCallback(async (data: iTourObject): Promise<iTourObject> => {
		const res = await Api.put('/Tour', data);
		const tour = res.data;
		return tour;
	}, []);

	return (
		<TourContext.Provider value={{ getTours, getTourById, getCount, postTour, putTour, putAllTour }}>
			{children}
		</TourContext.Provider>
	)
}