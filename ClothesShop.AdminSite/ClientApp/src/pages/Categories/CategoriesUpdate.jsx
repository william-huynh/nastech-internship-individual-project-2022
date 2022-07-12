import { useRef } from "react";
import { Link } from "react-router-dom";

import "./CategoriesUpdate.scss";
import { Button } from "@mui/material";

import Sidebar from "../../components/Sidebar/Sidebar";

const CategoriesUpdate = () => {
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
            <div className="findGroup">
              <input
                type="text"
                placeholder="Enter categories name to find.."
                // ref="categoryFind"
              />
              <Button variant="contained" color="primary" className="button">
                Find
              </Button>
            </div>
          </div>
          <hr />
          <div className="categoriesDetailContainer">
            <div className="categoriesInfo">
              <div className="inputGroup">
                <span>Id</span>
                <div>
                  <input type="number" />
                </div>
              </div>
              <div className="inputGroup">
                <span>Name</span>
                <div>
                  <input type="text" />
                </div>
              </div>
              <div className="inputGroup">
                <span>Description</span>
                <div>
                  <input type="text" />
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
