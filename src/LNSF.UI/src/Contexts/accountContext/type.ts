export interface iUserProvider {
    children: React.ReactNode
}

export interface iUser {
    id: string,
    userName: string,
    email: string,
    phoneNumber?: string
}

export interface iregisterUser {
    userName: string,
    email: string,
    phoneNumber?: string,
    password: string,
}

export interface iregisterUserRole {
    userId: string,
    roleName: string,
}

export interface iattPasswordUser {
    id: string,
    oldPassword: string,
    newPassword: string
}

export interface idelUserID {
    id: string,
}

export interface idelUserRole {
    userId: string,
    roleName: string,
}


export interface iRole{
    id: string,
    name: string,
}

export interface iUserRole{
    userId: string,
    roleId: string,
}
export interface iUserTypes {
    viewUser(): Promise<iUser[] | Error>;
    registerUser(data: iregisterUser): Promise<iUser | Error>;
    registerUserRole(data: iregisterUserRole): Promise<iUser | Error>;
    updateUser(data: iUser): Promise<iUser | Error>;
    updatePassword(data: iattPasswordUser): Promise<iUser | Error>;
    deleteUserId(data: idelUserID): Promise<iUser | Error>;
    deleteUserRole(data: idelUserRole): Promise<iUser | Error>;


    viewRole(): Promise<iRole[] | Error>;

    viewUserRole(): Promise<iUserRole[] | Error>;

}