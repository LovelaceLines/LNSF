import { useEffect } from "react";
import { useForm } from "react-hook-form";

import { patientTreatment } from "@/types";
import { usePatientStore, useTreatmentStore } from "@/zustand";

export const usePatientTreatmentFormPage = ({ patientTreatment }: { patientTreatment: patientTreatment }) => {
	const { addTreatmentToPatient, removeTreatmentFromPatient } = usePatientStore();
	const { register, getValues, watch, setValue } = useForm<patientTreatment>({
		values: patientTreatment,
	});

	const { treatments, getTreatments } = useTreatmentStore();

	useEffect(() => {
		getTreatments({ sort: "name", page: 1, perPage: 1000 });
	}, []);

	return {
		treatments,
		addTreatmentToPatient,
		removeTreatmentFromPatient,
		register,
		getValues,
		watch,
		setValue,
	};
};
