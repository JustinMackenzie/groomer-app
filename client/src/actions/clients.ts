import { AppAction } from "../types/actions";
import { Client } from "../types/Client";
import { Dispatch } from "redux";
import { AppState } from "../store/store";
import axios from 'axios';

export const addClient = (client: Client): AppAction => ({
  type: 'ADD_CLIENT',
  client
});

export const getClients = (clients: Client[]): AppAction => ({
  type: 'GET_CLIENTS',
  clients
})

export const startAddClient = (client: Client) => {
  return (dispatch: Dispatch<AppAction>, getState: () => AppState) => {
    dispatch(addClient(client));
  };
};

export const startGetClients = () => {
  return (dispatch: Dispatch<AppAction>, getState: () => AppState) => {
    axios.get('https://localhost:44321/api/client')
      .then((response) => {
        dispatch(getClients(response.data));
      })
  }; 
}