import React, { useEffect, useState } from 'react';
import { withStyles, Theme } from '@material-ui/core/styles';
import { ClassNameMap } from '@material-ui/core/styles/withStyles';
import { getTree } from '../api/tree-api';
import { ITreeEntity } from '../interfaces/ITreeEntity';
import { Grid, Typography, Modal } from '@material-ui/core';
import SideTree from './SideTree';
import { ENUMModalType } from '../../enums/ENUMModalType';
import EditPermissionsModal from './modals/EditPermissionsModal';
import { IOpenModalParams } from '../interfaces/modal-interafaces';
import TreeContextMenu from './contextMenu/TreeContextMenu';


interface IFilesTreeProps {
    classes: ClassNameMap
}
interface IFilesTreeState {
    password: string,
    name: string,
    showPassword: boolean,
    errorMessage: string,
    showErrorMessage: boolean
}

const styles = (theme: Theme) => ({

});

const ModalTypes = {
    [ENUMModalType.EditPermissions]: EditPermissionsModal
}

const FilesTree = (props: IFilesTreeProps) => {

    const [tree, setTree] = useState<ITreeEntity[] | null>(null);
    const [open, setOpen] = React.useState(false);
    const [modalBody, setModalBody] = useState<ENUMModalType>(ENUMModalType.EditPermissions);
    const [modalEntity, setModalEntity] = useState<ITreeEntity | null>(null);

    const handleOpen = ({ entity, modalType }: IOpenModalParams) => {
        setModalBody(modalType);
        setModalEntity(entity);
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
    };

    useEffect(() => {
        getTree().then((data) => {
            setTree(data)
        })
    }, [])

    return (
        <div>
            <Grid container>
                <Grid item xs={12}>
                    <Typography variant={'h1'}>
                        {'עץ תיקיות'}
                    </Typography>
                </Grid>
                <Grid item xs={12}>
                    <Grid xs={3}>
                        <SideTree tree={tree} openModal={handleOpen} />
                    </Grid>
                    <Grid xs={9}>

                    </Grid>
                </Grid>
            </Grid>
            <Modal
                open={open}
                onClose={handleClose}
            >
                {React.createElement(ModalTypes[modalBody], { entity: modalEntity, closeModal: handleClose })}
            </Modal>
            <TreeContextMenu />
        </div >
    )
}

export default withStyles(styles, { withTheme: true })(FilesTree);
