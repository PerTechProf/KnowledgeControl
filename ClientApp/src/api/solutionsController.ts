import axios from "axios";

import {PostSolutionModel, SolutionModel} from "../models/SolutionModel";

const getSolutions = (testId: number): Promise<SolutionModel[]> =>
  axios.get(`/api/Solutions/${testId}`)
    .then(response => response.data)

const postSolution = (test: PostSolutionModel) =>
  axios.post("/api/Solutions", test)

export default {
  getSolutions,
  postSolution
}