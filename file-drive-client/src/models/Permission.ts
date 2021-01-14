import { ENUMPermissionType } from './../enums/ENUMPermissionType';
import { User } from './../models/User';
import { ITreeEntity } from './../files-tree/interfaces/ITreeEntity';

export class Permission {
    public id: number
    public user: User
    public entity: ITreeEntity
    public permissionType: ENUMPermissionType


    public constructor(id: number, user: User, entity: ITreeEntity, permissionType: ENUMPermissionType) {
        this.id = id;
        this.user = user;
        this.entity = entity;
        this.permissionType = permissionType;
    }
}