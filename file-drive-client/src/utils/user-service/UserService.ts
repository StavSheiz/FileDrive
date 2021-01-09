export class UserService {
    static getCurrentUser = () => {
        return localStorage.getItem("user");
    }

    static setCurrentUser = (userObject: any) => {
        localStorage.setItem("user", userObject);
    }
}