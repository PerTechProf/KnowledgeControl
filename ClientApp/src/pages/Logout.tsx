import { observer } from "mobx-react-lite";
import { useEffect } from "react";
import { Navigate, useNavigate } from "react-router-dom";
import authController from "../api/authController";
import authStore from "../stores/AuthStore";
import { appRoutes } from "./routes";
import questionsStore from "../stores/QuestionsStore";

export const Logout = observer(() => {
  const navigate = useNavigate();

  useEffect(() => {
    authController.logout()
      .then(
        () => authStore.setUserData({token: '', isEmployer: false}))
      .then(() => {
        questionsStore.setQuestions([])
        navigate('/login', {replace: true})
      });
  })

  return <div></div>
})