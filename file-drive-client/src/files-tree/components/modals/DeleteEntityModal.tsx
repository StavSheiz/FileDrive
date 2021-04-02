import React from "react";
import { deleteTreeEntity } from "../../api/tree-api";
import { ITreeEntity } from "../../interfaces/ITreeEntity";
import { IBaseModalProps } from "../../interfaces/modal-interafaces";
import ValidationPopUp from './ValidationPopUp'

const DeleteEntityModal = (props: IBaseModalProps) => {
    const { closeModal, entity, setTree } = props;
    const deleteEntityInTree = (treeLevel: ITreeEntity[], entityId: number) => {
        const filteredLevel = treeLevel.filter(entity => entity.id != entityId)

        if (filteredLevel.length !== treeLevel.length){            
            return filteredLevel
        } else {
            treeLevel.forEach(treeEntity => 
                treeEntity.children = deleteEntityInTree(treeEntity.children, entityId))
            return treeLevel
        }
    }
    const onDelete = () => {
        const entityIdToDelete = (entity as ITreeEntity).id
        deleteTreeEntity(entityIdToDelete)
        setTree && setTree((prev: ITreeEntity[] | null) => {
            deleteEntityInTree(prev as ITreeEntity[], (entity as ITreeEntity).id)
            return prev as ITreeEntity[]
        })
        closeModal()
    }
    return <ValidationPopUp onClose={closeModal} onClick={onDelete} />
}

export default DeleteEntityModal