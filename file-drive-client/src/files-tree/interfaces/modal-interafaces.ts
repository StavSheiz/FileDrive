import { ENUMModalType } from './../../enums/ENUMModalType';
import { ITreeEntity } from './ITreeEntity';
export interface IOpenModalParams {
    entity: ITreeEntity,
    modalType: ENUMModalType
}

export interface IBaseModalInterface {
    entity: ITreeEntity | null,
    closeModal: () => void
}