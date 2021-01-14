import React from 'react';
import { makeStyles, Theme } from '@material-ui/core/styles';
import { ITreeEntity } from '../interfaces/ITreeEntity';
import { TreeItem, TreeView } from '@material-ui/lab';
import { Folder } from '@material-ui/icons';
import { TreeContextMenuTrigger } from './contextMenu/TreeContextMenuTrigger';
import { IOpenModalParams } from '../interfaces/modal-interafaces';



interface ISideTreeProps {
    tree: ITreeEntity[] | null,
    openModal: (params: IOpenModalParams) => void
}
interface ISideTreeState {
    password: string,
    name: string,
    showPassword: boolean
    errorMessage: string,
    showErrorMessage: boolean
}

const useStyles = makeStyles((theme: Theme) => ({
    root: {
        textAlign: 'left'
    }
}));

const collect = (props: any) => {
    return props
}

const SideTree = (props: ISideTreeProps) => {
    const { tree, openModal } = props
    const classes = useStyles()
    const buildTree = (currentTreeEntity: ITreeEntity) => {
        if (currentTreeEntity)
            return (
                <TreeContextMenuTrigger id="context-menu" collect={collect} entity={currentTreeEntity} openModal={openModal}>
                    <TreeItem nodeId={currentTreeEntity.id.toString()} label={currentTreeEntity.name} icon={/* check if folder or file */<Folder />}>
                        {
                            currentTreeEntity.children && currentTreeEntity.children.map(child => buildTree(child))
                        }
                    </TreeItem>
                </TreeContextMenuTrigger>
            )
    }

    return (
        <TreeView classes={{ root: classes.root }}>
            {
                tree?.map(child => buildTree(child))
            }
        </TreeView>
    )
}

export default SideTree;
