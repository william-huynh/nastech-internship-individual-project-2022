import { useRef, useState, useEffect } from "react";
import { Link, useParams } from "react-router-dom";
import axios from "axios";

import "./CategoriesUpdate.scss";
import { Button } from "@mui/material";

import Sidebar from "../../components/Sidebar/Sidebar";

// Base address for api
const baseAddress = "https://localhost:7167/api/";

const CategoriesUpdate = () => {
  // Variables
  let id = useParams();

  // States
  let [message, setMessage] = useState(null);

  // Refs
  let categoryId = useRef();
  let categoryName = useRef();
  let categoryDescription = useRef();

  // Get category
  useEffect(() => {
    axios.get(baseAddress + "Categories/" + id.categoriesId).then((result) => {
      categoryId.current.value = result.data.id;
      categoryName.current.value = result.data.name;
      categoryDescription.current.value = result.data.description;
    });
  }, []);

  // Update category
  const handleUpdate = () => {
    let config = {
      headers: {
        authorization: "Bearer " + localStorage.getItem("token"),
      },
    };

    axios
      .put(
        baseAddress + "Categories",
        {
          Id: categoryId.current.value,
          Name: categoryName.current.value,
          Description: categoryDescription.current.value,
        },
        config
      )
      .then((result) => {
        alert("Update category succesfully!");
        window.location.reload(false);
      })
      .catch((error) => {
        setMessage(error.response.data);
        alert(message);
      });
  };

  return (
    <div className="mainContainer">
      <Sidebar />
      <div className="categoriesContainer">
        <h2>UPDATE</h2>
        <div className="tableContainer">
          <div className="button-group">
            <Link to="/categories" style={{ textDecoration: "none" }}>
              <Button variant="contained" color="secondary" className="button">
                Return to Index
              </Button>
            </Link>
          </div>
          <hr />
          <div className="categoriesDetailContainer">
            <div className="categoriesInfo">
              <div className="categoriesInputGroup">
                <span>Id</span>
                <div>
                  <input
                    type="number"
                    className="inputId"
                    ref={categoryId}
                    disabled
                  />
                </div>
              </div>
              <div className="categoriesInputGroup">
                <span>Name</span>
                <div>
                  <input type="text" ref={categoryName} />
                </div>
              </div>
              <div className="categoriesInputGroup">
                <span>Description</span>
                <div>
                  <textarea
                    className="inputTextarea"
                    type="text"
                    ref={categoryDescription}
                  />
                </div>
              </div>
              <div className="categoriesInputGroup">
                <Button
                  variant="contained"
                  color="success"
                  className="button"
                  onClick={handleUpdate}
                >
                  Update Category
                </Button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default CategoriesUpdate;
