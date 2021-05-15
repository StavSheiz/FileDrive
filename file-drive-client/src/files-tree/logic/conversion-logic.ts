import { convertFile } from './../api/tree-api';
import { ENUMConverterType } from './../../enums/ENUMConverterType';

export class ConversionLogic {
    private static extensionToConversionMap: { [key: string]: ENUMConverterType[] } = {
        'png': [ENUMConverterType.PNGToJPG],
        'jpg': [ENUMConverterType.JPGToPNG],
        'doc': [ENUMConverterType.WORDToPDF],
        'docx': [ENUMConverterType.WORDToPDF],
        'pdf': [ENUMConverterType.PDFToWORD],
    }

    public static getAvailableConversionTypes(fileName: string): ENUMConverterType[] {
        const extension = ConversionLogic.getFileExtension(fileName)

        return ConversionLogic.extensionToConversionMap[extension.toLowerCase()] || [];
    }

    public static getFileExtension(fileName: string) {
        const splitName = fileName.split('.');
        const extension = splitName[splitName.length - 1];

        return extension;
    }

    public static async convertFile(fileId: number, conversionType: ENUMConverterType) {
        const response = await convertFile(fileId, conversionType)

        return response
    }
}