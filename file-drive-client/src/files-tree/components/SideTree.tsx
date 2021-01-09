import React from 'react';
import { makeStyles, Theme } from '@material-ui/core/styles';
import { ITreeEntity } from '../interfaces/ITreeEntity';
import { TreeItem, TreeView } from '@material-ui/lab';
import { Folder } from '@material-ui/icons';


interface ISideTreeProps {
tree: ITreeEntity[] | null
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

const SideTree = (props: ISideTreeProps) => {
    const {tree} = props
    const classes = useStyles()
    const buildTree = (currentTreeEntity: ITreeEntity) => {
        if (currentTreeEntity)
        return <TreeItem nodeId={currentTreeEntity.id.toString()} label={currentTreeEntity.name} icon={/* check if folder or file */<Folder />}>
            {
                currentTreeEntity.children && currentTreeEntity.children.map(child => buildTree(child))
            }
        </TreeItem>
    }

    return <TreeView classes={{root: classes.root}}>
        {
            tree?.map(child => buildTree(child))
        }
    </TreeView>
}

export default SideTree;
