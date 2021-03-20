import { routerMiddleware } from 'connected-react-router';
import { History } from 'history';
import getRootReducer, { RootState } from './rootApplicationReducer';
import { Action, configureStore, getDefaultMiddleware, ThunkAction } from '@reduxjs/toolkit';

const createApplicationStore = (history: History, initialState?: RootState) => {

    return configureStore({
        reducer: getRootReducer(history),
        middleware: [...getDefaultMiddleware(), routerMiddleware(history)],
        preloadedState: initialState
    });
}

export interface AppThunkAction<TAction> {
    (dispatch: (action: TAction) => void, getState: () => RootState): void;
}

export type AppThunk = ThunkAction<void, RootState, null, Action<string>>
export default createApplicationStore
