import { ConversionErrorMessage } from './conversion-error-messages';
import { ENUMExceptionCodes } from './../../enums/ENUMExceptionCodes';
import { convertFile } from './../api/tree-api';
import { ENUMConverterType } from './../../enums/ENUMConverterType';

export class ConversionLogic {
    private static extensionToConversionMap: { [key: string]: ENUMConverterType[] } = {
        'png': [ENUMConverterType.PNGToJPG],
        'jpg': [ENUMConverterType.JPGToPNG]
    }

    public static getAvailableConversionTypes(fileName: string): ENUMConverterType[] {
        const splitName = fileName.split('.');
        const extension = splitName[splitName.length - 1];

        return ConversionLogic.extensionToConversionMap[extension.toLowerCase()];
    }

    public static async convertFile(fileId: number, conversionType: ENUMConverterType) {
        const response = await convertFile(fileId, conversionType)

        if (response.exception) {
            let message;

            switch (response.exception.exceptionCode) {
                case ENUMExceptionCodes.ConversionFailed: {
                    message = response.exception.message;
                    break;
                }
                default: {
                    message = ConversionErrorMessage[ENUMExceptionCodes.RequestError];
                    break;
                }
            }

            return { data: null, message }
        }

        return { data: response.data, message: null }
    }
}