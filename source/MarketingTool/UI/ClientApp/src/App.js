import React, { Component } from 'react';
import { Route } from 'react-router';
import Login from './Pages/Login';

export default class App extends Component {
  static displayName = App.name;

  render () {
      return (
        <Login />
        <Route exact path='/' component={Login} />
       
    );
  }
}
