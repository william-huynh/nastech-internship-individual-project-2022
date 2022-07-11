import React from "react";
import { Route, Redirect } from "react-router-dom";
import { decodeToken } from "react-jwt";

const RouteGuard = ({ component: Component, ...rest }) => {
  function hasJWT() {
    let flag = false;

    let token = localStorage.getItem("token");

    // Check if user has JWT token and authorize
    if (token != null) {
      if (decodeToken(token) === "Administrator") {
        flag = true;
      } else {
        flag = false;
      }
    } else {
      flag = false;
    }

    return flag;
  }

  return (
    <Route
      {...rest}
      render={(props) =>
        hasJWT() ? (
          <Component {...props} />
        ) : (
          <Redirect to={{ pathname: "/login" }} />
        )
      }
    />
  );
};

export default RouteGuard;
