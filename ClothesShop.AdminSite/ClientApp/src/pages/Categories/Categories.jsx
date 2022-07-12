import { Link } from "react-router-dom";

import "./Categories.scss";
import { Button } from "@mui/material";

import CategoriesTable from "../../components/CategoriesTable/CategoriesTable";
import Sidebar from "../../components/Sidebar/Sidebar";

const Categories = () => {
  return (
    <div className="mainContainer">
      <Sidebar />
      <div className="categoriesContainer">
        <h2>CATEGORIES</h2>
        <div className="tableContainer">
          <div className="button-group">
            <Link to="/categories-create" style={{ textDecoration: "none" }}>
              <Button
                variant="contained"
                className="button"
                style={{ marginBottom: 10 }}
              >
                Add new Category
              </Button>
            </Link>
          </div>
          <CategoriesTable />
        </div>
      </div>
    </div>
  );
};

export default Categories;

// import React, { Component } from "react";
// import axios from "axios";
// import { Button, ButtonToolbar, Table, Form, Modal } from "react-bootstrap";

// const baseAddress = "https://localhost:7167/api/";

// export class Categories extends Component {
//   constructor(props) {
//     super(props);
//     this.state = {
//       categories: [],
//       message: "",
//     };
//   }

//   refreshList() {
//     axios.get(baseAddress + "Categories").then((result) => {
//       this.setState({ categories: result.data });
//     });
//   }

//   componentDidMount() {
//     this.refreshList();
//   }

//   handleAdd = () => {
//     axios
//       .post(baseAddress + "Categories", {
//         Name: this.refs.CategoryName.value,
//         Description: this.refs.CategoryDescription.value,
//         ProductQuantity: this.refs.CategoryProductQuantity.value,
//       })
//       .then((result) => {
//         this.setState({ message: result.data });
//         window.location.assign("/categories");
//         alert("Create succesfully!");
//       })
//       .catch((error) => {
//         this.setState({ message: error.response.data });
//         alert(this.state.message);
//       });
//   };

//   handleDelete = (id) => {
//     axios
//       .delete(baseAddress + "Categories" + "/" + id)
//       .then((result) => window.location.assign("/categories"));
//   };

//   handleGetSingle = (id) => {
//     axios.get(baseAddress + "Categories" + "/" + id).then((result) => {
//       this.setState({ category: result.data });
//       console.log(result.data);
//       this.refs.CategoryUpdateId.value = result.data.id;
//       this.refs.CategoryUpdateName.value = result.data.name;
//       this.refs.CategoryUpdateDescription.value = result.data.description;
//       this.refs.CategoryUpdateProductQuantity.value =
//         result.data.productQuantity;
//     });
//   };

//   handleUpdate = () => {
//     axios
//       .put(
//         baseAddress + "Categories" + "/" + this.refs.CategoryUpdateId.value,
//         {
//           Name: this.refs.CategoryUpdateName.value,
//           Description: this.refs.CategoryUpdateDescription.value,
//           ProductQuantity: this.refs.CategoryUpdateProductQuantity.value,
//         }
//       )
//       .then((result) => {
//         this.setState({ message: result.data });
//         window.location.assign("/categories");
//         alert("Update succesfully!");
//       })
//       .catch((error) => {
//         this.setState({ message: error.response.data });
//         alert(this.state.message);
//       });
//   };

//   render() {
//     const { categories } = this.state;
//     return (
//       <div>
//         {/* DISPLAY ALL CATEGORIES */}
//         <h2 className={"d-flex justify-content-center text-primary"}>
//           CATEGORIES LIST
//         </h2>
//         <Table className="mt-4" striped bordered hover size="sm">
//           <thead>
//             <tr>
//               <th className="text-center">Id</th>
//               <th className="text-center">Name</th>
//               <th className="text-center">Description</th>
//               <th className="text-center">Product Count</th>
//               <th className="text-center">Modify</th>
//               <th className="text-center">Delete</th>
//             </tr>
//           </thead>
//           <tbody>
//             {categories.map((category) => (
//               <tr key={category.id}>
//                 <td className="text-center align-item-center">{category.id}</td>
//                 <td className="text-center">{category.name}</td>
//                 <td className="text-center">{category.description}</td>
//                 <td className="text-center">{category.productQuantity}</td>
//                 <td className="text-center">
//                   <button
//                     value={category.id}
//                     onClick={(event) =>
//                       this.handleGetSingle(event.target.value)
//                     }
//                     className="btn btn-warning"
//                   >
//                     Edit
//                   </button>
//                 </td>
//                 <td className="text-center">
//                   <button
//                     onClick={this.handleUpdate}
//                     className="btn btn-danger"
//                   >
//                     Delete
//                   </button>
//                 </td>
//               </tr>
//             ))}
//           </tbody>
//         </Table>

//         {/* ADD NEW CATEGORY */}
//         <h2 className="mt-4 d-flex justify-content-center text-warning">
//           ADD NEW CATEGORY
//         </h2>
//         <div>
//           <div className="form-group">
//             <label for="CategoryName">Category name:</label>
//             <input
//               type="text"
//               className="form-control"
//               id="CategoryName"
//               placeholder="Category name"
//               ref="CategoryName"
//             />
//           </div>
//           <div className="form-group">
//             <label for="CategoryDescription">Category description:</label>
//             <input
//               type="text"
//               className="form-control"
//               id="CategoryDescription"
//               placeholder="Category description"
//               ref="CategoryDescription"
//             />
//           </div>
//           <div className="form-group">
//             <label for="CategoryProductQuantity">Category count:</label>
//             <input
//               type="number"
//               className="form-control"
//               id="CategoryProductQuantity"
//               placeholder="Product count"
//               ref="CategoryProductQuantity"
//             />
//           </div>
//           <div className="d-flex justify-content-center mb-4">
//             <button className="btn btn-primary" onClick={this.handleAdd}>
//               Submit
//             </button>
//           </div>
//         </div>

//         {/* UPDATE CATEGORY */}
//         <h2 className="mt-4 d-flex justify-content-center text-danger">
//           UPDATE CATEGORY
//         </h2>
//         <div>
//           <div className="form-group">
//             <label for="CategoryUpdateId">Category ID:</label>
//             <input
//               type="text"
//               className="form-control"
//               id="CategoryUpdateId"
//               placeholder="Category ID"
//               ref="CategoryUpdateId"
//               readOnly
//             />
//           </div>
//         </div>
//         <div>
//           <div className="form-group">
//             <label for="CategoryUpdateName">Category name:</label>
//             <input
//               type="text"
//               className="form-control"
//               id="CategoryUpdateName"
//               placeholder="Category name"
//               ref="CategoryUpdateName"
//             />
//           </div>
//           <div className="form-group">
//             <label for="CategoryUpdateDescription">Category description:</label>
//             <input
//               type="text"
//               className="form-control"
//               id="CategoryUpdateDescription"
//               placeholder="Category description"
//               ref="CategoryUpdateDescription"
//             />
//           </div>
//           <div className="form-group">
//             <label for="CategoryUpdateProductQuantity">Category count:</label>
//             <input
//               type="number"
//               className="form-control"
//               id="CategoryUpdateProductQuantity"
//               placeholder="Product count"
//               ref="CategoryUpdateProductQuantity"
//             />
//           </div>
//           <div className="d-flex justify-content-center mb-4">
//             <button onClick={this.handleUpdate} className="btn btn-primary">
//               Submit
//             </button>
//           </div>
//         </div>
//       </div>
//     );
//   }
// }
