import React from 'react';

import { observer } from 'mobx-react-lite';
import {
  Routes,
  Route
} from "react-router-dom";

import authStore from '../stores/AuthStore';
import { appRoutes } from './routes';

const Paths = observer(() => {
  return (
    <Routes>
      {appRoutes.public.map((route) => 
        <Route key={route.path} path={route.path} element={<route.component />} />)}
      
      {authStore.token && appRoutes.authOnly.public.map((route) => 
        <Route key={route.path} path={route.path} element={<route.component />} />)}
      
      {!authStore.isEmployer && appRoutes.authOnly.userOnly.map((route) => 
        <Route key={route.path} path={route.path} element={<route.component />} />)}
      
      {authStore.isEmployer && appRoutes.authOnly.employerOnly.map((route) => 
        <Route key={route.path} path={route.path} element={<route.component />} />)}
      
      {!authStore.token && 
        <Route path={appRoutes.login.path} element={<appRoutes.login.component/>} />}
      {!authStore.token && 
        <Route path={appRoutes.register.path} element={<appRoutes.register.component/>} />}
      
      {authStore.token && 
        <Route path={appRoutes.logout.path} element={<appRoutes.logout.component/>} />}
    </Routes>
  )
})

export default Paths;