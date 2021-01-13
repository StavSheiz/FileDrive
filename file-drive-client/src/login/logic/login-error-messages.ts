import { baseErrorMessage } from './../../consts/base-error-messages';
import { ENUMExceptionCodes } from './../../enums/ENUMExceptionCodes';

export const loginErrorMessage = {
    ...baseErrorMessage,
    [ENUMExceptionCodes.ObjectDoesNotExist]: 'Username or password does not exist',
    [ENUMExceptionCodes.UserNameExists]: 'Username already taken, try a different one',
    [ENUMExceptionCodes.InvalidPassword]: 'Invalid password. Password must contain at least 8 characters',
    [ENUMExceptionCodes.PasswordNotMatching]: 'Confirmation password is incorrect, make sure it matches the password'
}