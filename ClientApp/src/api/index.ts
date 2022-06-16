import auth from "./authController";
import tests from "./testController";
import solutions from "./solutionsController";
import results from "./resultsController";

export const getCookie = (name: string): string =>
  Object.fromEntries(document.cookie
    .split('; ')
    .map(cookie => 
      cookie.split('=')
    )
  )[name];

export default {
    auth,
    tests,
    solutions,
    results
}