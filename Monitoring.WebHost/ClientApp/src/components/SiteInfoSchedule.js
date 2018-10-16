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
      .catch(error => this.setError('Произошла ошибка.'));
  }

  setData = (data) =>
    this.setState({loading: false, data});

  setError = (error) =>
    this.setState({loading: false, error});

  render() {
    if (this.state.redirect) {
      return <Redirect to="/login"/>;
    }

    return (
      <div>
        GGGG
      </div>
    );
  }
}