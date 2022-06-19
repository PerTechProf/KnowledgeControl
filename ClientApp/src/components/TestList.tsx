import React, {useEffect, useState} from "react";

import {Button, CloseButton, Col, Container, OverlayTrigger, Row, Spinner, Tooltip} from "react-bootstrap";

import {useNavigate} from "react-router-dom";
import {observer} from "mobx-react-lite";

import api from "../api";
import authStore from "../stores/AuthStore";
import {TestViewModel} from "../models/TestViewModel";

export const TestList = observer(() => {
  const navigate = useNavigate()

  const [tests, setTests] = useState<TestViewModel[]>()

  const onTest = (id: number) => {
    navigate(`/tests/${id}`)
  }
  
  const addTest = () => {
    navigate('/tests/new')
  }
  
  const onDelete = async (id: number) => {
    console.log(id)
    await api.tests.deleteTest(id)
    
    setTests(tests?.filter(_ => _.id != id))
  }

  useEffect(() => {
    api.tests.getTests()
      .then(data => setTests(data))
  }, [])

  return <Container className={authStore.isEmployer ? "w-75" : "w-25"}>
    {tests?.map(test => (<Row className="mt-3 border p-3" key={test.id}>
      <Col md={8}><b>{test.name}</b></Col>
      {authStore.isEmployer ?
        <Col md={4}>
          <Row>
            <Col><Button className="me-1" onClick={() => navigate(`/solutions/${test.id}`)}>Решения</Button></Col>
            <Col><Button className="me-1" onClick={() => onTest(test.id)}>Изменить</Button></Col>
            <Col>
              <OverlayTrigger
                overlay={
                  <Tooltip>
                    Удалить
                  </Tooltip>
                }
                placement={"right"}
              >
                <CloseButton onClick={() => onDelete(test.id)}/>
              </OverlayTrigger>
            </Col>
          </Row>
        </Col> :
        <Col md={4}>
          <Button onClick={() => onTest(test.id)}>Пройти</Button>
        </Col>
      }
    </Row>)) ?? <Spinner animation="border"/>}
    <Row>{authStore.isEmployer && <Button className="mt-5" onClick={addTest}>Добавить тест</Button>}</Row>
  </Container>
})