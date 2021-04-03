import React, { useEffect, useState } from 'react';
import { addFile, addFolder, duplicateFile, getTree } from '../api/tree-api';
import { ITreeEntity } from '../interfaces/ITreeEntity';
import { Grid, Typography, Modal } from '@material-ui/core';
import SideTree from './SideTree';
import { ENUMModalType } from '../../enums/ENUMModalType';
import EditPermissionsModal from './modals/EditPermissionsModal';
import { IOpenModalParams } from '../interfaces/modal-interafaces';
import TreeContextMenu from './contextMenu/TreeContextMenu';
import CurrentTreeEntity from './CurrentTreeEntity';
import { AddTreeEntityToTree, UpdateTreeEntity } from '../helpers/tree-helpers';
import useErrorContext from '../../errors/ErrorContext';
import DeleteEntityModal from './modals/DeleteEntityModal';
import RenameEntityModal from './modals/RenameEntityModal';
import DetailsModal from './modals/DetailsModal';
import { ENUMConverterType } from '../../enums/ENUMConverterType';
import { ConversionLogic } from '../logic/conversion-logic';
import { b64toBlob } from '../helpers/file-helpers';

interface IFilesTreeProps {
}

const ModalTypes = {
    [ENUMModalType.EditPermissions]: EditPermissionsModal,
    [ENUMModalType.Delete]: DeleteEntityModal,
    [ENUMModalType.Rename]: RenameEntityModal,
    [ENUMModalType.Details]: DetailsModal
}

const FilesTree = (props: IFilesTreeProps) => {

    const [tree, setTree] = useState<ITreeEntity[] | null>(null);
    const [selectedTreeEntity, setSelectedTreeEntity] = useState<ITreeEntity | undefined>(undefined);
    const [open, setOpen] = React.useState(false);
    const [modalBody, setModalBody] = useState<ENUMModalType>(ENUMModalType.EditPermissions);
    const [modalEntity, setModalEntity] = useState<ITreeEntity | null>(null);
    const { setException } = useErrorContext()

    const onTreeEntitySelect = (entity: ITreeEntity) => {
        setSelectedTreeEntity(entity);
    }

    const onAddFile = async (file: File) => {
        try {
            const formData = new FormData()
            formData.append('uploadedFile', file)
            formData.append('parentId', (selectedTreeEntity as ITreeEntity).id.toString())
            const { data, exception } = await addFile(formData)
            if (exception) {
                setException(exception)
            } else if (data) {
                const newTreeEntity = data
                setTree(prevTree => {
                    let newTree = [...(prevTree as ITreeEntity[])]
                    newTree.forEach(child => AddTreeEntityToTree(child, newTreeEntity))
                    return newTree
                })
            }
        } catch (error) {
            console.error(error)
        }
    }

    const onAddFolder = async (folderName: string) => {
        try {
            const { data, exception } = await addFolder(folderName, (selectedTreeEntity as ITreeEntity).id)

            if (exception) {
                setException(exception)
            } else if (data) {
                const newTreeEntity = { ...data, children: []}
                setTree(prevTree => {
                    let newTree = [...(prevTree as ITreeEntity[])]
                    newTree.forEach(child => AddTreeEntityToTree(child, newTreeEntity))
                    return newTree
                })
            }
        } catch (error) {
            console.error(error)
        }
    }

    const handleDuplicate = async (entity: ITreeEntity) => {
        try {

            const { data, exception } = await duplicateFile(entity.id);

            if (exception) {
                setException(exception)
            } else if (data) {
                const newTreeEntity = data
                setTree(prevTree => {
                    let newTree = [...(prevTree as ITreeEntity[])]
                    newTree.forEach(child => AddTreeEntityToTree(child, newTreeEntity))
                    return newTree
                })
            }

        } catch (error) {
            console.error(error)
        }
    }

    const handleDownload = async (entity: ITreeEntity) => {
        const url = window.URL.createObjectURL(b64toBlob(entity.file));
        const a = document.createElement('a');
        a.style.display = 'none';
        a.href = url;
        // the filename you want
        a.download = entity.name;
        document.body.appendChild(a);
        a.click();
        window.URL.revokeObjectURL(url);
    }

    const handleConvert = async (entity: ITreeEntity, conversionType: ENUMConverterType) => {
        try {

            const { data, exception } = await ConversionLogic.convertFile(entity.id, conversionType);;

            if (exception) {
                setException(exception)
            } else if (data) {
                const entityToUpdate = data
                setTree(prevTree => {
                    let newTree = [...(prevTree as ITreeEntity[])]
                    newTree.forEach(child => UpdateTreeEntity(child, entityToUpdate))
                    return newTree
                })
            }

        } catch (error) {
            console.error(error)
        }
    }

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
                <Grid container item xs={12}>
                    <Grid xs={3}>
                        <SideTree tree={tree} openModal={handleOpen} download={handleDownload} handleDuplicate={handleDuplicate} handleConvert={handleConvert} onTreeItemClick={onTreeEntitySelect} />
                    </Grid>
                    <Grid xs={9}>
                        <CurrentTreeEntity openModal={handleOpen} download={handleDownload} entity={selectedTreeEntity} handleDuplicate={handleDuplicate} handleConvert={handleConvert} onAddFile={onAddFile} onAddFolder={onAddFolder} />
                    </Grid>
                </Grid>
            </Grid>
            <Modal
                open={open}
                onClose={handleClose}>
                {React.createElement(ModalTypes[modalBody], { entity: modalEntity, closeModal: handleClose, setTree: setTree })}
            </Modal>
            <TreeContextMenu />
        </div >
    )
}

export default FilesTree;
