import React, { Component } from 'react';
import './App.css';
import Clients from './components/Clients';
import { Provider } from 'react-redux';
import { store } from './store/store';

export class App extends Component<{}, {}> {
  render() {
    return <Provider store={store}>
      <div className="App">
        <Clients></Clients>
      </div>
    </Provider>
  }
}
