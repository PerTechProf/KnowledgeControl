import axios from "axios";

import {CertificateModel} from "../models/CertificateModel";

const getCertificate = (testId: number): Promise<CertificateModel> =>
  axios.get(`/api/Results/${testId}`)
    .then(response => response.data)

export default {
  getCertificate
}