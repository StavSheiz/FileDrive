import { Permission } from './../../models/Permission';
import { User } from './../../models/User';
import { ENUMPermissionType } from './../../enums/ENUMPermissionType';
import { permissionsErrorMessage } from './permissions-error-messages';
import { ENUMExceptionCodes } from './../../enums/ENUMExceptionCodes';
import { PermissionsAPI } from './../api/permissions-api';
import { ITreeEntity } from './../interfaces/ITreeEntity';

export class PermissionsLogic {
    public static async getPermissions(entity: ITreeEntity) {
        const response = await PermissionsAPI.getPermissions(entity.id);

        if (response.exception) {
            let message;

            switch (response.exception.exceptionCode) {
                default: {
                    message = permissionsErrorMessage[ENUMExceptionCodes.RequestError];
                    break;
                }
            }

            return { data: null, message }
        }

        return { data: response.data, message: null }
    }

    public static async getUsers() {
        const response = await PermissionsAPI.getUsers();

        if (response.exception) {
            let message;

            switch (response.exception.exceptionCode) {
                default: {
                    message = permissionsErrorMessage[ENUMExceptionCodes.RequestError];
                    break;
                }
            }

            return { data: null, message }
        }

        return { data: response.data, message: null }
    }

    public static async DeletePermission(permission: Permission) {
        const response = await PermissionsAPI.deletePermission(permission.id);

        if (response.exception) {
            let message;

            switch (response.exception.exceptionCode) {
                default: {
                    message = permissionsErrorMessage[ENUMExceptionCodes.RequestError];
                    break;
                }
            }

            return message
        }
    }

    public static async EditPermission(permission: Permission) {
        const newPermissionType = permission.permissionType === ENUMPermissionType.View ? ENUMPermissionType.Edit : ENUMPermissionType.View;

        const response = await PermissionsAPI.editPermission(permission.id, newPermissionType);

        if (response.exception) {
            let message;

            switch (response.exception.exceptionCode) {
                default: {
                    message = permissionsErrorMessage[ENUMExceptionCodes.RequestError];
                    break;
                }
            }

            return message
        }
    }

    public static async AddPermission(user: User, entity: ITreeEntity, permissionType: ENUMPermissionType) {
        const response = await PermissionsAPI.addPermission(user.id, entity.id, permissionType);

        if (response.exception) {
            let message;

            switch (response.exception.exceptionCode) {
                default: {
                    message = permissionsErrorMessage[ENUMExceptionCodes.RequestError];
                    break;
                }
            }

            return message
        }
    }

}