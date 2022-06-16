import React, {useEffect, useState} from 'react';
import {Spinner} from "react-bootstrap";
import api from "../../api";
import {CertificateModel} from "../../models/CertificateModel";
import "./index.css";
import {EmployeeModel} from "../../models/EmployeeModel";

export default ({testId}: {testId: number}) => {
  const [certificate, setCertificate] = useState<CertificateModel>()
  
  useEffect(() => {
    api.results.getCertificate(testId)
      .then(_ => setCertificate(_))
  }, [])
  
  if (!certificate)
    return <Spinner animation={"border"}/>
  
  return (
    <div className="certificate">
      <div className="main-container">
        <div className="logo">
          PerTechProf
        </div>
    
        <div className="marquee">
          Сертификат
        </div>
    
        <div className="assignment">
          Выдан
        </div>
    
        <div className="person">
          {certificate.personName}
        </div>
    
        <div className="reason">
          За успешное прохождение теста "{certificate.testName}"<br/>
          {certificate.percentage >= 0.9 ? "С отличием" : null}
        </div>
      </div>
    </div>
  )
}