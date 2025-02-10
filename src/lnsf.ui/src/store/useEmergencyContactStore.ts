import { create } from "zustand";

import { emergencyContact, queryResult } from "@/types";
import { Axios } from "@/http";
import { toast } from "react-toastify";

type state = {
  emergencyContacts: emergencyContact[];

  getEmergencyContactsByPeopleId: (peopleId: number) => Promise<emergencyContact[]>;
  postEmergencyContact: (emergencyContact: emergencyContact) => Promise<emergencyContact>;
  putEmergencyContact: (emergencyContact: emergencyContact) => Promise<emergencyContact>;
  deleteEmergencyContact: (id: number) => Promise<emergencyContact>;
};

export const useEmergencyContactStore = create<state>((set) => ({
  emergencyContacts: [] as emergencyContact[],

  getEmergencyContactsByPeopleId: async (peopleId): Promise<emergencyContact[]> => {
    const res = await Axios.get<queryResult<emergencyContact>>("/EmergencyContact", {
      params: { peopleId: peopleId },
    });
    set({ emergencyContacts: res.data.items });
    return res.data.items;
  },

  postEmergencyContact: async (emergencyContact): Promise<emergencyContact> => {
    const res = await Axios.post<emergencyContact>("/EmergencyContact", emergencyContact);
    set((state) => ({ emergencyContacts: [...state.emergencyContacts, res.data] }));
    return res.data;
  },

  putEmergencyContact: async (emergencyContact): Promise<emergencyContact> => {
    const res = await Axios.put<emergencyContact>("/EmergencyContact", emergencyContact);
    set((state) => ({
      emergencyContacts: state.emergencyContacts.map((p) => (p.id === emergencyContact.id ? res.data : p)),
    }));
    return res.data;
  },

  deleteEmergencyContact: async (id): Promise<emergencyContact> => {
    const res = await Axios.delete<emergencyContact>(`/EmergencyContact/${id}`);
    set((state) => ({ emergencyContacts: state.emergencyContacts.filter((p) => p.id !== id) }));
    toast.success("Contato excluído com sucesso. Atualize a página!");
    return res.data;
  },
}));
