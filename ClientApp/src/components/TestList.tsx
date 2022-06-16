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

  return <Container className={authStore.isEmployer ? "w-50" : "w-25"}>
    <Row>
      {tests?.map(test => (<Col key={test.id} md={12}>
        <b className="display-6">{test.name}</b>
        {authStore.isEmployer ?
          <div className="float-end">
            <Button className="me-3" onClick={() => onTest(test.id)}>Изменить</Button>
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
          </div> :
          <>
            <Button className="float-end" onClick={() => onTest(test.id)}>Пройти</Button>
          </>
        }
      </Col>)) ?? <Spinner animation="border"/>}
      {authStore.isEmployer && <Button className="mt-5" onClick={addTest}>Добавить тест</Button>}
    </Row>
  </Container>
})