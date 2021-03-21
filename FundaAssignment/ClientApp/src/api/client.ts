async function request<TResponse>(
    url: string,
    config: RequestInit
): Promise<TReponse> {
    const response = await fetch(url, config);
    return await response.json();
}

export const apiClient = {
    get: <TResponse>(url: string) =>
        request<TResponse>(url, { method: 'GET' }) as Promise<TResponse>,

    post: <TBody extends BodyInit, TResponse>(url: string, body: TBody) =>
        request<TResponse>(url, { method: 'POST', body }) as Promise<TResponse>,
}