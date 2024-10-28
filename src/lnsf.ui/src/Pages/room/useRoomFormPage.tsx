import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { useParams } from "react-router-dom";

import { room } from "@/types";
import { useRoomStore } from "@/store";
import { toValue } from "@/utils";

export const useRoomFormPage = () => {
	const { getRoom, postRoom, putRoom } = useRoomStore();
	const { id } = useParams<{ id: string | undefined }>();
	const {
		register,
		handleSubmit,
		formState: { errors },
		getValues,
		watch,
		setValue,
	} = useForm<room>({
		values: { id: 0, beds: 0, number: "0", storey: 0, bathroom: false, available: false },
	});

	useEffect(() => {
		if (id) getRoom(id).then((data) => toValue(data, setValue));
	}, [id]);

	const handleSave = (data: room) => {
		if (!id) postRoom(data);
		else putRoom(data);
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
