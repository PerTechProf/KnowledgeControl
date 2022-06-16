import React from 'react';
import {CertificatesList} from "../components/CertificatesList";
import {Container} from "react-bootstrap";

export const Certificates = () => {
  return <Container className="mt-5"> 
    <CertificatesList/>
  </Container>
}