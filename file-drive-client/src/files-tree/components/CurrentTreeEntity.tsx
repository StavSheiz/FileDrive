import React, { useState } from 'react';
import { withStyles, Theme } from '@material-ui/core/styles';
import { ITreeEntity } from '../interfaces/ITreeEntity';
import { Grid, Typography } from '@material-ui/core';
import { Description, Folder } from '@material-ui/icons';
import AddTreeEntity from './AddTreeEntity';
import { TreeContextMenuTrigger } from './contextMenu/TreeContextMenuTrigger';
import { IOpenModalParams } from '../interfaces/modal-interafaces';
import { ENUMConverterType } from '../../enums/ENUMConverterType';

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
        <Grid container justify={"space-around"}>
            {entity && entity.children ? entity.children.map(child => {
                return (
                    <TreeContextMenuTrigger id="context-menu" collect={collect} entity={child} handleDuplicate={handleDuplicate} handleConvert={handleConvert} download={download} openModal={openModal}>
                        <Grid item xs={3} key={child.id}>
                            {
                                child.file ? <Description /> : <Folder />
                            }
                            <Typography>{child.name}</Typography>
                        </Grid>
                    </TreeContextMenuTrigger>
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
