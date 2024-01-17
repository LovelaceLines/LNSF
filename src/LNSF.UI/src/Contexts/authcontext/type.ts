
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

export interface iUser {
    id: string;
    userName: string;
    roles: string[];
    email: string;
    phone: string;
}

export interface iDataLogin {
    userName: string;
    password: string;
}

export interface iToken {
    accessToken: string;
    refreshToken: string;
}

export interface iRegisterUser {
    userName: string;
    email: string;
    phoneNumber: string;
    password: string;
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
    isAuthenticated: boolean;
    login(data: iDataLogin): Promise<void>;
    logout(): void;
    getUser(): Promise<iUser>;
}
