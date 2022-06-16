import React, {ChangeEvent, FormEvent, useRef, useState} from "react";

import {observer} from "mobx-react-lite";

import {Form} from "react-bootstrap";
import {QuestionModel} from "../models/QuestionModel";
import authStore from "../stores/AuthStore";
import questionsStore from "../stores/QuestionsStore";

interface QuestionProps extends QuestionModel {
  index: number
}

export const Question = observer(({question: propQuestion, answer: propAnswer, index}: QuestionProps) => {
  const questionInput = useRef<HTMLTextAreaElement>(null)
  const answerInput = useRef<HTMLTextAreaElement>(null)

  const onChange = () => {
    let questions = [...questionsStore.questions]
    questions[index] = {
      question: questionInput?.current?.value ?? "",
      answer: answerInput?.current?.value ?? ""
    }
    questionsStore.setQuestions(questions)
  }

  return (<>
    <Form.Group className="mb-2">
      <Form.Label>Вопрос</Form.Label>
      <Form.Control as="textarea" 
                    onChange={onChange} 
                    ref={questionInput}
                    defaultValue={propQuestion}
                    placeholder="Вопрос"
                    disabled={!authStore.isEmployer}/>
    </Form.Group>
    <Form.Group>
      <Form.Label>Ответ</Form.Label>
      <Form.Control as="textarea"
                    onChange={onChange} 
                    ref={answerInput}
                    defaultValue={propAnswer}
                    placeholder="Ответ"/>
    </Form.Group>
  </>)
})