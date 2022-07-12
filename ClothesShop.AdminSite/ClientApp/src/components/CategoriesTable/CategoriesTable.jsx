import { useState, useEffect } from "react";
import axios from "axios";
import { Link } from "react-router-dom";

import "./CategoriesTable.scss";
import { Button } from "@mui/material";
import { DataGrid, GridColDef, GridValueGetterParams } from "@mui/x-data-grid";

// Base address for api
const baseAddress = "https://localhost:7167/api/";

// Table column define
const columns: GridColDef[] = [
  { field: "id", headerName: "ID", width: 80 },
  { field: "name", headerName: "Name", width: 150 },
  { field: "description", headerName: "Description", width: 740 },
  {
    field: "action",
    headerName: "Action",
    width: 210,
    renderCell: () => {
      return (
        <>
          <Link to="/categories-update" style={{ textDecoration: "none" }}>
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

const CategoriesTable = () => {
  // Set categories list state
  let [categories, setCategories] = useState([]);

  // Get categories list
  useEffect(() => {
    axios.get(baseAddress + "Categories").then((result) => {
      setCategories(result.data);
    });
  }, []);

  return (
    <div className="datatable">
      <DataGrid
        sx={{ fontSize: 16 }}
        rows={categories}
        columns={columns}
        pageSize={5}
        rowsPerPageOptions={[5]}
        checkboxSelection
        disableSelectionOnClick
      />
    </div>
  );
};

export default CategoriesTable;
