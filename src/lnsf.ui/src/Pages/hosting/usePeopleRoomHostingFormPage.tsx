import { peopleRoomHosting } from "@/types";
import { usePeopleStore } from "@/store";
import { useForm } from "react-hook-form";

export const usePeopleRoomHostingFormPage = ({ prh }: { prh: peopleRoomHosting }) => {
  const { register, getValues, watch } = useForm<peopleRoomHosting>({
    values: prh,
  });

  const { addPeopleToRoom, removePeopleFromRoom } = usePeopleStore();

  return {
    addPeopleToRoom,
    removePeopleFromRoom,
    register,
    getValues,
    watch,
  };
};
