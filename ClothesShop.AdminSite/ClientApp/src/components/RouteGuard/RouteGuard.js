import React from "react";
import { Route, Redirect } from "react-router-dom";
import { isExpired, decodeToken } from "react-jwt";

const RouteGuard = ({ component: Component, ...rest }) => {
  function hasJWT() {
    let flag = false;

    let token = localStorage.getItem("token");
    let decodedToken = decodeToken(token).Role;

    // Check if user has JWT token and authorize
    if (token != null && decodedToken == "Administrator") {
      flag = true;
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
          <Redirect to={{ pathname: "/error" }} />
        )
      }
    />
  );
};

export default RouteGuard;
