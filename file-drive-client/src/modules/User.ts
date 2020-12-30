import { ENUMUserType } from '../enums/ENUMUserType';

export class User {
    public id: string;
    public name: string;
    public userType: ENUMUserType

    public constructor(id: string, name: string, userType: ENUMUserType) {
        this.id = id;
        this.name = name;
        this.userType = userType;
    }
}