import { action, makeAutoObservable } from "mobx";
import { getCookie } from "../api";
import { AuthModel } from "../models/AuthModel";
import axios from "axios";

export class AuthStore {
  token = getCookie("authToken") ?? '';
  
  isEmployer: boolean = JSON.parse(getCookie("isEmployer") ?? false);

  @action
  setUserData = ({token, isEmployer}: AuthModel): void => {
    this.token = token;
    this.isEmployer = isEmployer;


    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
  }

  constructor() {
    makeAutoObservable(this);
    axios.defaults.headers.common['Authorization'] = `Bearer ${this.token}`;
  }
}

const authStore = new AuthStore();

export default authStore;