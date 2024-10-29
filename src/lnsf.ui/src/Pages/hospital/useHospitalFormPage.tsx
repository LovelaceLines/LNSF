import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { useParams } from "react-router-dom";

import { hospital } from "@/types";
import { useHospitalStore } from "@/store";
import { toValue } from "@/utils";

export const useHospitalFormPage = () => {
	const { id } = useParams<{ id: string | undefined }>();
	const {
		control,
		formState: { errors },
		getValues,
		handleSubmit,
		register,
		setValue,
		watch,
	} = useForm<hospital>({
		values: { id: 0, name: "", acronym: "" },
	});

	useEffect(() => {
		if (id) getHospital(id).then((data) => toValue(data, setValue));
	}, [id]);

	const { getHospital, postHospital, putHospital } = useHospitalStore();

	const handleSave = (data: hospital) =>
		!getValues("id")
			? postHospital(data).then((h) => toValue(h, setValue))
			: putHospital(data).then((h) => toValue(h, setValue));

	return {
		handleSave,
		control,
		errors,
		getValues,
		handleSubmit,
		register,
		setValue,
		watch,
	};
};
