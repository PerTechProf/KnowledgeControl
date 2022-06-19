import React, {FormEvent, useState} from 'react';

import {EmployeeModel} from "../models/EmployeeModel";
import {Button, Col, Form, Row} from "react-bootstrap";
import {useNavigate} from "react-router-dom";
import api from "../api";

export const EditEmployeeForm = (employee: EmployeeModel | undefined) => {
  const navigate = useNavigate()
  
  const [userName, setUserName] = useState(employee?.userName ?? '')
  const [password, setPassword] = useState('')
  const [email, setEmail] = useState(employee?.email ?? '')
  const [name, setName] = useState(employee?.name ?? '')
  
  const [isAborted, setAborted] = useState(false)
  
  const onEdit = async (e: FormEvent) => {
    e.preventDefault()
    
    if (!employee)
      return;
    
    try {
      setAborted(false)
      
      await api.auth.editEmployee({
        id: employee.id,
        userName,
        password,
        email,
        name
      })
      
      navigate('/employees')
    } catch {
      setAborted(true)
    }
  }
  
  return (
    <Form onSubmit={onEdit}>
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
          <Button variant={isAborted ? 'danger' : 'primary'} type="submit" className="w-100">Сохранить</Button>
        </Col>
      </Row>
    </Form>
  )
}