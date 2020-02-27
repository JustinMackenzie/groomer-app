import { Client } from "../types/Client";
import { ClientActionType } from "../types/actions";

const clientReducerInitialState: Client[] = [];

const clientReducer = (state = clientReducerInitialState, action: ClientActionType): Client[] => {
  switch (action.type) {
    case 'ADD_CLIENT':
      return [...state, action.client];
    case 'GET_CLIENTS':
      return action.clients;
    default:
      return state;
  }
};

export { clientReducer };