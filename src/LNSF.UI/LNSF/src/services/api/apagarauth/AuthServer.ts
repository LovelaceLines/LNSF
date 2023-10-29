 import { Api } from "../axios";

interface IAuth {
    AccessToken: string;
}


const auth = async (email: string, password: string): Promise<IAuth | Error> => {
    try {
        //essa parte temq criptografar, passar por header?
        // const dados = {
        //     email,
        //     password
        // }
        // const {data} = await Api.post('/login', dados);
        const { data } = await Api.get('/auth', { data: { email, password } });
        if (data) {
            return data
        }
        console.log("aqui",data)

        return new Error('Erro no login.')
    } catch (error) {
        console.error(error);
        return new Error((error as { message: string }).message || 'Erro ao consultar os registros.')
    }
};


export const AthServer = {
    auth,
}