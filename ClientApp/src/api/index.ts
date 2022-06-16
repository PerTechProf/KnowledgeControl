import auth from "./authController";
import tests from "./testController";
import solutions from "./solutionsController";
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
    solutions
}