import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { useParams } from "react-router-dom";

import { room } from "@/types";
import { useRoomStore } from "@/zustand";
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
		values: { id: 0, beds: 1, number: "1", storey: 1, bathroom: true, available: true } as room,
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
