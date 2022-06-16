import React from 'react';
import {useParams} from "react-router-dom";
import Certificate from "../components/Certificate";
import {Col, Container} from "react-bootstrap";

export const CertificateView = () => {
  const {testId} = useParams()
  
  if (!testId)
    return null;
  
  return <Container className="mt-5">
      <Certificate testId={+testId}/>
  </Container>
}