import { createAsyncThunk, createEntityAdapter, createSlice } from "@reduxjs/toolkit";
import { RootState } from "../../store/rootApplicationReducer";
import { topAgentClient } from "./topAgentClient";
import { Agent, topAgentsRequestParameters } from "./topAgentTypes";

export type stateStatus = "idle" | "loading" | "error"

const topAgentsAdapater = createEntityAdapter<Agent>(
    {
        selectId: (agent) => agent.agentId
    }
)


const initialState = topAgentsAdapater.getInitialState(
    {
        status: "idle" as stateStatus
    }
)

export const fetchTopAgents = createAsyncThunk('topAgents/fetchTopAgents', async (params: topAgentsRequestParameters) => {
    return await topAgentClient.getTopAgents(params)
})

const topAgentsSlice = createSlice({
    name: 'topAgents',
    initialState: initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder
            .addCase(fetchTopAgents.pending, (state, action) => {
                state.status = 'loading'
            })
            .addCase(fetchTopAgents.fulfilled, (state, action) => {
                topAgentsAdapater.removeAll(state)
                topAgentsAdapater.addMany(state, action.payload)
                state.status = 'idle'
            })
            .addCase(fetchTopAgents.rejected, (state, action) => {
                topAgentsAdapater.removeAll(state)
                state.status = 'error'
            })
    }
})

export const topAgentsReducer = topAgentsSlice.reducer

export const {
    selectAll: selectTopAgents,
} = topAgentsAdapater.getSelectors<RootState>((state) => state.topAgents)

export const selectTopAgentsStatus = (state: RootState) => state.topAgents.status