import {Container} from "react-bootstrap";
import {useMatch, useParams} from "react-router-dom";
import {QuestionsList} from "../components/QuestionsList";

export const Test = () => {
  const { id } = useParams()
  
  if (isNaN(id as any))
    return null
  
  return <Container className="mt-5">
    <QuestionsList id={id ? +id : undefined}/>
  </Container>
}