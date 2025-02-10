import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { useParams } from "react-router-dom";

import { room } from "@/types";
import { useRoomStore } from "@/store";
import { toValue } from "@/utils";

export const useRoomFormPage = () => {
  const { id } = useParams<{ id: string | undefined }>();
  const {
    control,
    register,
    handleSubmit,
    formState: { errors },
    getValues,
    watch,
    setValue,
  } = useForm<room>({
    values: { id: 0, beds: 0, number: "", storey: 0, bathroom: false, available: false },
  });

  useEffect(() => {
    if (id) getRoom(id).then((data) => toValue(data, setValue));
  }, [id]);

  const { getRoom, postRoom, putRoom } = useRoomStore();

  const handleSave = (data: room) =>
    !getValues("id")
      ? postRoom(data).then((r) => toValue(r, setValue))
      : putRoom(data).then((r) => toValue(r, setValue));

  return {
    handleSave,
    control,
    errors,
    handleSubmit,
    getValues,
    register,
    setValue,
    watch,
  };
};
