import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { useParams } from "react-router-dom";

import { people, maritalStatus, raceColor, gender } from "@/types";
import { usePeopleStore } from "@/zustand";
import { toValue } from "@/utils";

export const usePeopleFormPage = () => {
	const { getPeople, postPeople, putPeople } = usePeopleStore();
	const { id } = useParams<{ id: string | undefined }>();
	const {
		register,
		handleSubmit,
		formState: { errors },
		getValues,
		watch,
		setValue,
	} = useForm<people>({
		// TODO - Fix - Ao editar um registro, os valores não são estilizados
		values: {
			id: 0,
			name: "",
			gender: gender.other,
			maritalStatus: maritalStatus.single,
			raceColor: raceColor.ignored,
			email: "",
			rg: "",
			issuingBody: "",
			cpf: "",
			street: "",
			houseNumber: "",
			neighborhood: "",
			city: "",
			state: "",
			phone: "",
			note: "",
			experience: "",
			status: "",
			tours: [],
			emergencyContacts: [],
		},
	});

	useEffect(() => {
		if (id) getPeople(id).then((data) => toValue(data, setValue));
	}, []);

	const handleSave = (data: people) => {
		if (!id) postPeople(data);
		else putPeople(data);
	};

	return {
		id,
		register,
		handleSubmit,
		errors,
		getValues,
		setValue,
		watch,
		handleSave,
	};
};
