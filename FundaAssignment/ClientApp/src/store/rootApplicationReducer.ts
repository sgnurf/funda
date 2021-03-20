import { History } from 'history';
import { combineReducers } from '@reduxjs/toolkit';
import { connectRouter } from 'connected-react-router';


const getRootReducer = (history: History) => {
    return combineReducers({
        router: connectRouter(history)
    })
}

export type RootState = ReturnType<ReturnType<typeof getRootReducer>>

export default getRootReducer