import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { useParams } from "react-router-dom";

import { escort } from "@/types";
import { toValue } from "@/utils";
import { useEscortStore, usePeopleStore } from "@/store";

export const useEscortFormPage = () => {
	const { id } = useParams<{ id: string | undefined }>();
	const {
		formState: { errors },
		getValues,
		handleSubmit,
		register,
		setValue,
		watch,
	} = useForm<escort>({
		values: { id: 0, peopleId: 0 } as escort,
	});

	useEffect(() => {
		if (id) getEscort(id).then((data) => toValue(data, setValue));
	}, [id]);

	const { getEscort, postEscort, putEscort } = useEscortStore();

	const handleSave = (data: escort) =>
		!getValues("id") ? postEscort(data).then((e) => toValue(e, setValue)) : putEscort(data).then((e) => toValue(e, setValue));

	const { peoples, getPeoples } = usePeopleStore();

	useEffect(() => {
		getPeoples({ isEscort: false, sort: "name", page: 1, perPage: 99999 });
	}, []);

	return {
		peoples,
		handleSave,
		errors,
		getValues,
		handleSubmit,
		register,
		setValue,
		watch,
	};
};
