export class UserService {
    static getCurrentUser = () => {
        const user = localStorage.getItem("user");
        return user ? JSON.parse(user) : null;
    }

    static setCurrentUser = (userObject: any) => {
        localStorage.setItem("user", JSON.stringify(userObject));
    }
}