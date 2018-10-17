import React, { Component } from 'react';
import { Glyphicon, Nav, Navbar, NavItem, NavDropdown } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';

export default class HeaderMenu extends Component {

  constructor(props) {
    super(props);
    this.state = {
      ...this.state,
      redirect: false
    };
  }

    logout = () => {
      localStorage.clear();
    }

    render() {

      return (
        <Navbar inverse collapseOnSelect>
          <Navbar.Header>
            <Navbar.Brand>
              <a href="/">Мониторинг сайтов</a>
            </Navbar.Brand>
          </Navbar.Header>
          <Navbar.Collapse>
            <Nav>
              <LinkContainer to={'/'} exact>
                <NavItem>
                  <Glyphicon glyph="home" /> На главную
                </NavItem>
              </LinkContainer>
              <LinkContainer to={'/admin'} exact>
                <NavItem>
                  <Glyphicon glyph="th-list" /> Панель администрирования
                </NavItem>
              </LinkContainer>
            </Nav>
            {localStorage.accessToken &&
                        <Nav pullRight>
                          <NavDropdown title={localStorage.username} id="basic-nav-dropdown">
                            <LinkContainer to={'/login'}>
                              <NavItem onClick={this.logout} href="#"> Выйти </NavItem>
                            </LinkContainer>
                          </NavDropdown>
                        </Nav>
            }
          </Navbar.Collapse>
        </Navbar>
      );
    }
}