import React, { Component } from "react";
import axios from "axios";
import { Table } from "reactstrap";

export class Categories extends Component {
  constructor(props) {
    super(props);
    this.state = {
      categories: [],
    };
  }

  refreshList() {
    axios.get("https://localhost:7167/api/Categories").then((result) => {
      this.setState({ categories: result.data });
      console.log(result);
    });
    // fetch("https://localhost:7167/api/Categories")
    //   .then((response) => response.json)
    //   .then((data) => {
    //     this.setState({ categories: data.data });
    //     console.log(data.data);
    //   });
  }

  componentDidMount() {
    this.refreshList();
  }

  // componentDidUpdate() {
  //   this.refreshList();
  // }

  render() {
    const { categories } = this.state;
    return (
      <div>
        <Table className="mt-4" striped bordered hover size="sm">
          <thead>
            <tr>
              <th>Id</th>
              <th>Name</th>
              <th>Description</th>
              <th>Product Count</th>
              <th>Options</th>
            </tr>
          </thead>
          <tbody>
            {categories.map((category) => (
              <tr key={category.id}>
                <td>{category.id}</td>
                <td>{category.name}</td>
                <td>{category.description}</td>
                <td>{category.productQuantity}</td>
                <td>Edit / Delete</td>
              </tr>
            ))}
          </tbody>
        </Table>
      </div>
    );
  }
}
