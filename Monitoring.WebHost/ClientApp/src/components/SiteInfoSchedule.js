import React, { Component } from 'react';
import { Redirect } from 'react-router';
import { fetchGet, fetchPost, fetchPut, fetchDelete } from '../utils';

export default class SiteInfoSchedule extends Component {
  displayName = SiteInfoSchedule.name
  constructor(props) {
    super(props);
    this.state = {
      data: {
        items: []
      },
      loading: true,
      error: null,
      redirect: false
    };

    fetchGet('api/admin/SiteInfoSchedule')
      .then(response => {
        if (response.status == 401) {
          this.setState({redirect: true});
          return response;
        }
        else {
          return response.json();
        }
      })
      .then(result => {
        if (result.success) {
          this.setData(result.data);
        } else {
          this.setError(result.message);
        }
      })
      .catch(_ => this.setError('Произошла ошибка.'));
  }

  setData = (data) =>
    this.setState({loading: false, data});

  setError = (error) =>
    this.setState({loading: false, error});

  renderRow = (item, key) => {
    item = { ...item };
    return item.editable 
    ? <tr key={key}> 
        <td><input type="text"/></td>
        <td><input type="text"/></td>
        <td><input type="number"/></td>  
      </tr>
    : <tr key={key}> 
        <td>{item.url}</td>
        <td>{item.name}</td>
        <td>{item.interval}</td>     
      </tr>
  }

  renderData = (items) => {
    return (
      <table className="table">
        <thead>
          <tr>
            <th>Наименование</th>
            <th>URL</th>
            <th>Интервал</th>
          </tr>
        </thead>
        <tbody>
          {items.map((item, i) => this.renderRow(item, i))}
          {this.renderRow({editable: true}, "new-row")}
        </tbody>
      </table>
    );
  }

  render() {
    if (this.state.redirect) {
      return <Redirect to="/login"/>;
    }

    const contents = this.state.loading
        ? <p><em>Loading...</em></p>
        : this.state.error
          ? <p><em>{this.state.error}</em></p>
          : this.renderData(this.state.data.items);

    return (
      <div>
        <h1>Настройки мониторинга</h1>
        {contents}
      </div>
    );
  }
}