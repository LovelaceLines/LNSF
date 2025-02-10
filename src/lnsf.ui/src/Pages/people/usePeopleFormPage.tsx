import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { useParams } from "react-router-dom";

import { gender, maritalStatus, people, raceColor } from "@/types";
import { usePeopleStore } from "@/store";
import { toValue } from "@/utils";

export const usePeopleFormPage = () => {
  const { id } = useParams<{ id: string | undefined }>();
  const {
    control,
    register,
    handleSubmit,
    formState: { errors },
    getValues,
    watch,
    setValue,
  } = useForm<people>({
    values: {
      id: 0,
      name: "",
      email: "",
      rg: "",
      issuingBody: "",
      cpf: "",
      street: "",
      houseNumber: "",
      neighborhood: "",
      city: "",
      state: "",
      phone: "",
      note: "",
      experience: undefined,
      status: undefined,
      birthDate: undefined,
      emergencyContacts: [],
      gender: gender.other,
      maritalStatus: maritalStatus.single,
      raceColor: raceColor.brown,
      tours: [],
    },
  });

  useEffect(() => {
    if (id) getPeople(id).then((data) => toValue(data, setValue));
  }, [id]);

  const { getPeople, postPeople, putPeople } = usePeopleStore();

  const handleSave = (data: people) =>
    !getValues("id")
      ? postPeople(data).then((p) => toValue(p, setValue))
      : putPeople(data).then((p) => toValue(p, setValue));

  return {
    handleSave,
    errors,
    control,
    getValues,
    handleSubmit,
    register,
    setValue,
    watch,
  };
};
