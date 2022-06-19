import React, {useEffect, useState} from 'react';
import {useParams} from "react-router-dom";
import {EmployeeModel} from "../models/EmployeeModel";
import api from "../api";
import {Col, Container, Row, Spinner} from "react-bootstrap";
import {EditEmployeeForm} from "../components/EditEmployeeForm";

export const EditEmployee = () => {
  const {userId} = useParams()
  const [employee, setEmployee] = useState<EmployeeModel>()
  
  useEffect(() => {
    if (!userId || isNaN(userId as any))
      return
    
    api.auth.getEmployee(+userId)
      .then((user) => setEmployee(user))
  }, [])

  if (!userId || isNaN(userId as any))
    return null
  
  if (!employee)
    return <Spinner animation={"border"}/>
  
  return <Container className="mt-5 w-50">
    <Row>
      <Col>
        <EditEmployeeForm {...employee}/>
      </Col>
    </Row>
  </Container>
  
}