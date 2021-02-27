import { TextField } from "@material-ui/core";
import React from "react";
import { deleteTreeEntity } from "../../api/tree-api";
import ValidationPopUp from './ValidationPopUp'

const RenameEntityModal = (props: any) => {
    const { closeModal, entity, classes } = props;
    const onRename = () => {
        deleteTreeEntity(entity.id)
        closeModal()
    }
    return <ValidationPopUp onClose={closeModal} onClick={onRename}>
        <TextField name="New name" label="New Name" />
    </ValidationPopUp>
}

export default RenameEntityModal