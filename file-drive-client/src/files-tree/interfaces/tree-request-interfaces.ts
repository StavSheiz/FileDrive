import { ENUMConverterType } from './../../enums/ENUMConverterType';
export interface IDuplicateFileRequestParams {
    entityId: number
}

export interface IConvertFileRequestParams {
    entityId: number,
    type: ENUMConverterType
}

