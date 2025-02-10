import { useForm } from "react-hook-form";

import { patientTreatment } from "@/types";
import { useTreatmentStore } from "@/store";

export const usePatientTreatmentFormPage = ({ patientTreatment }: { patientTreatment: patientTreatment }) => {
  const { control, getValues, setValue, register, watch } = useForm<patientTreatment>({
    values: patientTreatment,
  });

  const { treatments } = useTreatmentStore();

  return {
    treatments,
    control,
    getValues,
    setValue,
    register,
    watch,
  };
};
