import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { useParams } from "react-router-dom";

import { treatment, typeTreatment } from "@/types";
import { useTreatmentStore } from "@/store";
import { toValue } from "@/utils";

export const useTreatmentFormPage = () => {
  const { id } = useParams<{ id: string | undefined }>();
  const {
    control,
    register,
    handleSubmit,
    formState: { errors },
    getValues,
    watch,
    setValue,
  } = useForm<treatment>({
    values: { id: 0, name: "", type: typeTreatment.cancer },
  });

  useEffect(() => {
    if (id) getTreatment(Number(id)).then((data) => toValue(data, setValue));
  }, [id]);

  const { getTreatment, postTreatment, putTreatment } = useTreatmentStore();

  const handleSave = (data: treatment) =>
    !getValues("id")
      ? postTreatment(data).then((t) => toValue(t, setValue))
      : putTreatment(data).then((t) => toValue(t, setValue));

  return {
    handleSave,
    control,
    errors,
    getValues,
    handleSubmit,
    setValue,
    register,
    watch,
  };
};
