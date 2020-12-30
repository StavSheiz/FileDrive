import { ENUMExceptionCodes } from '../../enums/ENUMExceptionCodes';
import { IResponse } from './interfaces/response-interfaces';
import { Logger } from '../log/logger';
import { IGetRequestParams, IPostRequestParams } from './interfaces/request-params-interfaces';
import axios from 'axios'


export class AxiosRequest {
    private constructor() { }

    public static async get<TUrlParams, TResponseData>({ url, urlParams }: IGetRequestParams<TUrlParams>) {
        let responseData: IResponse<TResponseData> = {
            data: null,
            exception: { message: "Error in get request", exceptionCode: ENUMExceptionCodes.RequestError }
        }

        try {
            const response = await axios.get(url, { params: urlParams });

            if (response.status === 200) {
                responseData = response.data;
            } else {
                Logger.error(`get request failed - url: ${url}`, new Error(`Bad response - status: ${response.status} ${response.statusText}`))
            }
        } catch (ex) {
            Logger.error(`get request failed - url: ${url}`, ex);
        }

        return responseData;
    }

    public static async post<TUrlParams, TData, TResponseData>({ url, urlParams, data }: IPostRequestParams<TUrlParams, TData>) {
        let responseData: IResponse<TResponseData> = {
            data: null,
            exception: { message: "Error in post request", exceptionCode: ENUMExceptionCodes.RequestError }
        }

        try {
            const response = await axios.post(url, data, { params: urlParams });

            if (response.status === 200) {
                responseData = response.data;
            } else {
                Logger.error(`post request failed - url: ${url}`, new Error(`Bad response - status: ${response.status} ${response.statusText}`))
            }
        } catch (ex) {
            Logger.error(`post request failed - url: ${url}`, ex);
        }

        return responseData;
    }
}
