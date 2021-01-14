import { ContextMenu, MenuItem, connectMenu, ConnectMenuProps } from "react-contextmenu";
import React from 'react';
import { Theme, withStyles } from "@material-ui/core";
import { UserService } from "../../../login/logic/user-service";
import { ENUMModalType } from '../../../enums/ENUMModalType';
import { IOpenModalParams } from "../../interfaces/modal-interafaces";
import './cotext.css'

interface ITreeContextMenuState { }

const styles = (theme: Theme) => ({

});


class ConnectedMenu extends React.Component<ConnectMenuProps, ITreeContextMenuState> {
    handleClick = (event: any, data: IOpenModalParams) => {
        this.props.trigger.openModal(data);
    }

    render() {
        const { trigger } = this.props;
        const user = UserService.getCurrentUser();

        return (
            <ContextMenu id="context-menu">
                {trigger && user && trigger.entity.owner.id === user.id &&
                    < MenuItem data={{ entity: trigger.entity, modalType: ENUMModalType.EditPermissions }} onClick={this.handleClick}>
                        Edit Permissions
                    </MenuItem>
                }
            </ContextMenu>
        )
    }
}

const TreeContextMenu = connectMenu("context-menu")(ConnectedMenu);


export default TreeContextMenu
