import React, { Component } from 'react';
import { FormControl, Button, Glyphicon } from 'react-bootstrap';

/**
 * Редактируемая строка "Настройки мониторинга"
 */
export default class EditableRow extends Component {
  displayName = EditableRow.name
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

  onSave = () => this.props.onSave && this.props.onSave({...this.state});

  onChange = (e) => {
    let { name, value } = e.target;
    if (name === 'interval' && !(value > 0)) {
      value = 0;
    }
    this.setState({[name]: value});
  }

  render() {
    const { disabled } = this.props;
    const controlProps = { disabled, onChange: this.onChange };
    return (
      <tr>
        <td><FormControl type="text" name="name" value={this.state.name} {...controlProps}/></td>
        <td><FormControl type="text" name="url" value={this.state.url} {...controlProps}/></td>
        <td><FormControl type="number" name="interval" value={this.state.interval} {...controlProps}/></td>
        {this.state.id > 0
          ?
          <td>
            <Button onClick={this.onSave} bsStyle="success" disabled={disabled}><Glyphicon glyph="ok" /></Button>&nbsp;           
          </td>
          :
          <td>
            <Button onClick={this.onSave.bind(this)} bsStyle="primary" disabled={disabled}><Glyphicon glyph="plus" /></Button>&nbsp;
          </td>}
      </tr>
    );
  };
}