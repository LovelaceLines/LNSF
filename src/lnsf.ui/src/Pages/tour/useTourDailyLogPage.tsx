import { useEffect, useState } from "react";

import { people } from "@/types";
import { usePeopleStore, useTourStore } from "@/store";

export const useTourDailyLogPage = () => {
  const { getOpenTours, openTours } = useTourStore();
  const [people, setPeople] = useState<people>();
  const { getPeoples, peoples } = usePeopleStore();

  useEffect(() => {
    getOpenTours();
    getPeoples({ isActive: true, sort: "name", page: 1, perPage: 9999 });
  }, []);

  const handleSelect = (id: number) => setPeople(peoples.find((p) => p.id === id));

  return {
    people,
    setPeople,
    peoples,
    openTours,
    handleSelect,
  };
};
