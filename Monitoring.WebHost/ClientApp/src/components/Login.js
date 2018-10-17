import React, { Component } from 'react';
import { Redirect } from 'react-router';
import { Panel } from 'react-bootstrap';
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
          this.setState({error: result.message})
        }
      })
      .catch(_ => this.setState({error: 'Произошла ошибка.'}));
  }

  setData = (data) => {
    localStorage.setItem('accessToken', data.accessToken);
    localStorage.setItem('username', data.username);
    this.setState({redirect: true});
  }

  render() {
    if (this.state.redirect) {
      return <Redirect to="/admin"/>;
    }

    let style = {width:'320px', left:'50%', top:'50%', position:'absolute', marginLeft:'-160px'};
    let style1 = {...style, marginTop:'-220px'};
    let style2 = {...style, marginTop:'-280px', color:'red', borderColor:'red'};

    return (
      <div>
        <Panel style={style1}>        
          <form action="javascript:void(0);" onSubmit={this.onSubmit}>
            <div className="form-group">
              <label htmlFor="username">Логин</label>
              <input type="text" className="form-control" id="username" placeholder="Введите логин"/>
            </div>
            <div className="form-group">
              <label htmlFor="password">Пароль</label>
              <input type="password" className="form-control" id="password" placeholder="Пароль"/>
            </div>
            <button type="submit" className="btn btn-primary">Войти</button>
          </form>          
        </Panel>
        {this.state.error && <Panel bsStyle="danger" style={style2}>{this.state.error}</Panel>}
      </div>
    );
  }
}