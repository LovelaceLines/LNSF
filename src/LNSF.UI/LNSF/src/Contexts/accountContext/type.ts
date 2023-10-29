export interface iAccountProvider {
    children: React.ReactNode
}

export interface iaccount {
    id: string,
    userName: string,
    role: number
}

export interface iregisterAccount {
    password: string,
    userName: string,
    role: number
}

export interface iattAccount {
    id?: string,
    userName?: string,
    newPassword?: string,
    password?: string,
    oldPassword?: string,
    role?: number
}

export interface idelAccount {
    id: string,
}

export interface iAccountTypes {
    account: iaccount,
    setAccount: React.Dispatch<React.SetStateAction<iaccount>>
    viewAccount (userName?: string): Promise<iaccount[] | Error>;
    registerAccount(data: iregisterAccount): Promise<iaccount | Error>;
    updateAccount(data: iattAccount, verificador: boolean): Promise<iaccount | Error>;
    deleteAccount(data: idelAccount): Promise<iaccount | Error>;
}