import React from 'react';
import {observer} from "mobx-react-lite";
import {Container} from "react-bootstrap";
import {SolutionsTable} from "../components/SolutionsTable";
import {useParams} from "react-router-dom";

export const TestSolutions = () => {
  const {testId} = useParams()
  
  if (isNaN(testId as any) || !testId)
    return null
  
  return <Container><SolutionsTable id={+testId}/></Container>
}