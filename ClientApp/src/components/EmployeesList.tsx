import React, {FormEvent, useEffect, useState} from 'react';
import {observer} from "mobx-react-lite";
import {Button, Card, Col, Container, Form, Row, Spinner} from "react-bootstrap";
import authStore from "../stores/AuthStore";
import api from "../api";
import {EmployeeModel} from "../models/EmployeeModel";

export const EmployeesList = observer(() => {
  const [userName, setUserName] = useState('')
  const [password, setPassword] = useState('')
  const [email, setEmail] = useState('')
  const [name, setName] = useState('')

  const [employees, setEmployees] = useState<EmployeeModel[]>([])
  
  const [isLoading, setLoading] = useState(true)
  
  const onCreate = (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault()
    
    if (!(password && userName && email && name))
      return;
    
    setLoading(true)
    
    api.auth.createEmployee({
      userName,
      email,
      name,
      password
    }).then(() => api.auth.getEmployees())
      .then(data => setEmployees(data))
      .finally(() => setLoading(false))
  }
  
  const onDelete = (id: number) => {
    setLoading(true)
    
    api.auth.deleteEmployee(id)
      .then(() => setEmployees(employees.filter(_ => _.id != id)))
      .finally(() => setLoading(false))
  }

  useEffect(() => {
    setLoading(false)
    api.auth.getEmployees()
      .then(data => setEmployees(data))
      .finally(() => setLoading(false))
  }, [])
  
  if (isLoading)
    return <Spinner animation="border"/>
  
  return (
    <Container>
      <Form onSubmit={onCreate}>
        <Row>
          <Col>
            <Form.Group>
              <Form.Label>Имя пользователя</Form.Label>
              <Form.Control
                onChange={(e) => setUserName(e.target.value)}
                type="text"
                value={userName}
                placeholder="Логин"
              />
            </Form.Group>
          </Col>
          <Col>
            <Form.Group>
              <Form.Label>Почта</Form.Label>
              <Form.Control
                onChange={(e) => setEmail(e.target.value)}
                type="email"
                value={email}
                placeholder="Email"
              />
            </Form.Group>
          </Col>
        </Row>
        <Row className="mt-3">
          <Col>
            <Form.Group>
              <Form.Label>Имя</Form.Label>
              <Form.Control
                onChange={(e) => setName(e.target.value)}
                type="text"
                value={name}
                placeholder="Имя"
              />
            </Form.Group>
          </Col>
          <Col>
            <Form.Group>
              <Form.Label>Пароль</Form.Label>
              <Form.Control
                onChange={(e) => setPassword(e.target.value)}
                type="password"
                value={password}
                placeholder="Пароль"
              />
            </Form.Group>
          </Col>
        </Row>
        <Row className="mt-3">
          <Col md={8}/>
          <Col md={4}>
            <Button type="submit" className="w-100">Добавить</Button>
          </Col>
        </Row>
      </Form>
      <Row className="mt-4">
        {employees.map(employee => (
          <Col key={employee.id} md={4}>
            <Card>
              <Card.Header as="h5">{employee.userName}</Card.Header>
              <Card.Body>
                <Card.Title>{employee.name}</Card.Title>
                <Card.Text>
                  {employee.email}
                </Card.Text>
                {/*<Button variant="success">Изменить</Button>*/}
                <Button onClick={() => onDelete(employee.id)} className="ms-2" variant="danger">Удалить</Button>
              </Card.Body>
            </Card>
          </Col>
        ))}
      </Row>
    </Container>
  )
})