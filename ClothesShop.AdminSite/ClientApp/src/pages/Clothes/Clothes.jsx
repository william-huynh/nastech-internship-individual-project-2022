import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import axios from "axios";

import "./Clothes.scss";
import { Button } from "@mui/material";

import ClothesTable from "../../components/ClothesTable/ClothesTable";
import Sidebar from "../../components/Sidebar/Sidebar";

// Base address for api
const baseAddress = "https://localhost:7167/api/";

const Clothes = () => {
  // States
  let [clothesDeleted, setClothesDeleted] = useState([]);

  // Get clothes list
  useEffect(() => {
    axios.get(baseAddress + "Clothes/Deleted").then((result) => {
      setClothesDeleted(result.data);
    });
  }, []);

  return (
    <div className="mainContainer">
      <Sidebar />
      <div className="clothesContainer">
        <h2>CLOTHES</h2>
        <div className="tableContainer">
          <div className="button-group">
            <Link
              to={{
                pathname: "/clothes-create",
                state: clothesDeleted.at(-1),
              }}
              style={{ textDecoration: "none" }}
            >
              <Button
                variant="contained"
                className="button"
                style={{ marginBottom: 10 }}
              >
                Add new Clothes
              </Button>
            </Link>
          </div>
          <ClothesTable />
        </div>
      </div>
    </div>
  );
};

export default Clothes;
