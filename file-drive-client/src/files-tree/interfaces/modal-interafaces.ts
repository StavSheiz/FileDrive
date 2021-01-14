import { ENUMModalType } from './../../enums/ENUMModalType';
import { ITreeEntity } from './ITreeEntity';
export interface IOpenModalParams {
    entity: ITreeEntity,
    modalType: ENUMModalType
}

export interface IBaseModalProps {
    entity: ITreeEntity | null,
    closeModal: () => void
}