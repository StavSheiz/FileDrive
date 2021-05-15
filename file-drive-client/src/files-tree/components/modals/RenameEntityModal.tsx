import { TextField } from "@material-ui/core";
import React, { useState } from "react";
import { renameEntity } from "../../api/tree-api";
import { ITreeEntity } from "../../interfaces/ITreeEntity";
import { IBaseModalProps } from "../../interfaces/modal-interafaces";
import ValidationPopUp from './ValidationPopUp';

const RenameEntityModal = (props: IBaseModalProps) => {
    const { closeModal, entity, setTree } = props;
    const [newName, setNewName] = useState("")

    const renameEntityInTree = (treeLevel: ITreeEntity[], entityId: number, newName: string) => {
        const entityToRename = treeLevel.find(entity => entity.id === entityId)

        if (entityToRename){            
            entityToRename.name = newName
            return
        } else {
            treeLevel.forEach(treeEntity => 
                renameEntityInTree(treeEntity.children, entityId, newName))
            return
        }
    }

    const onRename = () => {
        renameEntity(newName, (entity as ITreeEntity).id)
        setTree && setTree((prev: ITreeEntity[] | null) => {
            renameEntityInTree(prev as ITreeEntity[], (entity as ITreeEntity).id, newName)
            return prev as ITreeEntity[]
        })
        closeModal()
    }

    return <ValidationPopUp onClose={closeModal} onClick={onRename}>
        <TextField name="New name" label="New Name" onChange={e => setNewName(e.target.value)}/>
    </ValidationPopUp>
}

export default RenameEntityModal