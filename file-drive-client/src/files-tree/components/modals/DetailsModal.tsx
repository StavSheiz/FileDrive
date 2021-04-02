import React from 'react';
import { Theme, withStyles, TableContainer, Table, TableHead, TableRow, TableCell, Paper, TableBody } from "@material-ui/core";
import { IBaseModalProps } from '../../interfaces/modal-interafaces';
import { ClassNameMap } from '@material-ui/core/styles/withStyles';


interface IDetailsModalProps extends IBaseModalProps {
    classes: ClassNameMap,
}
interface IDetailsModalState {
}

const styles = (theme: Theme) => ({
    paper: {
        width: 400,
        backgroundColor: theme.palette.background.paper,
        border: '2px solid #000',
        boxShadow: theme.shadows[5],
        padding: theme.spacing(2, 4, 3),
    },
    errorPaper: {
        backgroundColor: '#cb2431',
        padding: theme.spacing(1),
    }
});

class DetailsModal extends React.Component<IDetailsModalProps, IDetailsModalState> {
    state: IDetailsModalState = {
    }

    getModalStyle = () => {

        return {
            top: `30%`,
            left: `50%`,
            transform: `translate(-50%, -50%)`,
        };
    }



    componentDidMount() {
    }

    render() {
        const { entity, classes } = this.props;

        return (
            <div style={{ ...(this.getModalStyle()), position: 'absolute' }}>
                <TableContainer component={Paper}>
                    <Table className={classes.table} aria-label="simple table">
                        <TableHead>
                            <TableRow>
                                <TableCell>{entity?.file === null ? 'Folder name' : 'File name'}</TableCell>
                                <TableCell >Owner</TableCell>
                                <TableCell >Size</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            <TableRow key={entity?.id}>
                                <TableCell component="th" scope="row">{entity?.name}</TableCell>
                                <TableCell >{entity?.owner.name}</TableCell>
                                <TableCell >{entity?.size}</TableCell>
                            </TableRow>
                        </TableBody>
                    </Table>
                </TableContainer>
            </ div>
        )
    }
}

export default withStyles(styles, { withTheme: true })(DetailsModal)
