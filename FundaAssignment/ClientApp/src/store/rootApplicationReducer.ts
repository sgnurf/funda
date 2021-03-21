import { History } from 'history';
import { combineReducers } from '@reduxjs/toolkit';
import { connectRouter } from 'connected-react-router';
import { topAgentsReducer } from '../features/topAgents/topAgentsSlice';


const getRootReducer = (history: History) => {
    return combineReducers({
        router: connectRouter(history),
        topAgents: topAgentsReducer
    })
}

export type RootState = ReturnType<ReturnType<typeof getRootReducer>>

export default getRootReducer