import { combineReducers, createStore, applyMiddleware } from "redux";
import thunk, { ThunkMiddleware } from 'redux-thunk';
import { clientReducer } from "../reducers/clientReducer";
import { AppAction } from "../types/actions";

export const rootReducer = combineReducers({
    clients: clientReducer
});

export type AppState = ReturnType<typeof rootReducer>;

export const store = createStore(rootReducer, applyMiddleware(thunk as ThunkMiddleware<AppState, AppAction>));