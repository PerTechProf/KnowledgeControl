import {action, makeAutoObservable, observable} from "mobx";
import { getCookie } from "../api";
import { AuthModel } from "../models/AuthModel";
import {QuestionModel} from "../models/QuestionModel";

export class QuestionsStore {
  @observable
  questions: QuestionModel[] = []
  
  @action
  setQuestions = (questions: QuestionModel[]): void => {
    this.questions = questions;
  }

  constructor() {
    makeAutoObservable(this);
  }
}

const questionsStore = new QuestionsStore();

export default questionsStore;