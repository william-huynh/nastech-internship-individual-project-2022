import React, { Component } from "react";
import { Home } from "./components/Home/Home";
import { Categories } from "./components/Categories/Categories";
import { Layout } from "./components/Layout/Layout";
import { Route } from "react-router-dom";

import "./App.css";

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      // <div className="container">
      //   <h3 className="m-3 d-flex justify-content-center">React JS Thing</h3>
      // </div>
      <Layout>
        <Route exact path="/" component={Home} />
        <Route path="/categories" component={Categories} />
      </Layout>
    );
  }
}
