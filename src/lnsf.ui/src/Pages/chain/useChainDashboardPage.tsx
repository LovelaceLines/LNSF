import { useChainStore, usePeopleStore } from "@/store";
import { formatGender, gender } from "@/types";
import { dateOnlyToStr } from "@/utils";
import { useEffect, useState } from "react";
import { useForm } from "react-hook-form";

export const useChainDashboardPage = () => {
  const {
    peopleHosted,
    peopleWillHosted,
    peopleWillBirthdate,
    typeTreatmentCount,
    getCountPeopleHosted,
    getPeopleWillHosted,
    getPeopleWillBirthdate,
    getCountTypeTreatment,
  } = useChainStore();

  const [genderChartCctivePeople, setGenderChartCctivePeople] = useState<
    { id: number; value: number; label: string }[]
  >([]);
  const { getPeoples } = usePeopleStore();

  const currentDate = new Date();
  const firstDayOfYear = new Date(currentDate.getFullYear(), 0, 1).toISOString().split("T")[0];
  const lastDayOfYear = new Date(currentDate.getFullYear(), 11, 31).toISOString().split("T")[0];

  const {
    control,
    register,
    handleSubmit,
    formState: { errors },
    getValues,
    watch,
    setValue,
  } = useForm<any>({
    values: {
      date: dateOnlyToStr(new Date().toISOString().split("T")[0]),
      daysToCheck: 7,
      daysToBirthdate: 15,
      checkIn: dateOnlyToStr(firstDayOfYear),
      checkOut: dateOnlyToStr(lastDayOfYear),
    },
  });

  useEffect(() => {
    getPeoples({ isActive: true, page: 1, perPage: 99999 })
      .then((p) =>
        p.reduce((acc: Record<string, number>, cur) => {
          const gender = cur.gender;
          if (!acc[gender]) acc[gender] = 0;
          acc[gender] += 1;
          return acc;
        }, {})
      )
      .then((data) => {
        setGenderChartCctivePeople(
          Object.entries(data).map(([key, value]) => ({
            id: parseInt(key),
            value,
            label: formatGender(+key as gender),
          }))
        );
      });
  }, []);

  useEffect(() => {
    getCountPeopleHosted({ date: getValues("date") });
  }, [watch("date")]);

  useEffect(() => {
    getPeopleWillHosted({ days: getValues("daysToCheck") });
  }, [watch("daysToCheck")]);

  useEffect(() => {
    getPeopleWillBirthdate({ days: getValues("daysToBirthdate") });
  }, [watch("daysToBirthdate")]);

  useEffect(() => {
    getCountTypeTreatment({
      checkIn: dateOnlyToStr(getValues("checkIn")),
      checkOut: dateOnlyToStr(getValues("checkOut")),
    });
  }, [watch("checkIn"), watch("checkOut")]);

  return {
    peopleHosted,
    peopleWillHosted,
    peopleWillBirthdate,
    genderChartCctivePeople,
    typeTreatmentCount,
    control,
    register,
    handleSubmit,
    errors,
    getValues,
    watch,
    setValue,
  };
};
