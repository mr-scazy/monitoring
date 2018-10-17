import React, { Component } from 'react';
import Grid from './Grid';

export default class Page extends Component {
  displayName = Page.name
  constructor(props) {
    super(props);
    this.state = {...this.state};
  }

  render() {
    return (
      <div>
        <h1>Настройки мониторинга</h1>
        <Grid/>
      </div>
    );
  }
}