import React, {useEffect, useState} from 'react';
import {SolutionModel} from "../models/SolutionModel";
import {Spinner, Table} from "react-bootstrap";
import api from "../api";
import {TestModel} from "../models/TestModel";
import {EmployeeModel} from "../models/EmployeeModel";

export const SolutionsTable = ({id}: {id: number}) => {
  const [solutions, setSolutions] = useState<SolutionModel[]>([])
  const [test, setTest] = useState<TestModel>();
  const [employees, setEmployees] = useState<EmployeeModel[]>([])
  
  useEffect(() => {
    api.solutions.getSolutions(id)
      .then(_ => {
        setSolutions(_)
        api.auth.getEmployees()
          .then(_ =>
            setEmployees(_))
      })
    api.tests.getTest(id)
      .then(_ => setTest(_))
  }, [])
  
  if (!test)
    return <Spinner animation={"border"}/>

  const correctAnswers = (JSON.parse(test.answers) as string[])
  const getAnswers = (solution: SolutionModel) =>
    (JSON.parse(solution.answers) as string[])
  
  return <Table>
    <thead>
      <tr>
        <th>#</th>
        <th>Имя</th>
        {correctAnswers.map(ans =>
          <th key={ans}>{ans}</th>)}
        <th>Правильных</th>
      </tr>
    </thead>
    <tbody>
    {solutions.map((sol, index) => <tr key={sol.id}>
      <td>{index+1}</td>
      <td>{employees.find(_ => _.id == sol.userId)?.name ?? ''}</td>
      {getAnswers(sol).map(ans =>
        <td key={ans}>{ans}</td>)}
      <td>{getAnswers(sol).reduce((sum, el, i) => 
        el.toLowerCase() == correctAnswers[i].toLowerCase() ? sum+1 : sum, 0)}
      </td>
    </tr>)}
    </tbody>
  </Table>
}