import { appConfig } from "../../appConfig";
import { AxiosRequest } from "../../utils/api/axios-request";
import { ITreeEntity } from "../interfaces/ITreeEntity";

export const getTree = async () => {
        const response = await AxiosRequest.get<any, ITreeEntity[]>({
            url: appConfig.baseUrl + "/api/files/tree"
        });

        return response.data;
}