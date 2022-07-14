import { useState, useEffect } from "react";
import axios from "axios";

import "./usersTable.scss";
import { DataGrid, GridColDef } from "@mui/x-data-grid";

// Base address for api
const baseAddress = "https://localhost:7167/api/";

// Table column define
const columns: GridColDef[] = [
  { field: "id", headerName: "ID", width: 50 },
  { field: "username", headerName: "Username", width: 100 },
  { field: "passwordHash", headerName: "Password", width: 210 },
  { field: "role", headerName: "Role", width: 140 },
  { field: "name", headerName: "Name", width: 120 },
  { field: "phone", headerName: "Phone", width: 130 },
  { field: "address", headerName: "Address", width: 150 },
  { field: "email", headerName: "Email", width: 280 },
];

const UsersTable = () => {
  // Set users list state
  let [users, setUsers] = useState([]);

  // Get users list
  useEffect(() => {
    axios.get(baseAddress + "Users").then((result) => {
      setUsers(result.data);
    });
  }, []);

  return (
    <div className="datatable">
      <DataGrid
        sx={{ fontSize: 16 }}
        rows={users}
        columns={columns}
        pageSize={5}
        rowsPerPageOptions={[5]}
      />
    </div>
  );
};

export default UsersTable;
