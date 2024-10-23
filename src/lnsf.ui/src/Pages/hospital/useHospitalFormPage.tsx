import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { useParams } from "react-router-dom";

import { hospital } from "@/types";
import { useHospitalStore } from "@/zustand";
import { toValue } from "@/utils";

export const useHospitalFormPage = () => {
	const { getHospital, postHospital, putHospital } = useHospitalStore();
	const { id } = useParams<{ id: string | undefined }>();
	const {
		register,
		handleSubmit,
		formState: { errors },
		getValues,
		watch,
		setValue,
	} = useForm<hospital>({
		values: { id: 0, name: "nome", acronym: "n" } as hospital,
	});

	useEffect(() => {
		if (id) getHospital(id).then((data) => toValue(data, setValue));
	}, [id]);

	const handleSave = (data: hospital) => {
		if (!id) postHospital(data);
		else putHospital(data);
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
