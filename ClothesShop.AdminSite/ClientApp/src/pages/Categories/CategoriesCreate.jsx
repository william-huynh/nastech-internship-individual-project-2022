import { useRef, useState } from "react";
import { useHistory } from "react-router";
import { Link } from "react-router-dom";
import axios from "axios";

import "./CategoriesCreate.scss";
import { Button } from "@mui/material";

import Sidebar from "../../components/Sidebar/Sidebar";

// Base address for api
const baseAddress = "https://localhost:7167/api/";

const categoriesCreate = () => {
  // Variables
  const history = useHistory();

  // States
  let [message, setMessage] = useState(null);

  // Refs
  let categoryName = useRef();
  let categoryDescription = useRef();

  // Create category
  const handleCreate = () => {
    axios
      .post(baseAddress + "Categories", {
        Name: categoryName.current.value,
        Description: categoryDescription.current.value,
      })
      .then((result) => {
        setMessage(result.data);
        history.push({
          pathname: "/categories",
        });
        alert("Create category succesfully!");
      })
      .catch((error) => {
        setMessage(error.response.data);
        alert(message);
      });
  };

  return (
    <div className="mainContainer">
      <Sidebar />
      <div className="categoriesContainer">
        <h2>CATEGORIES CREATE</h2>
        <div className="tableContainer">
          <div className="button-group">
            <Link to="/categories" style={{ textDecoration: "none" }}>
              <Button variant="contained" color="secondary" className="button">
                Return to Index
              </Button>
            </Link>
          </div>
          <hr />
          <div className="categoriesDetailContainer">
            <div className="categoriesInfo">
              <div className="inputGroup">
                <span>Id</span>
                <div>
                  <input type="number" disabled />
                </div>
              </div>
              <div className="inputGroup">
                <span>Name</span>
                <div>
                  <input type="text" ref={categoryName} />
                </div>
              </div>
              <div className="inputGroup">
                <span>Description</span>
                <div>
                  <input type="text" ref={categoryDescription} />
                </div>
              </div>
              <div className="inputGroup">
                <Button
                  variant="contained"
                  color="success"
                  className="button"
                  onClick={handleCreate}
                >
                  Add Categories
                </Button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default categoriesCreate;
