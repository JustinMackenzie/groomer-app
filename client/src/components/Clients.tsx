import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { AppState } from '../store/store';
import { ThunkDispatch } from 'redux-thunk';
import { AppAction } from '../types/actions';
import { startAddClient, startGetClients } from '../actions/clients';
import { connect } from 'react-redux';
import { Client } from '../types/Client';

interface ClientsProps {

}

interface ClientsState {

}

type Props = ClientsProps & LinkStateProps & LinkDispatchProps;

class Clients extends Component<Props, ClientsState> {
  onAddClient = (client: Client) => {
    this.props.startAddClient(client);
  };

  componentDidMount() {
    this.props.startGetClients();
  }

  render() {
    const clients = this.props.clients.map(client => (
      <tr key={client.id}>
        <td>{client.firstName}</td>
        <td>{client.lastName}</td>
        <td>{client.email}</td>
        <td>{client.phone}</td>
      </tr>
    ));
    return (
      <div>
        <h1>Clients</h1>
        <table className="table">
          <thead>
            <tr>
              <th scope="col">First Name</th>
              <th scope="col">Last Name</th>
              <th scope="col">Email</th>
              <th scope="col">Phone</th>
            </tr>
          </thead>
          <tbody>
            {clients}
          </tbody>
        </table>
      </div>
    );
  }
}

interface LinkStateProps {
  clients: Client[];
}

interface LinkDispatchProps {
  startAddClient: (client: Client) => void;
  startGetClients: () => void;
}

const mapStateToProps = (state: AppState, props: ClientsProps): LinkStateProps => ({
  clients: state.clients
});

const mapDispatchToProps = (dispatch: ThunkDispatch<any, any, AppAction>, props: ClientsProps): LinkDispatchProps => ({
  startAddClient: bindActionCreators(startAddClient, dispatch),
  startGetClients: bindActionCreators(startGetClients, dispatch)
});

export default connect(mapStateToProps, mapDispatchToProps)(Clients);