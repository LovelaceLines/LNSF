import { createContext, useCallback, useState } from "react";
import { iAddPeopleRoom, iAddPeopleToRoom, iPeopleFilter, iPeopleObject, iPeopleProvider, iPeopleRegister, iPeopleTypes } from "./type";
import { Api } from "../../services/api/axios";
import { apiUrl } from "../../environment/environment.temp";

export const PeopleContext = createContext({} as iPeopleTypes);

export const PeopleProvider = ({ children }: iPeopleProvider) => {
    const [people, setPeople] = useState<iPeopleObject>({} as iPeopleObject)

  const getPeoples = async (filter?: iPeopleFilter): Promise<iPeopleObject[]> => {
		const res = await Api.get<iPeopleObject[]>(`${apiUrl}/People`, { params: filter });
		const peoples = res.data;
		return peoples;
	}

	const getPeopleById = async (id: number): Promise<iPeopleObject> => {
		const res = await Api.get<iPeopleObject[]>(`${apiUrl}/People`, { params: id });
		const people = res.data[0];
		return people;
	}

	const getCount = async (): Promise<number> => {
		const res = await Api.get<number>('/People/count');
		const count = res.data;
		return count;
	};

	const postPeople = useCallback(async (data: iPeopleRegister): Promise<iPeopleObject> => {
		const res = await Api.post<iPeopleObject>('/People', data);
		const people = res.data;
		return people;
	}, []);

	const putPeople = useCallback(async (data: iPeopleObject): Promise<iPeopleObject> => {
		const res = await Api.put<iPeopleObject>(`${apiUrl}/People`, data);
		const people = res.data;
		return people;
	}, []);

    return (
        <PeopleContext.Provider value={{ people, setPeople, getPeoples, getPeopleById, getCount, postPeople, putPeople }}>
            {children}
        </PeopleContext.Provider>
    )
}