import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import axios from "axios";

import "./CategoriesTable.scss";
import { Button } from "@mui/material";
import { DataGrid, GridColDef } from "@mui/x-data-grid";

// Base address for api
const baseAddress = "https://localhost:7167/api/";

const CategoriesTable = (categoriesList) => {
  // States
  let [message, setMessage] = useState(null);
  const [pageState, setPageState] = useState({
    isLoading: false,
    data: [],
    total: 0,
    page: 1,
    pageSize: 4,
  });

  useEffect(() => {
    setPageState((old) => ({ ...old, isLoading: true }));
    axios
      .get(
        baseAddress +
          "Categories/Getlist/" +
          pageState.page +
          "/" +
          pageState.pageSize
      )
      .then((result) => {
        setPageState((old) => ({
          ...old,
          isLoading: false,
          data: result.data.categories,
          total: result.data.totalItem,
        }));
      });
  }, [pageState.page, pageState.pageSize]);

  // Delete category
  const handleDelete = (id) => {
    axios
      .delete(baseAddress + "Categories/" + id)
      .then((result) => {
        setMessage(result.data);
        window.location.reload(false);
        alert("Delete category successfully!");
      })
      .catch((error) => {
        setMessage(error.response.data);
        alert(message);
      });
  };

  // Table column define
  const columns: GridColDef[] = [
    { field: "id", headerName: "ID", width: 50 },
    { field: "name", headerName: "Name", width: 260 },
    { field: "description", headerName: "Description", width: 740 },
    {
      field: "action",
      headerName: "Action",
      width: 180,
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
        rows={pageState.data}
        rowCount={pageState.total}
        loading={pageState.isLoading}
        pagination
        page={pageState.page - 1}
        pageSize={pageState.pageSize}
        paginationMode="server"
        onPageChange={(newPage) => {
          setPageState((old) => ({ ...old, page: newPage + 1 }));
        }}
        onPageSizeChange={(newPageSize) =>
          setPageState((old) => ({ ...old, pageSize: newPageSize }))
        }
        columns={columns}
      />
    </div>
  );
};

export default CategoriesTable;
