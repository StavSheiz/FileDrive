import axios from 'axios';
import { ENUMExceptionCodes } from '../../enums/ENUMExceptionCodes';
import { Logger } from '../log/logger';
import { IDeleteRequestParams, IGetRequestParams, IPostRequestParams } from './interfaces/request-params-interfaces';
import { IResponse } from './interfaces/response-interfaces';

const defaultHeaders = {
    "Access-Control-Allow-Credentials": "*",
    "Content-Type": "application/json"
}

axios.defaults.withCredentials = true;



export class AxiosRequest {
    private constructor() { }

    public static async get<TUrlParams, TResponseData>({ url, urlParams }: IGetRequestParams<TUrlParams>) {
        let responseData: IResponse<TResponseData> = {
            data: null,
            exception: { message: "Error in get request", exceptionCode: ENUMExceptionCodes.RequestError }
        }

        try {
            const response = await axios.get(url, { params: urlParams, headers: defaultHeaders });

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

    public static async post<TUrlParams, TData, TResponseData>({ url, urlParams, data, headers }: IPostRequestParams<TUrlParams, TData>) {
        let responseData: IResponse<TResponseData> = {
            data: null,
            exception: { message: "Error in post request", exceptionCode: ENUMExceptionCodes.RequestError }
        }

        try {
            const response = await axios.post(url, data, { params: urlParams, headers: headers ? {...defaultHeaders, ...headers} : defaultHeaders });

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

    public static async delete<TUrlParams, TData, TResponseData>({ url, id, headers }: IDeleteRequestParams) {
        let responseData: IResponse<TResponseData> = {
            data: null,
            exception: { message: "Error in delete request", exceptionCode: ENUMExceptionCodes.RequestError }
        }

        try {
            const response = await axios.delete(`${url}/${id}`, {
                headers: headers ? {...defaultHeaders, ...headers} : defaultHeaders  });

            if (response.status === 200) {
                responseData = response.data;
            } else {
                Logger.error(`delete request failed - url: ${url}`, new Error(`Bad response - status: ${response.status} ${response.statusText}`))
            }
        } catch (ex) {
            Logger.error(`delete request failed - url: ${url}`, ex);
        }

        return responseData;
    }
}
