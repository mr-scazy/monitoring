import React, { Component } from 'react';
import { Redirect } from 'react-router';
import { fetchGet } from '../utils';

export default class Login extends Component {
  displayName = Login.name

  constructor(props) {
    super(props);
    this.state = {
      loading: true,
      error: null,
      redirect: false
    };

    fetchGet('api/admin/Account/check')
      .then(response => {
        if (response.ok) {
          this.setState({redirect: true});
        }
      });
  }

  onSubmit = (e) => {
    e.preventDefault();

    const username = e.target[0].value;
    const password = e.target[1].value;

    const url = `api/admin/Account/token?username=${username}&password=${password}`;

    fetch(url)
      .then(response => response.json())
      .then(result => {
        if (result.success) {
          this.setData(result.data);
        } else {
          this.setError(result.message);
        }
      })
      .catch(_ => this.setError('Произошла ошибка.'));
  }

  setData = (data) => {
    localStorage.setItem('accessToken', data.accessToken);
    localStorage.setItem('username', data.username);
    this.setState({redirect: true});
  }

  setError = (error) =>
    this.setState({loading: false, error});

  render() {
    if (this.state.redirect) {
      return <Redirect to="/admin"/>;
    }

    return (
      <div>
        <form action="javascript:void(0);" onSubmit={this.onSubmit}>
          <div className="form-group">
            <label htmlFor="username">Логин</label>
            <input type="text" className="form-control" id="username" placeholder="Введите логин"/>
          </div>
          <div className="form-group">
            <label htmlFor="password">Password</label>
            <input type="password" className="form-control" id="password" placeholder="Пароль"/>
          </div>
          <button type="submit" className="btn btn-primary">Войти</button>
        </form>
      </div>
    );
  }
}