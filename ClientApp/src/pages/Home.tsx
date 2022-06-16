import {Container} from "react-bootstrap"
import {observer} from "mobx-react-lite";
import authStore from "../stores/AuthStore";

export const Home = observer(() => {
  return <Container>
    <h1>
      {!authStore.token ? "Приложение для контроля знаний сотрудников" :
      `Вы авторизированы как ${authStore.isEmployer ? "руководитель" : "сотрудник"}`}
    </h1>
  </Container>
})