import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { useParams } from "react-router-dom";

import { notification } from "@/types";
import { useNotificationStore } from "@/store";
import { toValue } from "@/utils";

export const useNotificationFormPage = () => {
  const { id } = useParams<{ id: string | undefined }>();
  const {
    control,
    register,
    handleSubmit,
    formState: { errors },
    getValues,
    watch,
    setValue,
  } = useForm<notification>({
    values: { id: 0, title: "", content: "", validFrom: "", expiredAt: "" },
  });

  const { getNotifications, postNotification, putNotification } = useNotificationStore();

  useEffect(() => {
    if (id) getNotifications({ id: +id }).then((data) => toValue(data.items[0], setValue));
  }, [id]);

  const handleSave = (data: notification) =>
    !getValues("id")
      ? postNotification(data).then((r) => toValue(r, setValue))
      : putNotification(data).then((r) => toValue(r, setValue));

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
