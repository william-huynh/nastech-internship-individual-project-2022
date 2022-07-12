import { useState, useEffect } from "react";
import axios from "axios";

import "./UsersTable.scss";
import { Button } from "@mui/material";
import { DataGrid, GridColDef, GridValueGetterParams } from "@mui/x-data-grid";

// Base address for api
const baseAddress = "https://localhost:7167/api/";

// Table column define
const columns: GridColDef[] = [
  { field: "id", headerName: "ID", width: 80 },
  { field: "username", headerName: "Username", width: 120 },
  { field: "password", headerName: "Password", width: 120 },
  { field: "role", headerName: "Role", width: 100 },
  { field: "name", headerName: "Name", width: 120 },
  { field: "phone", headerName: "Phone", width: 130 },
  { field: "address", headerName: "Address", width: 150 },
  { field: "email", headerName: "Email", width: 150 },
  {
    field: "action",
    headerName: "Action",
    width: 210,
    renderCell: () => {
      return (
        <>
          <Button variant="contained" color="success" className="button">
            Edit
          </Button>
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

const UsersTable = () => {
  // Set users list state
  let [users, setUsers] = useState([]);

  // Get users list
  useEffect(() => {
    axios.get(baseAddress + "Users").then((result) => {
      setUsers(result.data);
    });
    console.log(users);
  }, []);

  return (
    <div className="datatable">
      <DataGrid
        sx={{ fontSize: 16 }}
        rows={users}
        columns={columns}
        pageSize={5}
        rowsPerPageOptions={[5]}
        checkboxSelection
        disableSelectionOnClick
      />
    </div>
  );
};

export default UsersTable;
