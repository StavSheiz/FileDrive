import { Grid, Typography } from '@material-ui/core';
import { Theme, withStyles } from '@material-ui/core/styles';
import { DescriptionOutlined, FolderTwoTone } from '@material-ui/icons';
import React from 'react';
import { ENUMConverterType } from '../../enums/ENUMConverterType';
import { ITreeEntity } from '../interfaces/ITreeEntity';
import { IOpenModalParams } from '../interfaces/modal-interafaces';
import AddTreeEntity from './AddTreeEntity';
import { TreeContextMenuTrigger } from './contextMenu/TreeContextMenuTrigger';

interface ICurrentTreeEntityProps {
    entity?: ITreeEntity,
    onAddFile: (file: File) => void,
    onAddFolder: (folderName: string) => void
    openModal: (params: IOpenModalParams) => void,
    handleDuplicate: (entity: ITreeEntity) => void,
    download: (entity: ITreeEntity) => void,
    handleConvert: (entity: ITreeEntity, conversionType: ENUMConverterType) => void
}

const styles = (theme: Theme) => ({

});

const CurrentTreeEntity = (props: ICurrentTreeEntityProps) => {

    const { entity, onAddFile, onAddFolder, openModal, handleDuplicate, handleConvert, download } = props

    const collect = (props: any) => {
        return props
    }


    return (
        <Grid container justify={"flex-start"} alignItems={"center"} spacing={5}>
            {entity && entity.children ? entity.children.map(child => {
                return (
                    <Grid item xs={3} key={child.id}>
                        <TreeContextMenuTrigger id="context-menu" collect={collect} entity={child} handleDuplicate={handleDuplicate} handleConvert={handleConvert} download={download} openModal={openModal}>
                            {child.file ? <DescriptionOutlined /> : <FolderTwoTone />}
                            <Typography>{child.name}</Typography>
                    </TreeContextMenuTrigger>
                        </Grid>
                )
            }) : undefined}
            {
                entity ?
                    <Grid item xs={3}>
                        <AddTreeEntity onAddFolder={onAddFolder} onAddFile={onAddFile} />
                    </Grid> : undefined
            }
        </Grid>
    )
}

export default withStyles(styles, { withTheme: true })(CurrentTreeEntity);
