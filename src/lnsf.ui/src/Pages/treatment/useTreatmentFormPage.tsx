import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { useParams } from "react-router-dom";

import { treatment, typeTreatment } from "@/types";
import { useTreatmentStore } from "@/store";
import { toValue } from "@/utils";

export const useTreatmentFormPage = () => {
	const { getTreatment, postTreatment, putTreatment } = useTreatmentStore();
	const { id } = useParams<{ id: string | undefined }>();
	const {
		register,
		handleSubmit,
		formState: { errors },
		getValues,
		watch,
		setValue,
	} = useForm<treatment>({
		values: { id: 0, name: "nome", type: typeTreatment.pretransplant },
	});

	useEffect(() => {
		if (id) getTreatment(Number(id)).then((data) => toValue(data, setValue));
	}, [id]);

	const handleSave = (data: treatment) => {
		if (!id) postTreatment(data);
		else putTreatment(data);
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
