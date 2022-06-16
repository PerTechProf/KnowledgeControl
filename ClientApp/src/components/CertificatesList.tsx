import React, {useEffect, useState} from "react";

import {Button, CloseButton, Col, Container, OverlayTrigger, Row, Spinner, Tooltip} from "react-bootstrap";

import {useNavigate} from "react-router-dom";
import {observer} from "mobx-react-lite";

import api from "../api";
import authStore from "../stores/AuthStore";
import {TestViewModel} from "../models/TestViewModel";

export const CertificatesList = observer(() => {
  const navigate = useNavigate()

  const [tests, setTests] = useState<TestViewModel[]>()

  const onOpen = (id: number) => {
    navigate(`/certificates/${id}`)
  }

  useEffect(() => {
    api.tests.getSolvedTests()
      .then(data => setTests(data))
  }, [])

  return <Container className="w-50">
    <Row>
      {tests?.map(test => (<Col key={test.id} md={12}>
        <b className="display-6">{test.name}</b>
        <Button className="float-end" onClick={() => onOpen(test.id)}>Сертификат</Button>
      </Col>)) ?? <Spinner animation="border"/>}
    </Row>
  </Container>
})