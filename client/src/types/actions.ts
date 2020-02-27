import { Client } from "./Client";

export const ADD_CLIENT = 'ADD_CLIENT';
export const GET_CLIENTS = 'GET_CLIENTS';
export const UPDATE_CLIENT = 'UPDATE_CLIENT';
export const DELETE_CLIENT = 'DELETE_CLIENT';

export interface AddClientAction {
  type: typeof ADD_CLIENT;
  client: Client;
}

export interface GetClientsAction {
  type: typeof GET_CLIENTS,
  clients: Client[];
}

export type ClientActionType = AddClientAction | GetClientsAction;

export type AppAction = ClientActionType;