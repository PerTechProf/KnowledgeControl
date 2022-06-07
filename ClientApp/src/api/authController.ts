import axios from "axios";
import { AuthModel } from "../models/AuthModel";
import { LoginModel } from "../models/LoginModel";
import { RegistrationModel } from "../models/RegistrationModel";
import authStore from "../stores/AuthStore";

const authController = {
  login: (model: LoginModel): Promise<AuthModel> =>
    axios.post("api/auth/login", model)
      .then(response => response.data),
  register: (model: RegistrationModel): Promise<AuthModel> =>
    axios.post("api/auth/register", model)
      .then(response => response.data)
      .then(data => JSON.parse(data)),
  logout: () =>
    axios.post("api/auth/logout")
}

export default authController;