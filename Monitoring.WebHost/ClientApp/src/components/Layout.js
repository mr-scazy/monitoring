import React, { Component } from 'react';
import { Navbar, Button } from 'react-bootstrap';

export class Layout extends Component {
  displayName = Layout.name

  render() {
    return (
      <div>
        <Navbar inverse collapseOnSelect>
          <Navbar.Header><Navbar.Brand>
            <a href="/">Мониторинг сайтов</a>
          </Navbar.Brand></Navbar.Header>
        </Navbar>
        <div className="container">
          {this.props.children}
        </div>
      </div>
    );
  }
}
