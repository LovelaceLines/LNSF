import { createContext } from "react";
import { iFamilyGroupProfileObject, iFamilyGroupProfilePost, iFamilyGroupProfileFilter, iFamilyGroupProfileTypes } from "./type";
import { Api } from "../../services/api/axios";

export const FamilyGroupProfileContext = createContext({} as iFamilyGroupProfileTypes);

export const FamilyGroupProfileProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const getFamilyGroupProfiles = async (filter: iFamilyGroupProfileFilter): Promise<iFamilyGroupProfileObject[]> => {
    const res = await Api.get<iFamilyGroupProfileObject[]>('/FamilyGroupProfile', { params: filter });
    const familyGroupProfile = res.data;
    return familyGroupProfile;
  }

  const getFamilyGroupProfileById = async (data: { id: number, getPatient: boolean }): Promise<iFamilyGroupProfileObject> => {
    const res = await Api.get<iFamilyGroupProfileObject>(`/FamilyGroupProfile/`, { params: data });
    const familyGroupProfile = res.data;
    return familyGroupProfile;
  }

  const getCountFamilyGroupProfile = async (): Promise<number> => {
    const res = await Api.get<number>('/FamilyGroupProfile/count');
    const count = res.data;
    return count;
  }

  const postFamilyGroupProfile = async (data: iFamilyGroupProfilePost): Promise<iFamilyGroupProfileObject> => {
    const res = await Api.post<iFamilyGroupProfileObject>('/FamilyGroupProfile', data);
    const familyGroupProfile = res.data;
    return familyGroupProfile;
  }

  const putFamilyGroupProfile = async (data: iFamilyGroupProfileObject): Promise<iFamilyGroupProfileObject> => {
    const res = await Api.put<iFamilyGroupProfileObject>('/FamilyGroupProfile', data);
    const familyGroupProfile = res.data;
    return familyGroupProfile;
  }

  const deleteFamilyGroupProfile = async (id: number): Promise<iFamilyGroupProfileObject> => {
    const res = await Api.delete<iFamilyGroupProfileObject>(`/FamilyGroupProfile/${id}`);
    const familyGroupProfile = res.data;
    return familyGroupProfile;
  }

  return (
    <FamilyGroupProfileContext.Provider value={{ getFamilyGroupProfiles, getFamilyGroupProfileById, getCountFamilyGroupProfile, postFamilyGroupProfile, putFamilyGroupProfile, deleteFamilyGroupProfile }}>
      {children}
    </FamilyGroupProfileContext.Provider>
  )
}