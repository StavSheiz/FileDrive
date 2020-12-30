import { User } from './../../modules/User';
import { appConfig } from './../../appConfig';
import { IGetUserRequestParams, IAddUserRequestParams, IAddUserRequestData } from './interfaces/login-request-interfaces';
import { AxiosRequest } from './../../utils/api/axios-request';

export class LoginAPI {
    private constructor() { }

    public static async getUser(name: string, password: string) {
        const response = await AxiosRequest.get<IGetUserRequestParams, User>({
            url: appConfig.baseUrl + "/api/login/",
            urlParams: { name, password }
        });

        return response;
    }

    public static async addUser(name: string, password: string) {
        const response = await AxiosRequest.post<IAddUserRequestParams, IAddUserRequestData, Boolean>({
            url: appConfig.baseUrl + "/api/login/",
            urlParams: {},
            data: { name, password },
        });

        return response;
    }
}