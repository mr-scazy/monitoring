import React, { Component } from 'react';
import { Button, Glyphicon } from 'react-bootstrap';

export default class Row extends Component {
  displayName = Row.name
  constructor(props) {
    super(props);
    this.state = {
      id: 0,
      name: '',
      url: '',
      interval: 0,
      ...this.props.item
    };
  }

  onEdit = () => this.props.onEdit && this.props.onEdit({...this.state});

  onRemove = () => this.props.onRemove && this.props.onRemove({...this.state});

  render() {
    const { disabled } = this.props;
    return (
      <tr>
        <td>{this.state.name}</td>
        <td>{this.state.url}</td>
        <td>{this.state.interval}</td>
        <td>
          <Button onClick={this.onEdit} disabled={disabled}><Glyphicon glyph="edit" /></Button>&nbsp;
          <Button onClick={this.onRemove} bsStyle="danger" disabled={disabled}><Glyphicon glyph="remove" /></Button>&nbsp;
        </td>
      </tr>
    );
  }
}