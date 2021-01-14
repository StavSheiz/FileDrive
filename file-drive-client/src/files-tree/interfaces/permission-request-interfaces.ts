import { ENUMPermissionType } from './../../enums/ENUMPermissionType';
export interface IGetPermissionsRequestParams {
    entityId: number
}

export interface IGetUsersRequestParams { }

export interface IDeletePermissionRequestParams {
    permissionId: number
}

export interface IEditPermissionRequestParams {
    permissionId: number,
    permissionType: ENUMPermissionType
}

export interface IAddPermissionRequestData {
    entityId: number,
    userId: number,
    permissionType: ENUMPermissionType
}

export interface IAddPermissionRequestParams { }
