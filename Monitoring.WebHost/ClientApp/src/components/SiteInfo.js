import React, { Component } from 'react';

export default class SiteInfo extends Component {
    displayName = SiteInfo.name
    constructor(props) {
      super(props);
      this.state = {
        data: {
          items: []
        },
        loading: true,
        error: null
      };

      fetch('api/public/siteinfo')
        .then(response => response.json())
        .then(result => {
          if (result.success) {
            this.setData(result.data);
          } else {
            this.setError(result.message);
          }
        })
        .catch(error => this.setError('Произошла ошибка.'));
    }

    setData = (data) =>
      this.setState({loading: false, data});

    setError = (error) =>
      this.setState({loading: false, error});

    static renderData(items) {
      return (
        <table className="table">
          <thead>
            <tr>
              <th>Сайт</th>
              <th>Доступен</th>
              <th>Время обновления статуса</th>
            </tr>
          </thead>
          <tbody>
            {items.map((item, i) =>
              <tr key={i}>
                <td><a href={item.url}>{item.name}</a></td>
                <td>{item.isAvailable ? 'Да' : 'Нет'}</td>
                <td>{item.statusUpdateTime}</td>
              </tr>
            )}
          </tbody>
        </table>
      );
    }

    render() {
      const contents = this.state.loading
        ? <p><em>Loading...</em></p>
        : this.state.error
          ? <p><em>{this.state.error}</em></p>
          : SiteInfo.renderData(this.state.data.items);

      return (
        <div>
          <h1>Информация о сайтах</h1>
          {contents}
        </div>
      );
    }
}