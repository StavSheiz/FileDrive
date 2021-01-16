import React from 'react';
import { withStyles, Theme } from '@material-ui/core/styles';
import { ITreeEntity } from '../interfaces/ITreeEntity';
import { Grid, Typography } from '@material-ui/core';
import { Description, Folder } from '@material-ui/icons';
import AddTreeEntity from './AddTreeEntity';

interface ICurrentTreeEntityProps {
    entity?: ITreeEntity,
    onAddFile: (file: File) => void,
    onAddFolder: (folderName: string) => void
}

const styles = (theme: Theme) => ({

});

const CurrentTreeEntity = (props: ICurrentTreeEntityProps) => {

    const {entity, onAddFile, onAddFolder} = props

    return (
            <Grid container justify={"space-around"}>
                {entity ? entity.children.map(child => {
                    return <Grid item xs={3} key={child.id}>
                        {
                            child.file ? <Description /> : <Folder />
                        }
                        <Typography>{child.name}</Typography>
                        </Grid>
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
