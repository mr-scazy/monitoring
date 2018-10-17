import React, { Component } from 'react';
import { Redirect } from 'react-router';
import { Table, Button, Glyphicon, Panel } from 'react-bootstrap';
import { fetchGet, fetchPost, fetchPut, fetchDelete } from '../../utils';
import EditableRow from './EditableRow';
import Row from './Row';

export default class Grid extends Component {
  displayName = Grid.name
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

    this.query();
  }

  checkAccess = response => {
    if (response.status == 401) {
      this.setState({redirect: true});
      return response;
    }
    return response.json();
  }

  catchHandler = e => this.setError(`Произошла ошибка. ${e.message}`);

  query = () => {
    fetchGet('api/admin/SiteInfoSchedule')
      .then(this.checkAccess)
      .then(result => {
        if (result.success) {
          this.setData(result.data);
        } else {
          this.setError(result.message);
        }
      })
      .catch(this.catchHandler);
  }

  save = (params) => {
    fetchPost('api/admin/SiteInfoSchedule', params)
      .then(this.checkAccess)
      .then(result => {
        if(result.success === false) {
          this.setError(result.message);
        } else {
          this.query();
        }
      })
      .catch(this.catchHandler);
  }

  update = (params) => {
    fetchPut('api/admin/SiteInfoSchedule', params)
      .then(this.checkAccess)
      .then(result => {
        if(result.success === false) {
          this.setError(result.message);
        } else {
          this.query();
        }
      })
      .catch(this.catchHandler);
  }

  delete = (params) => {
    fetchDelete(`api/admin/SiteInfoSchedule/${params.id}`)
      .then(this.checkAccess)
      .then(_ => {
        this.query();
      })
      .catch(this.catchHandler);
  }

  setData = (data) =>
    this.setState({loading: false, data, error: null});

  setError = (error) =>
    this.setState({loading: false, error});

  onSave = (row) => { 
    row.id > 0 
      ? this.update(row) 
      : this.save(row);
  } 

  onEdit = (row) => {
    const { data } = this.state;
    data.items.map(x => x.editable = false);
    var item = data.items.find(x => x.id == row.id);
    item.editable = true;
    this.setState({data});
  }

  onRemove = (row) => {
    this.delete(row);
  }

  render() {
    if (this.state.redirect) {
      return <Redirect to="/login"/>;
    }
    return (
      <div>
        {this.state.error && <Panel bsStyle="danger">{this.state.error}</Panel>}
        <Table>
          <thead>
            <tr>
              <th>Наименование</th>
              <th>URL</th>
              <th>Интервал</th>
              <th><Button onClick={this.query}><Glyphicon glyph="refresh" /></Button></th>
            </tr>
          </thead>
          <tbody>
            {this.state.data.items.map((item, i) => item.editable 
              ? <EditableRow key={i} item={item} onSave={this.onSave}/> 
              : <Row key={i} item={item} onEdit={this.onEdit} onRemove={this.onRemove}/>)}
            <EditableRow key="add-row" onSave={this.onSave}/>
          </tbody>
        </Table>
      </div>
    );
  }
}