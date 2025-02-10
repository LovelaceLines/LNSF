import { useForm } from "react-hook-form";

import { tour } from "@/types";
import { useTourStore } from "@/store/useTourStore";
import { toValue } from "@/utils";

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
    values: { id: 0, peopleId: 0, note: "", ...tour },
  });

  const handleSave = (data: tour) =>
    !data.id
      ? postTour(data).then((t) => toValue(t, setValue))
      : putTour(data).then((t) => toValue(t, setValue));

  const handlePutAll = (data: tour) => putAllTour(data).then((t) => toValue(t, setValue));

  return {
    handleSave,
    handlePutAll,
    errors,
    getValues,
    handleSubmit,
    register,
    setValue,
    watch,
  };
};
