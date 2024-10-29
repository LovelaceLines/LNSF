import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { useParams } from "react-router-dom";

import { patient } from "@/types";
import { toValue } from "@/utils";
import { useHospitalStore, usePatientStore, usePeopleStore, useTreatmentStore } from "@/store";

export const usePatientFormPage = () => {
	const { id } = useParams<{ id: string | undefined }>();
	const {
		formState: { errors },
		getValues,
		handleSubmit,
		register,
		watch,
		setValue,
	} = useForm<patient>({
		values: { id: 0, peopleId: 0, hospitalId: 0, socioeconomicRecord: false, term: false } as patient,
	});

	useEffect(() => {
		if (id) getPatient(id).then((data) => toValue(data, setValue));
	}, [id]);

	const { getPatient, postPatient, putPatient, addTreatmentToPatient, removeTreatmentFromPatient } =
		usePatientStore();

	const handleSave = (data: patient) =>
		!getValues("id")
			? postPatient(data).then((p) => toValue(p, setValue))
			: putPatient(data).then((p) => toValue(p, setValue));

	const { peoples, getPeoples } = usePeopleStore();
	const { hospitals, getHospitals } = useHospitalStore();
	const { getTreatments } = useTreatmentStore();

	useEffect(() => {
		getPeoples({ sort: "name", page: 1, perPage: 1000 });
		getHospitals({ sort: "name", page: 1, perPage: 1000 });
		getTreatments({ sort: "name", page: 1, perPage: 1000 });
	}, []);

	return {
		addTreatmentToPatient,
		removeTreatmentFromPatient,
		peoples,
		hospitals,
		handleSave,
		errors,
		getValues,
		handleSubmit,
		register,
		setValue,
		watch,
	};
};
