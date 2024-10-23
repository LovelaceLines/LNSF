import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { useParams } from "react-router-dom";

import { escort } from "@/types";
import { toValue } from "@/utils";
import { useEscortStore, usePeopleStore } from "@/zustand";

export const useEscortFormPage = () => {
	const { getEscort, postEscort, putEscort } = useEscortStore();
	const { id } = useParams<{ id: string | undefined }>();
	const {
		register,
		handleSubmit,
		formState: { errors },
		getValues,
		watch,
		setValue,
	} = useForm<escort>({
		values: { id: 0, peopleId: 0 } as escort,
	});

	useEffect(() => {
		if (id) getEscort(id).then((data) => toValue(data, setValue));
	}, []);

	const handleSave = (data: escort) => {
		if (!id) postEscort(data);
		else putEscort(data);
	};

	const { peoples, getPeoples } = usePeopleStore();

	useEffect(() => {
		getPeoples({ isEscort: false, sort: "name", page: 1, perPage: 1000 });
	}, []);

	return {
		id,
		peoples,
		register,
		handleSubmit,
		errors,
		getValues,
		setValue,
		watch,
		handleSave,
	};
};
