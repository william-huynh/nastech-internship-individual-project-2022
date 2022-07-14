import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import axios from "axios";

import "./categories.scss";
import { Button } from "@mui/material";

import CategoriesTable from "../../components/CategoriesTable/CategoriesTable";
import Sidebar from "../../components/Sidebar/Sidebar";

// Base address for api
const baseAddress = "https://localhost:7167/api/";

const Categories = () => {
  // States
  let [categories, setCategories] = useState([]);

  // Get categories list
  useEffect(() => {
    axios.get(baseAddress + "Categories").then((result) => {
      setCategories(result.data);
    });
  }, []);

  return (
    <div className="mainContainer">
      <Sidebar />
      <div className="categoriesContainer">
        <h2>CATEGORIES</h2>
        <div className="tableContainer">
          <div className="button-group">
            <Link
              to={{
                pathname: "/categories-create",
                state: categories.at(-1),
              }}
              style={{ textDecoration: "none" }}
            >
              <Button
                variant="contained"
                className="button"
                style={{ marginBottom: 10 }}
              >
                Add new Category
              </Button>
            </Link>
          </div>
          <CategoriesTable categoriesList={categories} />
        </div>
      </div>
    </div>
  );
};

export default Categories;
