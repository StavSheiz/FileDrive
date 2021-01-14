import { User } from './../../models/User';

export class UserService {
    static getCurrentUser = () => {
        const user = localStorage.getItem("user");
        return (user ? JSON.parse(user) : null) as User | null;
    }

    static setCurrentUser = (userObject: User) => {
        localStorage.setItem("user", JSON.stringify(userObject));
    }

    static removeUser = () => {
        localStorage.removeItem("user");
    }
}