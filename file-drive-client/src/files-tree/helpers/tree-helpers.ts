import { ITreeEntity } from "../interfaces/ITreeEntity";

export const AddTreeEntityToTree = (tree: ITreeEntity, newTreeEntity: ITreeEntity) => {
    if (tree.id === newTreeEntity.parentId) {
        tree.children.push(newTreeEntity)
    } else {
        tree.children.forEach(child => {
            AddTreeEntityToTree(child, newTreeEntity)
        })
    }
}