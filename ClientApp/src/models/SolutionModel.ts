import {TestModel} from "./TestModel";

export interface PostSolutionModel {
  answers: string
  testId: number
}

export interface SolutionModel extends PostSolutionModel {
  id: number
  test: TestModel
}