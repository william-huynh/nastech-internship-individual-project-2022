import { useRef, useState, useEffect } from "react";
import { Link, useParams } from "react-router-dom";
import axios from "axios";

import "./CategoriesUpdate.scss";
import { Button } from "@mui/material";

import Sidebar from "../../components/Sidebar/Sidebar";

// Base address for api
const baseAddress = "https://localhost:7167/api/";

const CategoriesUpdate = () => {
  // Get id params
  let id = useParams();
  // Set category state
  let [category, setCategory] = useState(null);
  // Input refs
  let categoryId = useRef();
  let categoryName = useRef();
  let categoryDescription = useRef();

  // Get category
  useEffect(() => {
    axios.get(baseAddress + "Categories/" + id.categoriesId).then((result) => {
      setCategory(result.data);
      categoryId.current.value = result.data.id;
      categoryName.current.value = result.data.name;
      categoryDescription.current.value = result.data.description;
    });
  }, []);

  return (
    <div className="mainContainer">
      <Sidebar />
      <div className="categoriesContainer">
        <h2>CATEGORIES UPDATE</h2>
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
                  <input type="number" ref={categoryId} disabled />
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
                <Button variant="contained" color="success" className="button">
                  Update Categories
                </Button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default CategoriesUpdate;
