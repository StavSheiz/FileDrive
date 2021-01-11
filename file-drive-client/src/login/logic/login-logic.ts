import { loginErrorMessage } from './login-error-messages';
import { LoginAPI } from './../api/login-api';
import { ENUMExceptionCodes } from '../../enums/ENUMExceptionCodes';
import { UserService } from './user-service';


export class LoginLogic {
    public static async signIn(name: string, password: string) {
        const response = await LoginAPI.signIn(name, password);

        if (response.exception) {
            let message;

            switch (response.exception.exceptionCode) {
                case ENUMExceptionCodes.UserDoesNotExist:
                case ENUMExceptionCodes.InvalidParameters: {
                    message = loginErrorMessage[ENUMExceptionCodes.UserDoesNotExist];
                    break;
                }
                default: {
                    message = loginErrorMessage[ENUMExceptionCodes.RequestError];
                    break;
                }
            }

            return message;
        }

        UserService.setCurrentUser({ name })
    }

    public static async signOut() {
        const response = await LoginAPI.signOut();

        if (response.exception) {
            let message;

            switch (response.exception.exceptionCode) {
                default: {
                    message = loginErrorMessage[ENUMExceptionCodes.RequestError];
                    break;
                }
            }

            return message;
        }

        UserService.setCurrentUser({})
    }

    public static async signUp(name: string, password: string, confirmPassword: string) {
        const response = await LoginAPI.addUser(name, password, confirmPassword);

        if (response.exception) {
            let message;

            switch (response.exception.exceptionCode) {
                case ENUMExceptionCodes.UserNameExists: {
                    message = loginErrorMessage[ENUMExceptionCodes.UserNameExists];
                    break;
                }
                case ENUMExceptionCodes.InvalidParameters: {
                    message = loginErrorMessage[ENUMExceptionCodes.InvalidParameters];
                    break;
                }
                case ENUMExceptionCodes.InvalidPassword: {
                    message = loginErrorMessage[ENUMExceptionCodes.InvalidPassword];
                    break;
                }
                case ENUMExceptionCodes.PasswordNotMatching: {
                    message = loginErrorMessage[ENUMExceptionCodes.PasswordNotMatching];
                    break;
                }
                default: {
                    message = loginErrorMessage[ENUMExceptionCodes.RequestError];
                    break;
                }
            }

            return message
        }

        // TODO: Redirect to login
    }
}
