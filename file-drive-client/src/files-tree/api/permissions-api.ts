import { ENUMPermissionType } from './../../enums/ENUMPermissionType';
import { User } from './../../models/User';
import { IGetPermissionsRequestParams, IGetUsersRequestParams, IDeletePermissionRequestParams, IEditPermissionRequestParams, IAddPermissionRequestParams, IAddPermissionRequestData } from './../interfaces/permission-request-interfaces';
import { Permission } from './../../models/Permission';
import { AxiosRequest } from './../../utils/api/axios-request';
import { appConfig } from './../../appConfig';

export class PermissionsAPI {
    private constructor() { }

    public static async getPermissions(entityId: number) {
        const response = await AxiosRequest.get<IGetPermissionsRequestParams, Permission[]>({
            url: appConfig.baseUrl + "/api/permissions/getByEntity",
            urlParams: { entityId }
        });

        return response;
    }

    public static async getUsers() {
        const response = await AxiosRequest.get<IGetUsersRequestParams, User[]>({
            url: appConfig.baseUrl + "/api/permissions/getUsers",
        });

        return response;
    }

    public static async deletePermission(permissionId: number) {
        const response = await AxiosRequest.get<IDeletePermissionRequestParams, boolean>({
            url: appConfig.baseUrl + "/api/permissions/delete",
            urlParams: { permissionId }
        });

        return response;
    }

    public static async editPermission(permissionId: number, permissionType: ENUMPermissionType) {
        const response = await AxiosRequest.get<IEditPermissionRequestParams, boolean>({
            url: appConfig.baseUrl + "/api/permissions/update",
            urlParams: { permissionId, permissionType }
        });

        return response;
    }

    public static async addPermission(userId: number, entityId: number, permissionType: ENUMPermissionType) {
        const response = await AxiosRequest.post<IAddPermissionRequestParams, IAddPermissionRequestData, boolean>({
            url: appConfig.baseUrl + "/api/permissions/add",
            urlParams: {},
            data: { userId, entityId, permissionType }
        });

        return response;
    }
}