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

export const UpdateTreeEntity = (tree: ITreeEntity, entityToUpdate: ITreeEntity) => {
    if (tree.id === entityToUpdate.parentId) {
        tree.children = tree.children.map(entity => { return entity.id === entityToUpdate.id ? entityToUpdate : entity });
    } else {
        tree.children.forEach(child => {
            UpdateTreeEntity(child, entityToUpdate)
        })
    }
}