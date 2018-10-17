import React, { Component } from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import SiteInfo from './components/SiteInfo';
import SiteInfoSchedule from './components/SiteInfoSchedule/Page';
import Login from './components/Login';

export default class App extends Component {
  displayName = App.name

  render() {
    return (
      <Layout>
        <Route exact path="/" component={SiteInfo} />
        <Route path="/admin" component={SiteInfoSchedule} />
        <Route path="/login" component={Login} />
      </Layout>
    );
  }
}
