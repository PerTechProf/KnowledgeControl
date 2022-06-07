import { Home } from "./Home";
import { Login } from "./Login";
import { Logout } from "./Logout";
import { Registration } from "./Registration";

export const appRoutes = {
  public: [
    {
      path: "/",
      component: Home
    }
  ],
  authOnly: {
    public: [
      
    ],
    userOnly: [
      
    ],
    employerOnly: [
      
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