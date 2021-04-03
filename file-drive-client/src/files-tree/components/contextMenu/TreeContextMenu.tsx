import { ContextMenu, MenuItem, connectMenu, ConnectMenuProps } from "react-contextmenu";
import React from 'react';
import { Theme, withStyles } from "@material-ui/core";
import { UserService } from "../../../login/logic/user-service";
import { ENUMModalType } from '../../../enums/ENUMModalType';
import { IOpenModalParams } from "../../interfaces/modal-interafaces";
import './cotext.css'
import { ITreeEntity } from "../../interfaces/ITreeEntity";
import { duplicateFile } from "../../api/tree-api";
import { ConversionLogic } from "../../logic/conversion-logic";
import { ENUMConverterType } from "../../../enums/ENUMConverterType";

interface ITreeContextMenuState { }

const styles = (theme: Theme) => ({

});


class ConnectedMenu extends React.Component<ConnectMenuProps, ITreeContextMenuState> {
    handleClick = (event: any, data: IOpenModalParams) => {
        this.props.trigger.openModal(data);
    }

    handleDuplicate = (event: any, data: { entity: ITreeEntity }) => {
        this.props.trigger.handleDuplicate(data.entity);
    }

    handleDownload = (event: any, data: {entity: ITreeEntity}) => {
        this.props.trigger.download(data.entity)
    }

    convertFile = (event: any, data: { entity: ITreeEntity, conversionType: ENUMConverterType }) => {
        this.props.trigger.handleConvert(data.entity, data.conversionType);
    }

    canConvert = (conversionType: ENUMConverterType, fileName: string) => {
        const conversionTypes = ConversionLogic.getAvailableConversionTypes(fileName);

        return conversionTypes.indexOf(conversionType) !== -1;
    }

    render() {
        const { trigger } = this.props;
        const user = UserService.getCurrentUser();

        return (
            <ContextMenu id="context-menu">
                {trigger && user && trigger.entity?.owner?.id === user.id &&
                <>
                    < MenuItem data={{ entity: trigger.entity, modalType: ENUMModalType.EditPermissions }} onClick={this.handleClick}>
                        Edit Permissions
                    </MenuItem>
                    < MenuItem data={{ entity: trigger.entity, modalType: ENUMModalType.Delete }} onClick={this.handleClick}>
                    Delete
                </MenuItem>
                < MenuItem data={{ entity: trigger.entity, modalType: ENUMModalType.Rename }} onClick={this.handleClick}>
                    Rename
                </MenuItem>
                </>
                }
                {trigger && trigger.entity &&
                    < MenuItem data={{ entity: trigger.entity, modalType: ENUMModalType.Details }} onClick={this.handleClick}>
                        Details
                    </MenuItem>
                }
                {trigger && trigger.entity && trigger.entity.file &&
                    < MenuItem data={{ entity: trigger.entity }} onClick={this.handleDuplicate}>
                        Duplicate
                    </MenuItem>
                }
                {trigger && trigger.entity && trigger.entity.file &&
                    < MenuItem data={{ entity: trigger.entity }} onClick={this.handleDownload}>
                        Download
                    </MenuItem>
                }
                {trigger && trigger.entity && trigger.entity.file && this.canConvert(ENUMConverterType.JPGToPNG, trigger.entity.name) &&
                    < MenuItem data={{ entity: trigger.entity, conversionType: ENUMConverterType.JPGToPNG }} onClick={this.convertFile}>
                        Convert To PNG
                    </MenuItem>
                }
                {trigger && trigger.entity && trigger.entity.file && this.canConvert(ENUMConverterType.PNGToJPG, trigger.entity.name) &&
                    < MenuItem data={{ entity: trigger.entity, conversionType: ENUMConverterType.PNGToJPG }} onClick={this.convertFile}>
                        Convert To JPG
                    </MenuItem>
                }
            </ContextMenu>
        )
    }
}

const TreeContextMenu = connectMenu("context-menu")(ConnectedMenu);


export default TreeContextMenu
