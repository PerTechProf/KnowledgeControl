import React from 'react'
import { Container } from 'react-bootstrap'
import { LoginForm } from '../components/LoginForm'

export const Login = () => {
  return <Container className='mw-50 m-0 m-auto mt-5'>
    <LoginForm/>
  </Container>
}