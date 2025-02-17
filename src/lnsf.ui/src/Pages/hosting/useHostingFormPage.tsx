import { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { useParams } from "react-router-dom";

import { escort, hosting, hostingEscort, peopleRoomHosting, room } from "@/types";
import {
  useEscortStore,
  useHostingStore,
  usePatientStore,
  usePeopleStore,
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
      ? postHosting({ ...data, checkOut: data.checkOut || undefined }).then((h) =>
          getHosting(h.id!).then((data) => toValue(data, setValue))
        )
      : putHosting({ ...data, checkOut: data.checkOut || undefined }).then((h) =>
          getHosting(h.id!).then((data) => toValue(data, setValue))
        );

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

  const handleAddEscortToHosting = (hostingEscort: hostingEscort) =>
    addEscortToHosting(hostingEscort).then((h) =>
      getHosting(h.hostingId!)
        .then((data) => toValue(data, setValue))
        .then(() => setEscort({ id: undefined, peopleId: 0, people: undefined }))
    );

  const handleRemoveEscortFromHosting = (hostingEscort: hostingEscort) =>
    removeEscortFromHosting(hostingEscort).then((h) =>
      getHosting(h.hostingId!).then((data) => toValue(data, setValue))
    );

  const { addPeopleToRoom, removePeopleFromRoom } = usePeopleStore();

  const handleAddPeopleToRoom = (peopleRoomHosting: peopleRoomHosting) =>
    addPeopleToRoom(peopleRoomHosting).then((h) => getPeopleRoomHostingByHostingId(h.hostingId));

  const handleRemovePeopleFromRoom = (peopleRoomHosting: peopleRoomHosting) =>
    removePeopleFromRoom(peopleRoomHosting).then((h) => getPeopleRoomHostingByHostingId(h.hostingId));

  return {
    prh,
    patients,
    escort,
    setEscort,
    escorts,
    room,
    setRoom,
    rooms,
    handleAddEscortToHosting,
    handleRemoveEscortFromHosting,
    register,
    handleSubmit,
    errors,
    getValues,
    setValue,
    watch,
    handleSave,
    handleAddPeopleToRoom,
    handleRemovePeopleFromRoom,
  };
};
