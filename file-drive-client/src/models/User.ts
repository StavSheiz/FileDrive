import { ENUMUserType } from '../enums/ENUMUserType';

export class User {
    public id: number;
    public name: string;
    public userType: ENUMUserType

    public constructor(id: number, name: string, userType: ENUMUserType) {
        this.id = id;
        this.name = name;
        this.userType = userType;
    }
}