import { useRef } from "react";
import { Link } from "react-router-dom";

import "./ClothesCreate.scss";
import { Button } from "@mui/material";

import Sidebar from "../../components/Sidebar/Sidebar";

const ClothesCreate = () => {
  return (
    <div className="mainContainer">
      <Sidebar />
      <div className="clothesContainer">
        <h2>CLOTHES CREATE</h2>
        <div className="tableContainer">
          <div className="button-group">
            <Link to="/clothes" style={{ textDecoration: "none" }}>
              <Button variant="contained" color="secondary" className="button">
                Return to Index
              </Button>
            </Link>
          </div>
          <hr />
          <div className="clothesDetailContainer">
            <div className="clothesInfo">
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
                <span>Price</span>
                <div>
                  <input type="number" />
                </div>
              </div>
              <div className="inputGroup">
                <span>Description</span>
                <div>
                  <input type="text" />
                </div>
              </div>
              <div className="inputGroup">
                <span>Added Date</span>
                <div>
                  <input type="text" disabled />
                </div>
              </div>
              <div className="inputGroup">
                <span>Updated Date</span>
                <div>
                  <input type="text" disabled />
                </div>
              </div>
              <div className="inputGroup">
                <span>Category Id</span>
                <div>
                  <input type="number" />
                </div>
              </div>
              <div className="inputGroup">
                <Button variant="contained" color="success" className="button">
                  Add Clothes
                </Button>
              </div>
            </div>
            <div className="clothesImage">
              <img src="" />
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default ClothesCreate;
