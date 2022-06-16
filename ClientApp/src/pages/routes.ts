import { Home } from "./Home";
import { Login } from "./Login";
import { Logout } from "./Logout";
import { Registration } from "./Registration";
import {Tests} from "./Tests";
import {Test} from "./Test";
import {Employees} from "./Employees";
import {NewTest} from "./NewTest";

export const appRoutes = {
  public: [
    {
      path: "/",
      component: Home
    }
  ],
  authOnly: {
    public: [
      {
        path: "/tests",
        component: Tests
      },
      {
        path: "/tests/:id",
        component: Test
      }
    ],
    userOnly: [
      
    ],
    employerOnly: [
      {
        path: "/tests/new",
        component: NewTest
      },
      {
        path: "/employees",
        component: Employees
      }
    ]
  },
  login: {
    path: "/login",
    component: Login
  },
  register: {
    path: "/register",
    component: Registration
  },
  logout: {
    path: "/logout",
    component: Logout
  }
};