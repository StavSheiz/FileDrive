import { User } from './../../modules/User';
import { appConfig } from './../../appConfig';
import { IAddUserRequestParams, IAddUserRequestData, ISignInRequestParams, ISignOutRequestParams } from './interfaces/login-request-interfaces';
import { AxiosRequest } from './../../utils/api/axios-request';

export class LoginAPI {
    private constructor() { }

    public static async signIn(name: string, password: string) {
        const response = await AxiosRequest.get<ISignInRequestParams, boolean>({
            url: appConfig.baseUrl + "/api/login/signIn",
            urlParams: { name, password }
        });

        return response;
    }

    public static async signOut() {
        const response = await AxiosRequest.get<ISignOutRequestParams, boolean>({
            url: appConfig.baseUrl + "/api/login/signOut",
            urlParams: {}
        });

        return response;
    }

    public static async addUser(name: string, password: string, confirmPassword: string) {
        const response = await AxiosRequest.post<IAddUserRequestParams, IAddUserRequestData, boolean>({
            url: appConfig.baseUrl + "/api/login/",
            urlParams: {},
            data: { name, password, confirmPassword },
        });

        return response;
    }
}