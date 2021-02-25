import { IDuplicateFileRequestParams } from './../interfaces/tree-request-interfaces';
import { appConfig } from "../../appConfig";
import { AxiosRequest } from "../../utils/api/axios-request";
import { ITreeEntity } from "../interfaces/ITreeEntity";

export const getTree = async () => {
    const response = await AxiosRequest.get<any, ITreeEntity[]>({
        url: appConfig.baseUrl + "/api/files/tree"
    });

    return response.data;
}

export const addFile = async (form: FormData) => {
    const response = await AxiosRequest.post<{}, FormData, ITreeEntity>({
        url: appConfig.baseUrl + "/api/files/addFile",
        data: form,
        headers: {
            "Content-Type": "multipart/form-data"
        }
    })

    return response
}

type AddFolderData = { folderName: string, parentId: number }
export const addFolder = async (folderName: string, parentId: number) => {
    const response = await AxiosRequest.post<{}, AddFolderData, ITreeEntity>({
        url: appConfig.baseUrl + "/api/files/addFolder",
        data: {
            folderName,
            parentId
        }
    })

    return response
}

export const duplicateFile = async (fileId: number) => {
    const response = await AxiosRequest.get<IDuplicateFileRequestParams, ITreeEntity>({
        url: appConfig.baseUrl + "/api/files/duplicateFile",
        urlParams: {
            entityId: fileId
        }
    })

    return response;
}