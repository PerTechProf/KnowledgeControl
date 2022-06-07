import { action, makeAutoObservable } from "mobx";
import { getCookie } from "../api";
import { AuthModel } from "../models/AuthModel";

export class AuthStore {
  token = getCookie("authToken") ?? '';
  
  isEmployer: boolean = JSON.parse(getCookie("isEmployer") ?? false);

  @action
  setUserData = ({token, isEmployer}: AuthModel): void => {
    this.token = token;
    this.isEmployer = isEmployer;
  }

  constructor() {
    makeAutoObservable(this);
  }
}

const authStore = new AuthStore();

export default authStore;