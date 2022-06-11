import React, { useEffect } from 'react';
import { LinkContainer } from 'react-router-bootstrap';
import { Navbar, Container, Nav } from 'react-bootstrap';
import { observer } from 'mobx-react-lite';

import { appRoutes } from '../pages/routes';
import authStore from '../stores/AuthStore';

export const NavBar = observer(() => {
  return (
    <Navbar bg="dark" variant="dark" expand="lg">
      <Container>
        <LinkContainer to="/"><Navbar.Brand>PerTechProf</Navbar.Brand></LinkContainer>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
        { authStore.token ? <>
            <Nav className="me-auto">
              {authStore.isEmployer && <>
                <LinkContainer to={'/'}><Nav.Link></Nav.Link></LinkContainer>
              </>}
            </Nav>
            <Nav>
                <LinkContainer to={appRoutes.logout.path}><Nav.Link>Выйти</Nav.Link></LinkContainer>
            </Nav>
          </>
          : ( <>
          <Nav className='ms-auto'>
            <LinkContainer to={appRoutes.login.path}><Nav.Link>Войти</Nav.Link></LinkContainer>
          </Nav>
          </>)
        }
        </Navbar.Collapse>
      </Container>
    </Navbar>
  )
})