export class Logger {
    private constructor() { }

    public static info(message: string) {
        console.log(`${new Date()} | INFO | ${message}`)
    }

    public static warning(message: string) {
        console.warn(`${new Date()} | WARNING | ${message}`)
    }

    public static error(message: string, ex: Error) {
        console.error(`${new Date()} | ERROR | ${message} | ${ex}`)
    }
}