import { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { useParams } from "react-router-dom";

import { escort, hosting, room } from "@/types";
import { useEscortStore, useHostingStore, usePatientStore, usePeopleRoomHostingStore, useRoomStore } from "@/zustand";
import { toValue } from "@/utils";

export const useHostingFormPage = () => {
	const { getHosting, postHosting, putHosting, addEscortToHosting, removeEscortFromHosting } = useHostingStore();
	const { id } = useParams<{ id: string | undefined }>();
	const {
		register,
		handleSubmit,
		formState: { errors },
		getValues,
		watch,
		setValue,
	} = useForm<hosting>({
		values: { id: 0, patientId: 0, escorts: [] } as hosting,
	});

	const handleSave = (data: hosting) => {
		if (!id) postHosting(data);
		else putHosting(data);
	};

	const { patients, getPatients } = usePatientStore();
	const [escort, setEscort] = useState<escort>();
	const { escorts, getEscorts } = useEscortStore();
	const [room, setRoom] = useState<room>();
	const { rooms, getRooms } = useRoomStore();
	const { getPeopleRoomHostingByHostingId } = usePeopleRoomHostingStore();

	useEffect(() => {
		getPatients({ page: 1, perPage: 9999, sort: "people.name" });
		getEscorts({ page: 1, perPage: 9999, sort: "people.name" });
		getRooms({ page: 1, perPage: 9999, sort: "number" });
	}, []);

	useEffect(() => {
		if (id) {
			getHosting(Number(id)).then((data) => toValue(data, setValue));
			getPeopleRoomHostingByHostingId(Number(id)).then((data) => setRoom(data[0].room));
		}
	}, [id]);

	return {
		id,
		patients,
		escort,
		setEscort,
		escorts,
		room,
		setRoom,
		rooms,
		addEscortToHosting,
		removeEscortFromHosting,
		register,
		handleSubmit,
		errors,
		getValues,
		setValue,
		watch,
		handleSave,
	};
};
