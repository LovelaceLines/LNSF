import { useForm } from "react-hook-form";

import { emergencyContact } from "@/types";
import { useEmergencyContactStore } from "@/zustand";

export const useEmergencyContactFormPage = ({ emergencyContact }: { emergencyContact?: emergencyContact }) => {
	const { deleteEmergencyContact: handleDelete, postEmergencyContact, putEmergencyContact } = useEmergencyContactStore();
	const {
		register,
		handleSubmit,
		formState: { errors },
		getValues,
		watch,
		setValue,
	} = useForm<emergencyContact>({
		values: emergencyContact,
	});

	const handleSave = (data: emergencyContact) => {
		if (!data.id) postEmergencyContact(data);
		else putEmergencyContact(data);
	};

	return {
		register,
		handleSubmit,
		errors,
		getValues,
		setValue,
		watch,
		handleSave,
		handleDelete,
	};
};
