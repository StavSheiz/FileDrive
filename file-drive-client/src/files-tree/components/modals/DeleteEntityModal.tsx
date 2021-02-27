import React from "react";
import { deleteTreeEntity } from "../../api/tree-api";
import ValidationPopUp from './ValidationPopUp'

const DeleteEntityModal = (props: any) => {
    const { closeModal, entity, classes } = props;
    const onDelete = () => {
        deleteTreeEntity(entity.id)
        closeModal()
    }
    return <ValidationPopUp onClose={closeModal} onClick={onDelete} />
}

export default DeleteEntityModal