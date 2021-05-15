import React from 'react';
import { makeStyles, Theme } from '@material-ui/core/styles';
import { ITreeEntity } from '../interfaces/ITreeEntity';
import { TreeItem, TreeView } from '@material-ui/lab';
import { DescriptionOutlined,  FolderTwoTone } from '@material-ui/icons';
import { TreeContextMenuTrigger } from './contextMenu/TreeContextMenuTrigger';
import { IOpenModalParams } from '../interfaces/modal-interafaces';
import { ENUMConverterType } from '../../enums/ENUMConverterType';
import { Drawer } from '@material-ui/core';



interface ISideTreeProps {
    tree: ITreeEntity[] | null,
    openModal: (params: IOpenModalParams) => void,
    onTreeItemClick: (entity: ITreeEntity) => void,
    handleDuplicate: (entity: ITreeEntity) => void,
    download: (entity: ITreeEntity) => void,
    handleConvert: (entity: ITreeEntity, conversionType: ENUMConverterType) => void
}

const useStyles = makeStyles((theme: Theme) => ({
    drawer: {
        backgroundColor: theme.palette.primary.main,
        padding: 10,
        width: 300,
    },
    tree: {
        textAlign: 'left'
    },
    logo: {
        margin: "0 auto",
        height: 250,
        width: 250
    },
    label: {
        fontSize: theme.typography.h6.fontSize
    }
}));

const collect = (props: any) => {
    return props
}

const SideTree = (props: ISideTreeProps) => {
    const { tree, openModal, onTreeItemClick, handleDuplicate, handleConvert, download } = props
    const classes = useStyles()

    const onClick = (treeEntity: ITreeEntity) => () => {
        onTreeItemClick && onTreeItemClick(treeEntity)
    }
    const buildTree = (currentTreeEntity: ITreeEntity) => {
        if (currentTreeEntity)
            return (
                <TreeContextMenuTrigger id="context-menu" collect={collect} entity={currentTreeEntity} download={download} handleDuplicate={handleDuplicate} handleConvert={handleConvert} openModal={openModal}>
                    <TreeItem
                        nodeId={currentTreeEntity.id.toString()}
                        label={currentTreeEntity.name}
                        classes={{
                            label: classes.label
                        }}
                        icon={currentTreeEntity.file ? <DescriptionOutlined /> : <FolderTwoTone />}
                        onClick={onClick(currentTreeEntity)}>
                        {
                            currentTreeEntity.children && currentTreeEntity.children.map(child => buildTree(child))
                        }
                    </TreeItem>
                </TreeContextMenuTrigger>
            )
    }

    return (
        <Drawer variant="permanent" classes={{paper: classes.drawer}}>
        <img alt={"logo"} className={classes.logo} src={"logo_transparent.png"}/>
        <TreeView classes={{ root: classes.tree }}>
            {
                tree?.map(child => buildTree(child))
            }
        </TreeView>
        </Drawer>
    )
}

export default SideTree;
