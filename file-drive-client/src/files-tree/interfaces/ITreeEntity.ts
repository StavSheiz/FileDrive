import { User } from '../../models/User';
export interface ITreeEntity {
    id: number,
    name: string,
    owner: User
    children: ITreeEntity[]
}