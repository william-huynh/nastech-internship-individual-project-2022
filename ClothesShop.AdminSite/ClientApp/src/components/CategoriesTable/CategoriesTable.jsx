import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import axios from "axios";

import "./categoriesTable.scss";
import { Button } from "@mui/material";
import { DataGrid, GridColDef } from "@mui/x-data-grid";

// Base address for api
const baseAddress = "https://localhost:7167/api/";

const CategoriesTable = () => {
  // States
  let [categories, setCategories] = useState([]);
  let [message, setMessage] = useState(null);

  // Get categories list
  useEffect(() => {
    axios.get(baseAddress + "Categories").then((result) => {
      setCategories(result.data);
    });
  }, []);

  // Delete category
  const handleDelete = (id) => {
    axios
      .delete(baseAddress + "Categories/" + id)
      .then((result) => {
        setMessage(result.data);
        window.location.reload(false);
        alert("Delete category succesfully!");
      })
      .catch((error) => {
        setMessage(error.response.data);
        alert(message);
      });
  };

  // Table column define
  const columns: GridColDef[] = [
    { field: "id", headerName: "ID", width: 50 },
    { field: "name", headerName: "Name", width: 180 },
    { field: "description", headerName: "Description", width: 740 },
    {
      field: "action",
      headerName: "Action",
      width: 210,
      renderCell: (params) => {
        return (
          <>
            <Link
              to={"/categories-update/" + params.row.id}
              style={{ textDecoration: "none" }}
            >
              <Button variant="contained" color="success" className="button">
                Edit
              </Button>
            </Link>
            <Button
              variant="contained"
              color="error"
              className="button"
              style={{ marginLeft: 10 }}
              onClick={() => handleDelete(params.row.id)}
            >
              Delete
            </Button>
          </>
        );
      },
    },
  ];

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
