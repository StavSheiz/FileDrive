
export interface IRequestParams<TUrlParams> {
    url: string;
    urlParams?: TUrlParams
}

export interface IGetRequestParams<TUrlParams> extends IRequestParams<TUrlParams> { }

export interface IPostRequestParams<TUrlParams, TData> extends IRequestParams<TUrlParams> {
    data: TData,
    headers?: any
}

export interface IDeleteRequestParams {
    url: string,
    id: number,
    headers?: any
}