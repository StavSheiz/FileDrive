import React from 'react';
import { makeStyles, Theme } from '@material-ui/core/styles';
import { ITreeEntity } from '../interfaces/ITreeEntity';
import { TreeItem, TreeView } from '@material-ui/lab';
import { Folder, Description } from '@material-ui/icons';
import { TreeContextMenuTrigger } from './contextMenu/TreeContextMenuTrigger';
import { IOpenModalParams } from '../interfaces/modal-interafaces';



interface ISideTreeProps {
    tree: ITreeEntity[] | null,
    openModal: (params: IOpenModalParams) => void,
    onTreeItemClick: (entity: ITreeEntity) => void
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
    const { tree, openModal, onTreeItemClick } = props
    const classes = useStyles()

    const onClick = (treeEntity: ITreeEntity) => () => {
        onTreeItemClick && onTreeItemClick(treeEntity)
    }
    const buildTree = (currentTreeEntity: ITreeEntity) => {
        if (currentTreeEntity)
            return (
                <TreeContextMenuTrigger id="context-menu" collect={collect} entity={currentTreeEntity} openModal={openModal}>
                    <TreeItem 
                        nodeId={currentTreeEntity.id.toString()} 
                        label={currentTreeEntity.name} 
                        icon={currentTreeEntity.file ? <Description /> : <Folder />}
                        onClick={onClick(currentTreeEntity)}>
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
