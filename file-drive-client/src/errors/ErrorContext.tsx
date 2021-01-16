import React, { createContext, useState, useContext } from 'react'
import { ENUMExceptionCodes } from '../enums/ENUMExceptionCodes'
import { loginErrorMessage } from '../login/logic/login-error-messages'
import { IResponseException } from '../utils/api/interfaces/response-interfaces'
interface IErrorContext {
    error: string | null,
    setError: (error: string | null) => void,
    setException: (error: IResponseException) => void 
}

const ErrorContext: React.Context<IErrorContext> = createContext({} as IErrorContext)
const { Provider } = ErrorContext

const ErrorContextProvider: React.FC = ({ children }) => {

    const [error, setError] = useState(null as any)
    const addError = (exception: IResponseException) => {
        switch (exception?.exceptionCode) {
            case ENUMExceptionCodes.UserNameExists: {
                setError(loginErrorMessage[ENUMExceptionCodes.UserNameExists]);
                break;
            }
            case ENUMExceptionCodes.InvalidParameters: {
                setError(loginErrorMessage[ENUMExceptionCodes.InvalidParameters]);
                break;
            }
            case ENUMExceptionCodes.InvalidPassword: {
                setError(loginErrorMessage[ENUMExceptionCodes.InvalidPassword]);
                break;
            }
            case ENUMExceptionCodes.PasswordNotMatching: {
                setError(loginErrorMessage[ENUMExceptionCodes.PasswordNotMatching]);
                break;
            }
            default: {
                setError(loginErrorMessage[ENUMExceptionCodes.RequestError]);
                break;
            }
        }
    }

    return <Provider value={{ error, setError, setException: addError }}>        
        {children}
    </Provider>
}

const useErrorContext = () => useContext(ErrorContext)

export { ErrorContextProvider }

export default useErrorContext