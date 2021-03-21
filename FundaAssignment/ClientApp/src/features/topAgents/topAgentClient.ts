import { apiClient } from "../../api/client";
import { Agent, topAgentsRequestParameters } from "./topAgentTypes";

const getTopAgents = async (params: topAgentsRequestParameters) => {
    return await apiClient.get<Agent[]>('/api/TopAgents/' + params.count + '/' + params.filter.join('/'))
}

export const topAgentClient = {
    getTopAgents
}