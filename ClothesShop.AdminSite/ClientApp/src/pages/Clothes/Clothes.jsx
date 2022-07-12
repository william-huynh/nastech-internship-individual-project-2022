import { Link } from "react-router-dom";

import "./Clothes.scss";
import { Button } from "@mui/material";
import { DataGrid, GridColDef, GridValueGetterParams } from "@mui/x-data-grid";

import ClothesTable from "../../components/ClothesTable/ClothesTable";
import Sidebar from "../../components/Sidebar/Sidebar";

const Clothes = () => {
  return (
    <div className="mainContainer">
      <Sidebar />
      <div className="clothesContainer">
        <h2>CLOTHES</h2>
        <div className="tableContainer">
          <div className="button-group">
            <Link to="/clothes-single" style={{ textDecoration: "none" }}>
              <Button
                variant="contained"
                color="secondary"
                className="button"
                style={{ marginBottom: 10 }}
              >
                Find Clothes
              </Button>
            </Link>
            <Link to="/clothes-create" style={{ textDecoration: "none" }}>
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
