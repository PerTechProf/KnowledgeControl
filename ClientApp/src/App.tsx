import React from 'react';
import { BrowserRouter } from 'react-router-dom';

import './App.css';

import { NavBar } from './components/NavBar';
import RouteApplet from './pages/RouteApplet';


function App() {
  return (
    <BrowserRouter>
      <NavBar/>
      <RouteApplet/>
    </BrowserRouter>
  );
}

export default App;
