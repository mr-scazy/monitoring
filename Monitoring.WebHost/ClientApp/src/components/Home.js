import React, { Component } from 'react';
import { Col, Grid, Row } from 'react-bootstrap';
import SiteInfo from './SiteInfo';

export class Home extends Component {
  displayName = Home.name

  render() {
    return (
      <Grid fluid>
        <Row>
          <Col sm={5}>
            <SiteInfo/>
          </Col>
        </Row>
      </Grid>
    );
  }
}
