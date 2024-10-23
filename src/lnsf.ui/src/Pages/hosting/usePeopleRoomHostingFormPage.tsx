import { peopleRoomHosting } from "@/types";
import { usePeopleStore } from "@/zustand";
import { useForm } from "react-hook-form";

export const usePeopleRoomHostingFormPage = ({ prh }: { prh: peopleRoomHosting }) => {
	const { addPeopleToRoom, removePeopleFromRoom } = usePeopleStore();
	const { register, getValues, watch } = useForm<peopleRoomHosting>({
		values: prh,
	});

	return {
		addPeopleToRoom,
		removePeopleFromRoom,
		register,
		getValues,
		watch,
	};
};
