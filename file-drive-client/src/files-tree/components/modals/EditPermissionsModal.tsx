import React from 'react';
import { Theme, withStyles } from "@material-ui/core";
import { ITreeEntity } from "../../interfaces/ITreeEntity";
import { IBaseModalInterface } from '../../interfaces/modal-interafaces';


interface IEditPermissionsModalProps extends IBaseModalInterface { }
interface IEditPermissionsModalState { }

const styles = (theme: Theme) => ({

});

class EditPermissionsModal extends React.Component<IEditPermissionsModalProps, IEditPermissionsModalState> {


    render() {
        const { entity } = this.props;

        return (
            <div>hi</div>
        )
    }
}

export default withStyles(styles, { withTheme: true })(EditPermissionsModal)
