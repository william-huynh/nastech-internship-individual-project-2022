import { useState, useEffect } from "react";
import axios from "axios";
import { Link } from "react-router-dom";

import "./ClothesTable.scss";
import { Button } from "@mui/material";
import { DataGrid, GridColDef } from "@mui/x-data-grid";

// Base address for api
const baseAddress = "https://localhost:7167/api/";

const clothesTable = () => {
  // States
  let [clothes, setClothes] = useState([]);
  let [message, setMessage] = useState(null);

  // Get clothes list
  useEffect(() => {
    axios.get(baseAddress + "Clothes").then((result) => {
      setClothes(result.data);
    });
  }, []);

  // Delete clothes
  const handleDelete = (id) => {
    axios
      .delete(baseAddress + "Clothes/" + id)
      .then((result) => {
        setMessage(result.data);
        window.location.reload(false);
        alert("Delete clothes succesfully!");
      })
      .catch((error) => {
        setMessage(error.response.data);
        alert(message);
      });
  };

  // Table column define
  const columns: GridColDef[] = [
    { field: "id", headerName: "ID", width: 50 },
    { field: "name", headerName: "Name", width: 120 },
    { field: "description", headerName: "Description", width: 170 },
    { field: "stock", headerName: "Stock", width: 70 },
    { field: "price", headerName: "Price", width: 70 },
    { field: "addedDate", headerName: "Added Date", width: 130 },
    { field: "updatedDate", headerName: "Updated Date", width: 130 },
    { field: "categoryId", headerName: "Category ID", width: 100 },
    { field: "categoryName", headerName: "Category Name", width: 130 },
    {
      field: "action",
      headerName: "Action",
      width: 210,
      renderCell: (params) => {
        return (
          <>
            <Link
              to={"/clothes-update/" + params.row.id}
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

export default clothesTable;
