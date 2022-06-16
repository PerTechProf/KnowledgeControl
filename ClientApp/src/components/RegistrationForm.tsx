import { observer } from 'mobx-react-lite';
import React, { ChangeEvent, FormEvent, useState } from 'react';
import { Button, Col, Form, NavLink, Row } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';
import { Navigate, useNavigate } from 'react-router-dom';
import authController from '../api/authController';
import { appRoutes } from '../pages/routes';
import authStore from '../stores/AuthStore';

export const RegistrationForm = observer(() => {
  const navigate = useNavigate()
  
  const [isBadStatus, setBadStatus] = useState(false);

  const [userName, setUserName] = useState('');
  const [password, setPassword] = useState('');
  const [email, setEmail] = useState('');

  const onSubmit = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    if (!(userName && password && email))
      return;
    try {
      await authController.register({userName, email, password})
      
      authStore.setUserData(
        await authController.login({userName, password})
      )
      navigate('/');
    } catch {
      setBadStatus(true);
    }
  }

  if (authStore.token)
    return <Navigate to="/"/>;
  
  return (
    <Form onSubmit={onSubmit}>
      <Form.Group className="mb-3">
        <Form.Label>Логин</Form.Label>
        <Form.Control 
          onChange={(event: ChangeEvent<HTMLInputElement>) => setUserName(event.target.value)}
          value={userName}
          id="username" name="username" type="text" />
      </Form.Group>
      <Form.Group className="mb-3">
        <Form.Label>Пароль</Form.Label>
        <Form.Control 
          onChange={(event: ChangeEvent<HTMLInputElement>) => setPassword(event.target.value)} 
          value={password}
          id="password" name="password" type="password" />
      </Form.Group>
      <Form.Group className="mb-3">
        <Form.Label>Почта</Form.Label>
        <Form.Control 
          onChange={(event: ChangeEvent<HTMLInputElement>) => setEmail(event.target.value)} 
          value={email}
          id="email" name="email" type="email" />
      </Form.Group>
      <Row>
          <Button variant={isBadStatus ? "danger" : "primary"} type="submit">
            Войти
          </Button>
      </Row>
      <Col>
        <Form.Text className="text-muted">
          <LinkContainer to={appRoutes.login.path}><NavLink>Авторизация</NavLink></LinkContainer>
        </Form.Text>
      </Col>
    </Form>
  )
})