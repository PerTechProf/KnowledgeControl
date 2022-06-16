import {observer} from "mobx-react-lite";
import api from "../api";
import {Button, Col, Container, Form, Row, Spinner} from "react-bootstrap";
import React, {ChangeEvent, useCallback, useEffect, useState} from "react";
import authStore from "../stores/AuthStore";
import {QuestionModel} from "../models/QuestionModel";
import {TestModel} from "../models/TestModel";
import {Question} from "./Question";
import questionsStore from "../stores/QuestionsStore";
import {useNavigate} from "react-router-dom";

export const QuestionsList = observer(({id}: { id?: number }) => {
  const navigate = useNavigate()
  
  const [isLoading, setLoading] = useState(true)

  const [test, setTest] = useState<TestModel>()
  const [name, setName] = useState('')

  const questions = questionsStore.questions
  const setQuestions = questionsStore.setQuestions

  const onNameChange = (e: ChangeEvent<HTMLInputElement>) => {
    setName(e.target.value)
  }
  
  const onEdit = () => {
    if (!test || questions.some(question => question.question == ''))
      return;

    api.tests.editTest({
      ...test,
      name,
      questions: JSON.stringify(questions.map(_ => _.question)),
      answers: JSON.stringify(questions.map(_ => _.answer))
    })
  }

  const onCreate = () => {
    if (questions.some(question => question.question == ''))
      return;
    
    setLoading(true)

    api.tests.createTest({
      name: name || "Без названия",
      questions: JSON.stringify(questions.map(_ => _.question)),
      answers: JSON.stringify(questions.map(_ => _.answer))
    }).then((data) => {
      console.log(data)
      if (data.id)
        return api.tests.getTest(data.id)
      else
        throw "something wrong"
    }).then((test) => navigate(`/tests/${test.id}`))
      .finally(() => setLoading(false))
  }

  const addQuestion = () => {
    setQuestions([...questions, {question: "", answer: ""}])
  }

  const onSubmit = () => {
    if (!test?.id)
      return;
    
    api.solutions.postSolution({
      answers: JSON.stringify(questions.map(_ => _.answer)),
      testId: test.id
    }).then(_ => navigate("/tests"))
  }

  useEffect(() => {
    if (id)
      api.tests.getTest(id)
        .then(test => {
          setTest(test)
          setName(test.name)
          let testQuestions: QuestionModel[] = [];
          const questionList = JSON.parse(test.questions) as string[]
          const answerList = JSON.parse(test.answers) as string[]
          questionList.forEach((question, index) =>
            testQuestions.push({question, answer: answerList[index] ?? ''}))
          setQuestions(testQuestions)
          setLoading(false)
        })
        .catch(() => setQuestions([]))
        .finally(() => setLoading(false))
    else {
      setQuestions([{question: "", answer: ""}])
      setLoading(false)
    }
  }, [])

  if (isLoading)
    return <Spinner animation="border"/>

  return (
    <Container>
      <Row>
        <Col>
          <Form.Group>
            <Form.Control
              onChange={onNameChange}
              type="text"
              value={name}
              placeholder="Название"
              disabled={!authStore.isEmployer}/>
          </Form.Group>
        </Col>
      </Row>
      {questions?.map((question, index) =>
        <div key={index}>
          <div className="mt-3 mb-4">
            <Question
              question={question.question}
              answer={question.answer}
              index={index}
            />
          </div>
          <hr/>
        </div>
      )}
      <Row className="mt-4">
        {authStore.isEmployer ? <>
          <Col as={Button} md={4} onClick={addQuestion}>Добавить вопрос</Col>
          <Col md={4}></Col>
          <Col as={Button} md={4} onClick={id ? onEdit : onCreate}>Сохранить</Col>
        </> : <>
          <Col md={8}></Col>
          <Col as={Button} md={4} onClick={onSubmit}>Отправить</Col>
        </>}
      </Row>
    </Container>
  )
})