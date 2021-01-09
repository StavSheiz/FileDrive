import React, { useEffect, useState } from 'react';
import { withStyles, Theme } from '@material-ui/core/styles';
import { ClassNameMap } from '@material-ui/core/styles/withStyles';
import { getTree } from './api/tree-api';
import { ITreeEntity } from './interfaces/ITreeEntity';
import { Grid, Typography } from '@material-ui/core';
import SideTree from './components/SideTree';


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

const FilesTree = (props: IFilesTreeProps) => {

    const [tree, setTree] = useState<ITreeEntity[] | null>(null);

    useEffect(() => {
        getTree().then((data) => {
            setTree(data)
        })
    })

    return <Grid container>
        <Grid item xs={12}>
            <Typography variant={'h1'}>
                {'עץ תיקיות'}
            </Typography>
        </Grid>
        <Grid item xs={12}>
            <Grid xs={3}>
                <SideTree tree={tree} />
            </Grid>
            <Grid xs={9}>

            </Grid>
        </Grid>
    </Grid>
}

export default withStyles(styles, { withTheme: true })(FilesTree);
