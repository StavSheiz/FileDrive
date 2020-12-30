import { ENUMExceptionCodes } from '../../../enums/ENUMExceptionCodes';
export interface IResponseException {
    message: string,
    exceptionCode: ENUMExceptionCodes
}
export interface IResponse<TResponseData> {
    data: TResponseData | null;
    exception: IResponseException | null
}