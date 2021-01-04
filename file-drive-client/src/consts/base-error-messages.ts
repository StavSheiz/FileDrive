import { ENUMExceptionCodes } from './../enums/ENUMExceptionCodes';

export const baseErrorMessage = {
    [ENUMExceptionCodes.RequestError]: 'Oops... something went wrong. try again later',
    [ENUMExceptionCodes.InvalidParameters]: 'One or more field is invalid. Make sure all mandatory field are filled'
}