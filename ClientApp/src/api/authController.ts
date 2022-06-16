import axios from "axios";
import { AuthModel } from "../models/AuthModel";
import { LoginModel } from "../models/LoginModel";
import { RegistrationModel } from "../models/RegistrationModel";
import authStore from "../stores/AuthStore";
import {EmployeeModel} from "../models/EmployeeModel";

const login = (model: LoginModel): Promise<AuthModel> =>
  axios.post("/api/auth/login", model)
    .then(response => response.data);

const register = (model: RegistrationModel): Promise<AuthModel> =>
  axios.post("/api/auth/register", model)
    .then(response => response.data)

const createEmployee = (model: RegistrationModel) =>
  axios.post("/api/Auth/CreateEmployee", model)

const getEmployees = (): Promise<EmployeeModel[]> =>
  axios.get("/api/Auth/GetEmployees")
    .then(response => response.data)

const deleteEmployee = (id: number) =>
  axios.delete(`/api/Auth/DeleteEmployee/${id}`)

const logout = () =>
  axios.post("/api/auth/logout");

export default {
    login,
    register,
    getEmployees,
    createEmployee,
    deleteEmployee,
    logout
};