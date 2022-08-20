import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import axios from "axios";

import "./ClothesTable.scss";
import { Button } from "@mui/material";
import { DataGrid, GridColDef } from "@mui/x-data-grid";

// Base address for api
const baseAddress = "https://localhost:7167/api/";

const ClothesTable = (clothesList) => {
  // States
  let [message, setMessage] = useState(null);
  let [clothes, setClothes] = useState([]);
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
          "Clothes/Getlist/" +
          pageState.page +
          "/" +
          pageState.pageSize
      )
      .then((result) => {
        console.log(result);
        setPageState((old) => ({
          ...old,
          isLoading: false,
          data: result.data.clothes,
          total: result.data.totalItem,
        }));
      });
  }, [pageState.page, pageState.pageSize]);

  // Delete clothes
  const handleDelete = (id) => {
    axios
      .delete(baseAddress + "Clothes/" + id)
      .then((result) => {
        setMessage(result.data);
        window.location.reload(false);
        alert("Delete clothes successfully!");
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
    { field: "description", headerName: "Description", width: 210 },
    { field: "stock", headerName: "Stock", width: 70 },
    { field: "price", headerName: "Price", width: 70 },
    { field: "addedDate", headerName: "Added Date", width: 140 },
    { field: "updatedDate", headerName: "Updated Date", width: 140 },
    { field: "categoryId", headerName: "Category ID", width: 110 },
    { field: "categoryName", headerName: "Category Name", width: 140 },
    {
      field: "action",
      headerName: "Action",
      width: 180,
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

export default ClothesTable;
