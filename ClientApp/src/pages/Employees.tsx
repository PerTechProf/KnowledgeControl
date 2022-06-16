import React from 'react';
import {observer} from "mobx-react-lite";
import {Container} from "react-bootstrap";
import {EmployeesList} from "../components/EmployeesList";

export const Employees = observer(() => {
  return <div className="mt-5">
    <EmployeesList/>
  </div>
})