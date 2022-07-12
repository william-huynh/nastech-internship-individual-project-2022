import { useState, useEffect } from "react";
import axios from "axios";
import { Link } from "react-router-dom";

import "./ClothesTable.scss";
import { Button } from "@mui/material";
import { DataGrid, GridColDef, GridValueGetterParams } from "@mui/x-data-grid";

// Base address for api
const baseAddress = "https://localhost:7167/api/";

// Table column define
const columns: GridColDef[] = [
  { field: "id", headerName: "ID", width: 80 },
  { field: "name", headerName: "Name", width: 100 },
  { field: "description", headerName: "Description", width: 180 },
  { field: "price", headerName: "Price", width: 100 },
  { field: "addedDate", headerName: "Added Date", width: 180 },
  { field: "updatedDate", headerName: "Updated Date", width: 180 },
  { field: "categoryId", headerName: "Category ID", width: 150 },
  {
    field: "action",
    headerName: "Action",
    width: 210,
    renderCell: () => {
      return (
        <>
          <Link to="/clothes-update" style={{ textDecoration: "none" }}>
            <Button variant="contained" color="success" className="button">
              Edit
            </Button>
          </Link>
          <Button
            variant="contained"
            color="error"
            className="button"
            style={{ marginLeft: 10 }}
          >
            Delete
          </Button>
        </>
      );
    },
  },
];

const ClothesTable = () => {
  // Set clothes list state
  let [clothes, setClothes] = useState([]);

  // Get clothes list
  useEffect(() => {
    axios.get(baseAddress + "Clothes").then((result) => {
      setClothes(result.data);
    });
  }, []);

  return (
    <div className="datatable">
      <DataGrid
        sx={{ fontSize: 16 }}
        rows={clothes}
        columns={columns}
        pageSize={5}
        rowsPerPageOptions={[5]}
        checkboxSelection
        disableSelectionOnClick
      />
    </div>
  );
};

export default ClothesTable;
