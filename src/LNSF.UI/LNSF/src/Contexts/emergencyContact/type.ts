export interface iEmergencyContactProvider {
    children: React.ReactNode
}

export interface iEmergencyContactObject {
    id?: number,
    name?: string,
    phone?: string,
    peopleId?: number
}

export interface iEmergencyContactTypes{
    emergencyContact : iEmergencyContactObject,
    setEmergencyContact: React.Dispatch<React.SetStateAction<iEmergencyContactObject>>
    viewEmergencyContact(page: number, filter: string, textFilter: string): Promise<iEmergencyContactObject[] | Error>;
    registerEmergencyContact(data: iEmergencyContactObject): Promise<iEmergencyContactObject | Error>;
    updateEmergencyContact(data: iEmergencyContactObject): Promise<iEmergencyContactObject | Error>;
    countEmergencyContact(): Promise<number>;
    deleteEmergencyContact(data: iEmergencyContactObject): Promise<iEmergencyContactObject | Error>;
}

