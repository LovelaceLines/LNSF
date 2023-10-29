
export interface iAuthProvider {
    children: React.ReactNode;
}

export interface iObjectUser {
    id: string;
    userName: string;
    role: number;
    createdAt?: string;
    password?: string;
    
}

export interface iDataLogin {
    userName: string;
    password: string;
}

export interface iTokens {
    accessToken: string;
    refreshToken: string;
    expires: string;
}

export interface iRegisterUser {
    userName: string;
    password: string;
    role: number;
}
export interface iAttUser {
    id: string;
    userName: string;
    password: string;
    oldPassword: string;
    role: number;
}
export interface iDelUser {
    accountId: string;
}

export interface iAuthTypes {
    user: iObjectUser;
    loginUser(data: iDataLogin): void;
    getUsers(useName?: string): Promise<iObjectUser | Error>;
    logoutUser(): void;
    tokens: iTokens;
    setTokens: React.Dispatch<React.SetStateAction<iTokens>>
    setUser: React.Dispatch<React.SetStateAction<iObjectUser>>
}
