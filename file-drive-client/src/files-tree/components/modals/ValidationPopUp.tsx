import { Button, Card, CardActions, CardContent, makeStyles, Theme } from '@material-ui/core'
import React from 'react'

const useStyles = makeStyles((theme: Theme) => ({
    root: {
        width: '30%',
        margin: 'auto'
    }
}));

const ValidationPopUp = (props: any) => {
    const {onClose, onClick} = props
    const classes = useStyles()
    return (
        <Card classes={{
            root: classes.root
        }}>
            <CardContent>
                {props.children ? props.children : "Are you sure you want to do it?"}
            </CardContent>
            <CardActions>
                <Button onClick={onClose}>Cancel</Button>
                <Button onClick={onClick} variant={"contained"}>Confirm</Button>
            </CardActions>
        </Card>
    )    
}

export default ValidationPopUp