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

type AddFolderData = {folderName: string, parentId: number}
export const addFolder = async (folderName: string, parentId: number) => {
    const response = await AxiosRequest.post<{},AddFolderData, ITreeEntity>({
        url: appConfig.baseUrl + "/api/files/addFolder",
        data: {
            folderName,
            parentId
        }        
    })

    return response
}

export const deleteTreeEntity = async (entityId: number) => {
    const response = await AxiosRequest.delete<any, any, boolean>({
        url: appConfig.baseUrl + "/api/files/deleteTreeEntity",
        id: entityId
    })

    return response
}