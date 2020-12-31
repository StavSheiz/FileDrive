import { baseErrorMessage } from './../../consts/base-error-messages';
import { ENUMExceptionCodes } from './../../enums/ENUMExceptionCodes';

export const loginErrorMessage = {
    ...baseErrorMessage,
    [ENUMExceptionCodes.UserDoesNotExist]: 'Username or password does not exist',
}