import axios from "axios";

import {TestViewModel} from "../models/TestViewModel";
import {TestModel} from "../models/TestModel";

const getTests = (): Promise<TestViewModel[]> =>
  axios.get("/api/Tests")
    .then(response => response.data)

const getSolvedTests = (): Promise<TestViewModel[]> =>
  axios.get("/api/Tests/solved")
    .then(response => response.data)

const getTest = (id: number): Promise<TestModel> =>
  axios.get(`/api/Tests/${id}`)
    .then(response => response.data)

const createTest = (test: TestModel): Promise<TestModel> =>
  axios.post("/api/Tests", test)
    .then(response => response.data)

const editTest = (test: TestModel) =>
  axios.put("/api/Tests", test)

const deleteTest = (id: number) =>
  axios.delete(`/api/Tests/${id}`)

export default {
  getTests,
  getSolvedTests,
  getTest,
  createTest,
  editTest,
  deleteTest
}