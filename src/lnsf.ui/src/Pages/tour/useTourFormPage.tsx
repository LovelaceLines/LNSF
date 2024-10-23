import { useForm } from "react-hook-form";

import { tour } from "@/types";
import { useTourStore } from "@/zustand/useTourStore";

export const useTourFormPage = ({ tour }: { tour?: tour }) => {
	const { postTour, putAllTour, putTour } = useTourStore();
	const {
		register,
		handleSubmit,
		formState: { errors },
		getValues,
		watch,
		setValue,
	} = useForm<tour>({
		values: tour,
	});

	const handleSave = (data: tour) => (!data.id ? postTour(data) : putTour(data));

	const handlePutAll = (data: tour) => putAllTour(data);

	return {
		register,
		handleSubmit,
		errors,
		getValues,
		setValue,
		watch,
		handleSave,
		handlePutAll,
	};
};
