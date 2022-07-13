import React from "react";
import { Switch, Route, Router } from "react-router-dom";
// import RouteGuard from "../components/RouteGuard/RouteGuard";

//history
import { history } from "../helpers/history";

//pages
import Login from "../pages/Login/Login";
import Home from "../pages/Home/Home";
import Error from "../pages/Error/Error";
import Categories from "../pages/Categories/Categories";
import CategoriesUpdate from "../pages/Categories/CategoriesUpdate";
import CategoriesCreate from "../pages/Categories/CategoriesCreate";
import Clothes from "../pages/Clothes/Clothes";
import ClothesUpdate from "../pages/Clothes/ClothesUpdate";
import ClothesCreate from "../pages/Clothes/ClothesCreate";
import Users from "../pages/Users/Users";

function Routes() {
  return (
    <Router history={history}>
      <Switch>
        <Route path="/login" component={Login} />
        <Route exact path="/" component={Home} />
        {/* <RouteGuard exact path="/" component={Home} /> */}

        {/* Categories */}
        <Route path="/categories" component={Categories} />
        <Route path="/categories-create" component={CategoriesCreate} />
        {/* <Route path="/categories-update" component={CategoriesUpdate} /> */}
        <Route
          path="/categories-update/:categoriesId"
          component={CategoriesUpdate}
        />

        {/* Clothes */}
        <Route path="/clothes" component={Clothes} />
        <Route path="/clothes-create" component={ClothesCreate} />
        <Route path="/clothes-update/:clothesId" component={ClothesUpdate} />
        {/* <Route path="/clothes-update" component={ClothesUpdate} /> */}

        {/* Users */}
        <Route path="/users" component={Users} />

        <Route path="/error" component={Error} />
        {/* <Redirect to="/" /> */}
      </Switch>
    </Router>
  );
}

export default Routes;
