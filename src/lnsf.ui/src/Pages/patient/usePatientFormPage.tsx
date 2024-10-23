import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { useParams } from "react-router-dom";

import { patient } from "@/types";
import { toValue } from "@/utils";
import { useHospitalStore, usePatientStore, usePeopleStore } from "@/zustand";

export const usePatientFormPage = () => {
	const { getPatient, postPatient, putPatient } = usePatientStore();
	const { id } = useParams<{ id: string | undefined }>();
	const {
		register,
		handleSubmit,
		formState: { errors },
		getValues,
		watch,
		setValue,
	} = useForm<patient>({
		values: { id: 0, peopleId: 0, hospitalId: 0, socioeconomicRecord: false, term: false } as patient,
	});

	useEffect(() => {
		if (id) getPatient(id).then((data) => toValue(data, setValue));
	}, []);

	const handleSave = (data: patient) => {
		if (!id) postPatient(data);
		else putPatient(data);
	};

	const { peoples, getPeoples } = usePeopleStore();
	const { hospitals, getHospitals } = useHospitalStore();

	useEffect(() => {
		getPeoples({ isPatient: false, sort: "name", page: 1, perPage: 1000 });
		getHospitals({ sort: "name", page: 1, perPage: 1000 });
	}, []);

	return {
		id,
		peoples,
		hospitals,
		register,
		handleSubmit,
		errors,
		getValues,
		setValue,
		watch,
		handleSave,
	};
};
