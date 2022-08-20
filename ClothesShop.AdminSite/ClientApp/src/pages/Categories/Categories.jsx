import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import axios from "axios";

import "./Categories.scss";
import { Button } from "@mui/material";

import CategoriesTable from "../../components/CategoriesTable/CategoriesTable";
import Sidebar from "../../components/Sidebar/Sidebar";

// Base address for api
const baseAddress = "https://localhost:7167/api/";

const Categories = () => {
  // States

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
          <CategoriesTable />
        </div>
      </div>
    </div>
  );
};

export default Categories;
