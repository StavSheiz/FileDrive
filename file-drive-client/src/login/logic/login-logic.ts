import { loginErrorMessage } from './login-error-messages';
import { LoginAPI } from './../api/login-api';
import { ENUMExceptionCodes } from '../../enums/ENUMExceptionCodes';


export class LoginLogic {
    private constructor() { }

    public static async signIn(name: string, password: string) {
        const response = await LoginAPI.signIn(name, password);

        if (response.exception) {
            let message;

            switch (response.exception.exceptionCode) {
                case ENUMExceptionCodes.UserDoesNotExist: {
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

        // TODO: Redirect here to homepage
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

        // TODO: Redirect here to login
    }
}
