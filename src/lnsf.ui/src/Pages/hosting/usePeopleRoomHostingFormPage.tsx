import { useForm } from "react-hook-form";

import { peopleRoomHosting } from "@/types";

export const usePeopleRoomHostingFormPage = ({ prh }: { prh: peopleRoomHosting }) => {
  const { register, getValues, watch } = useForm<peopleRoomHosting>({
    values: prh,
  });

  return {
    register,
    getValues,
    watch,
  };
};
