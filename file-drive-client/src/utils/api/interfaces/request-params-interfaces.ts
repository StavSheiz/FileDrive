
export interface IRequestParams<TUrlParams> {
    url: string;
    urlParams: TUrlParams
}

export interface IGetRequestParams<TUrlParams> extends IRequestParams<TUrlParams> { }

export interface IPostRequestParams<TUrlParams, TData> extends IRequestParams<TUrlParams> {
    data: TData,
}

