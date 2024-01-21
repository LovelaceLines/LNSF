import { createContext, useCallback } from "react";
import { iRole, iUser, iUserFilter, iUserProvider, iUserRole, iUserTypes, iattPasswordUser, idelUserID, idelUserRole, iregisterUser, iregisterUserRole } from "./type";
import { toast } from "react-toastify";
import { Api } from "../../services/api/axios";

export const AccountContext = createContext({} as iUserTypes);

export const AccountProvider = ({ children }: iUserProvider) => {

    const viewUser = async () => {
        try {

            const response = await Api.get('/User');

            if (response.status === 200) {
                return response.data as iUser[];
            }
        } catch (error: any) {
            if (error.response) {
                if (error.response.status === 400) {
                    const appException = error.response.data.appException;

                    if (appException) {
                        if (appException.code === 'SUA_CODIFICACAO_DE_ERRO') {
                            toast.error('Erro específico da aplicação: ' + appException.message);
                        } else {
                            toast.error('Erro desconhecido da aplicação: ' + appException.message);
                        }
                    } else {
                        toast.error(error.response.data.message);
                    }
                } else {
                    toast.error('Ocorreu um erro ao processar a requisição.');
                    console.error('Error Message:', error.message);
                }
            } else {
                toast.error('Ocorreu um erro desconhecido.');
                console.error('Error Message:', error.message);
            }
            return new Error((error as { message: string }).message || 'Ops, não foi possível acessar o banco.');
        }
        return [];
    }

    const registerUser = useCallback(async (data: iregisterUser) => {
        try {

            const response = await Api.post('/User', JSON.stringify(data))

            if (response.status === 200) {
                toast.success('Usuário cadastrado!');
                return response.data as iUser;
            }
        } catch (error: any) {
            if (error.response) {
                if (error.response.status === 400) {
                    const appException = error.response.data.appException;

                    if (appException) {
                        if (appException.code === 'SUA_CODIFICACAO_DE_ERRO') {
                            toast.error('Erro específico da aplicação: ' + appException.message);
                        } else {
                            toast.error('Erro desconhecido da aplicação: ' + appException.message);
                        }
                    } else {
                        toast.error(error.response.data.message);
                    }
                } else {
                    toast.error('Ocorreu um erro ao processar a requisição.');
                    console.error('Error Message:', error.message);
                }
            } else {
                toast.error('Ocorreu um erro desconhecido.');
                console.error('Error Message:', error.message);
            }
            return new Error((error as { message: string }).message || 'Ops, não foi possível acessar o banco.');
        }
        return {} as iUser;
    }, []);

    const registerUserRole = useCallback(async (data: iregisterUserRole) => {
        try {

            const response = await Api.post('/User', JSON.stringify(data))

            if (response.status === 200) {
                toast.success('Perfil adicionado ao usuário!');
                return response.data as iUser;
            }
        } catch (error: any) {
            if (error.response) {
                if (error.response.status === 400) {
                    const appException = error.response.data.appException;

                    if (appException) {
                        if (appException.code === 'SUA_CODIFICACAO_DE_ERRO') {
                            toast.error('Erro específico da aplicação: ' + appException.message);
                        } else {
                            toast.error('Erro desconhecido da aplicação: ' + appException.message);
                        }
                    } else {
                        toast.error(error.response.data.message);
                    }
                } else {
                    toast.error('Ocorreu um erro ao processar a requisição.');
                    console.error('Error Message:', error.message);
                }
            } else {
                toast.error('Ocorreu um erro desconhecido.');
                console.error('Error Message:', error.message);
            }
            return new Error((error as { message: string }).message || 'Ops, não foi possível acessar o banco.');
        }
        return {} as iUser;
    }, []);

    const updateUser = useCallback(async (data: iUser) => {
        try {

            const response = await Api.put('/User', JSON.stringify(data));

            if (response.status === 200) {
                toast.success('Usuário atualizado!');
                return response.data as iUser;
            }
        } catch (error: any) {
            if (error.response) {
                if (error.response.status === 400) {
                    const appException = error.response.data.appException;

                    if (appException) {
                        if (appException.code === 'SUA_CODIFICACAO_DE_ERRO') {
                            toast.error('Erro específico da aplicação: ' + appException.message);
                        } else {
                            toast.error('Erro desconhecido da aplicação: ' + appException.message);
                        }
                    } else {
                        toast.error(error.response.data.message);
                    }
                } else {
                    toast.error('Ocorreu um erro ao processar a requisição.');
                    console.error('Error Message:', error.message);
                }
            } else {
                toast.error('Ocorreu um erro desconhecido.');
                console.error('Error Message:', error.message);
            }
            return new Error((error as { message: string }).message || 'Ops, não foi possível acessar o banco.');
        }
        return {} as iUser;

    }, []);

    const updatePassword = useCallback(async (data: iattPasswordUser) => {
        try {

            const response = await Api.put('/User/password', JSON.stringify(data));

            if (response.status === 200) {
                toast.success('Senha do usuário atualizado!');
                return response.data as iUser;
            }
        } catch (error: any) {
            if (error.response) {
                if (error.response.status === 400) {
                    const appException = error.response.data.appException;

                    if (appException) {
                        if (appException.code === 'SUA_CODIFICACAO_DE_ERRO') {
                            toast.error('Erro específico da aplicação: ' + appException.message);
                        } else {
                            toast.error('Erro desconhecido da aplicação: ' + appException.message);
                        }
                    } else {
                        toast.error(error.response.data.message);
                    }
                } else {
                    toast.error('Ocorreu um erro ao processar a requisição.');
                    console.error('Error Message:', error.message);
                }
            } else {
                toast.error('Ocorreu um erro desconhecido.');
                console.error('Error Message:', error.message);
            }
            return new Error((error as { message: string }).message || 'Ops, não foi possível acessar o banco.');
        }
        return {} as iUser;

    }, []);

    const deleteUserRole = useCallback(async (data: idelUserRole) => {
        try {

            const response = await Api.delete('/Account/remove-user-from-role', { data });

            if (response.status === 200) {
                toast.success('Usuário deletado de sua função!');
                return response.data as iUser;
            }
        } catch (error: any) {
            if (error.response) {
                if (error.response.status === 400) {
                    const appException = error.response.data.appException;

                    if (appException) {
                        if (appException.code === 'SUA_CODIFICACAO_DE_ERRO') {
                            toast.error('Erro específico da aplicação: ' + appException.message);
                        } else {
                            toast.error('Erro desconhecido da aplicação: ' + appException.message);
                        }
                    } else {
                        toast.error(error.response.data.message);
                    }
                } else {
                    toast.error('Ocorreu um erro ao processar a requisição.');
                    console.error('Error Message:', error.message);
                }
            } else {
                toast.error('Ocorreu um erro desconhecido.');
                console.error('Error Message:', error.message);
            }
            return new Error((error as { message: string }).message || 'Ops, não foi possível acessar o banco.');
        }
        return {} as iUser;

    }, []);

    const deleteUserId = useCallback(async (data: idelUserID) => {
        try {

            const response = await Api.delete(`/Account/${data.id}`);

            if (response.status === 200) {
                toast.success('Usuário deletado!');
                return response.data as iUser;
            }
        } catch (error: any) {
            if (error.response) {
                if (error.response.status === 400) {
                    const appException = error.response.data.appException;

                    if (appException) {
                        if (appException.code === 'SUA_CODIFICACAO_DE_ERRO') {
                            toast.error('Erro específico da aplicação: ' + appException.message);
                        } else {
                            toast.error('Erro desconhecido da aplicação: ' + appException.message);
                        }
                    } else {
                        toast.error(error.response.data.message);
                    }
                } else {
                    toast.error('Ocorreu um erro ao processar a requisição.');
                    console.error('Error Message:', error.message);
                }
            } else {
                toast.error('Ocorreu um erro desconhecido.');
                console.error('Error Message:', error.message);
            }
            return new Error((error as { message: string }).message || 'Ops, não foi possível acessar o banco.');
        }
        return {} as iUser;

    }, []);


    const viewRole = async () => {
        try {

            const response = await Api.get('/Role');

            if (response.status === 200) {
                return response.data as iRole[];
            }
        } catch (error: any) {
            if (error.response) {
                if (error.response.status === 400) {
                    const appException = error.response.data.appException;

                    if (appException) {
                        if (appException.code === 'SUA_CODIFICACAO_DE_ERRO') {
                            toast.error('Erro específico da aplicação: ' + appException.message);
                        } else {
                            toast.error('Erro desconhecido da aplicação: ' + appException.message);
                        }
                    } else {
                        toast.error(error.response.data.message);
                    }
                } else {
                    toast.error('Ocorreu um erro ao processar a requisição.');
                    console.error('Error Message:', error.message);
                }
            } else {
                toast.error('Ocorreu um erro desconhecido.');
                console.error('Error Message:', error.message);
            }
            return new Error((error as { message: string }).message || 'Ops, não foi possível acessar o banco.');
        }
        return [];
    }

    const viewUserRole = async () => {
        try {

            const response = await Api.get('/UserRole');

            if (response.status === 200) {
                return response.data as iUserRole[];
            }
        } catch (error: any) {
            if (error.response) {
                if (error.response.status === 400) {
                    const appException = error.response.data.appException;

                    if (appException) {
                        if (appException.code === 'SUA_CODIFICACAO_DE_ERRO') {
                            toast.error('Erro específico da aplicação: ' + appException.message);
                        } else {
                            toast.error('Erro desconhecido da aplicação: ' + appException.message);
                        }
                    } else {
                        toast.error(error.response.data.message);
                    }
                } else {
                    toast.error('Ocorreu um erro ao processar a requisição.');
                    console.error('Error Message:', error.message);
                }
            } else {
                toast.error('Ocorreu um erro desconhecido.');
                console.error('Error Message:', error.message);
            }
            return new Error((error as { message: string }).message || 'Ops, não foi possível acessar o banco.');
        }
        return [];
    }

    const getUsers = useCallback(async (filter?: iUserFilter): Promise<iUser[]> => {
        const res = await Api.get<iUser[]>('/User', { params: filter });
        const users = res.data;
        return users;
    }, []);

    const getUserById = useCallback(async (id: string): Promise<iUser> => {
        const res = await Api.get<iUser[]>(`/User/`, { params: { id: id } });
        const user = res.data[0];
        return user;
    }, []);

    const getCount = useCallback(async (): Promise<number> => {
        const res = await Api.get<number>('/User/count');
        const count = res.data;
        return count;
    }, []);

    const postUser = useCallback(async (data: iregisterUser): Promise<iUser> => {
        const res = await Api.post<iUser>('/User', data);
        const user = res.data;
        return user;
    }, []);

    const putUser = useCallback(async (data: iUser): Promise<iUser> => {
        const res = await Api.put<iUser>('/User', data);
        const user = res.data;
        return user;
    }, []);

    const postAddUserToRole = useCallback(async (data: iregisterUserRole): Promise<iUser> => {
        const res = await Api.post<iUser>('/User/add-user-to-role', data);
        const user = res.data;
        return user;
    }, []);

    const putPassword = useCallback(async (data: iattPasswordUser): Promise<iUser> => {
        const res = await Api.put<iUser>('/User/password', data);
        const user = res.data;
        return user;
    }, []);

    const deleteUser = useCallback(async (id: string): Promise<iUser> => {
        const res = await Api.delete<iUser>(`/User/${id}`);
        const user = res.data;
        return user;
    }, []);

    const deleteRemoveUserFromRole = useCallback(async (data: idelUserRole): Promise<iUser> => {
        const res = await Api.delete<iUser>('/Account/remove-user-from-role', { data });
        const user = res.data;
        return user;
    }, []);

    return (

        <AccountContext.Provider value={{ viewUser, registerUser, registerUserRole, updateUser, updatePassword, deleteUserId, deleteUserRole, viewRole, viewUserRole, getUsers, getUserById, getCount, postUser, putUser, postAddUserToRole, putPassword, deleteUser, deleteRemoveUserFromRole }}>
            {children}
        </AccountContext.Provider>
    )
}