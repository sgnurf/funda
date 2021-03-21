export interface Agent {
    agentId: number,
    agentName: string,
    offerCount: number
}

export interface topAgentsRequestParameters {
    count: number,
    filter: string[]
}
