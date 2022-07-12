import { useRef } from "react";
import { Link } from "react-router-dom";

import "./ClothesUpdate.scss";
import { Button } from "@mui/material";

import Sidebar from "../../components/Sidebar/Sidebar";

const ClothesUpdate = () => {
  return (
    <div className="mainContainer">
      <Sidebar />
      <div className="clothesContainer">
        <h2>CLOTHES UPDATE</h2>
        <div className="tableContainer">
          <div className="button-group">
            <Link to="/clothes" style={{ textDecoration: "none" }}>
              <Button variant="contained" color="secondary" className="button">
                Return to Index
              </Button>
            </Link>
            <div className="findGroup">
              <input
                type="text"
                placeholder="Enter Clothes name to find.."
                // ref="categoryFind"
              />
              <Button variant="contained" color="primary" className="button">
                Find
              </Button>
            </div>
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
                <span>Category name</span>
                <div>
                  <input type="text" disabled />
                </div>
              </div>
              <div className="inputGroup">
                <Button variant="contained" color="success" className="button">
                  Update Clothes
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

export default ClothesUpdate;
