export interface iHospitalProvider {
    children: React.ReactNode
}

export interface iHospitalObject {
    id: number,
    name: string,
    acronym: string,
}

export interface iHospital {
    name: string,
    acronym: string,
}

export interface iHospitalTypes {
    hospital: iHospitalObject,
    setHospital: React.Dispatch<React.SetStateAction<iHospitalObject>>
    viewHospital(page: number, filter: string, textFilter: string): Promise<iHospitalObject[] | Error>;
    registerHospital(data: iHospital): Promise<iHospitalObject | Error>;
    updateHospital(data: iHospitalObject): Promise<iHospitalObject | Error>;
    countHospital(): Promise<number>;
    deleteHospital(data: string): Promise<iHospitalObject | Error>;
}