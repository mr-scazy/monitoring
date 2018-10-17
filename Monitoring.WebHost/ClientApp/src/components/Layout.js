import React, { Component } from 'react';
import HeaderMenu from '../components/HeaderMenu';

export class Layout extends Component {
  displayName = Layout.name

  render() {
    return (
      <div>
        <HeaderMenu/>
        <div className="container">
          {this.props.children}
        </div>
      </div>
    );
  }
}
