import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { useParams } from "react-router-dom";

import { people } from "@/types";
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
		values: {
			id: 0,
			name: "",
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
