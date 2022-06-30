import React from "react";
import { Redirect, Switch, Route, Router } from "react-router-dom";
import RouteGuard from "../components/RouteGuard/RouteGuard";

//history
import { history } from "../helpers/history";

//pages
import Home from "../pages/Home/Home";
import { Categories } from "../pages/Categories/Categories";
import Login from "../pages/Login/Login";

function Routes() {
  return (
    <Router history={history}>
      <Switch>
        <RouteGuard exact path="/" component={Home} />
        <Route path="/login" component={Login} />
        <Route path="/categories" component={Categories} />
        <Redirect to="/" />
      </Switch>
    </Router>
  );
}

export default Routes;
