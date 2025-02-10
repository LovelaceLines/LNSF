import { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { useParams } from "react-router-dom";

import { escort, hosting, room } from "@/types";
import {
  useEscortStore,
  useHostingStore,
  usePatientStore,
  usePeopleRoomHostingStore,
  useRoomStore,
} from "@/store";
import { toValue } from "@/utils";

export const useHostingFormPage = () => {
  const { id } = useParams<{ id: string | undefined }>();
  const {
    formState: { errors },
    getValues,
    handleSubmit,
    register,
    setValue,
    watch,
  } = useForm<hosting>({
    values: { id: 0, patientId: 0, escorts: [] },
  });

  useEffect(() => {
    if (id) getHosting(+id).then((data) => toValue(data, setValue));
  }, [id]);

  const { prh, getPeopleRoomHostingByHostingId } = usePeopleRoomHostingStore();

  useEffect(() => {
    if (getValues("id")) getPeopleRoomHostingByHostingId(+getValues("id")!);
  }, [getValues("id")]);

  const { getHosting, postHosting, putHosting, addEscortToHosting, removeEscortFromHosting } =
    useHostingStore();

  const handleSave = (data: hosting) =>
    !getValues("id")
      ? postHosting({ ...data, checkOut: data.checkOut || undefined }).then((h) => toValue(h, setValue))
      : putHosting({ ...data, checkOut: data.checkOut || undefined }).then((h) => toValue(h, setValue));

  const { patients, getPatients } = usePatientStore();
  const [escort, setEscort] = useState<escort>();
  const { escorts, getEscorts } = useEscortStore();
  const [room, setRoom] = useState<room>();
  const { rooms, getRooms } = useRoomStore();

  useEffect(() => {
    getPatients({ page: 1, perPage: 9999, sort: "people.name" });
    getEscorts({ page: 1, perPage: 9999, sort: "people.name" });
    getRooms({ isAvailable: true, page: 1, perPage: 9999, sort: "number" });
  }, []);

  return {
    prh,
    patients,
    escort,
    setEscort,
    escorts,
    room,
    setRoom,
    rooms,
    addEscortToHosting,
    removeEscortFromHosting,
    register,
    handleSubmit,
    errors,
    getValues,
    setValue,
    watch,
    handleSave,
  };
};
