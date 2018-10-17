import React from 'react';
import { Button, Glyphicon } from 'react-bootstrap';

const Row = ({key, item, onEdit, onRemove}) => {
  item = { ...item };
  return (
    <tr>
      <td>{item.name}</td>
      <td>{item.url}</td>
      <td>{item.interval}</td>
      <td>
        <Button onClick={onEdit}><Glyphicon glyph="edit" /></Button>
        <Button onClick={onRemove}><Glyphicon glyph="remove" /></Button>
      </td>
    </tr>
  );
};

export default Row;