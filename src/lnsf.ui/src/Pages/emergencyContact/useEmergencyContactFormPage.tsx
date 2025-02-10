import { useForm } from "react-hook-form";

import { emergencyContact } from "@/types";
import { useEmergencyContactStore } from "@/store";
import { toValue } from "@/utils";

export const useEmergencyContactFormPage = ({
  emergencyContact,
}: {
  emergencyContact?: emergencyContact;
}) => {
  const {
    control,
    formState: { errors },
    getValues,
    handleSubmit,
    register,
    setValue,
    watch,
  } = useForm<emergencyContact>({
    values: { id: 0, peopleId: 0, name: "", phone: "", ...emergencyContact },
  });

  const { deleteEmergencyContact, postEmergencyContact, putEmergencyContact } = useEmergencyContactStore();

  const handleSave = (data: emergencyContact) =>
    !data.id
      ? postEmergencyContact(data).then((ec) => toValue(ec, setValue))
      : putEmergencyContact(data).then((ec) => toValue(ec, setValue));

  return {
    handleSave,
    deleteEmergencyContact,
    control,
    errors,
    getValues,
    handleSubmit,
    register,
    watch,
  };
};
