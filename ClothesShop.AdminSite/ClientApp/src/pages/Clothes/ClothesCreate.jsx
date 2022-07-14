import { useRef, useState } from "react";
import { useHistory } from "react-router";
import { Link } from "react-router-dom";
import axios from "axios";

import "./ClothesCreate.scss";
import { Button } from "@mui/material";

import Sidebar from "../../components/Sidebar/Sidebar";

// Base address for api
const baseAddress = "https://localhost:7167/api/";

const clothesCreate = () => {
  // Variables
  const history = useHistory();

  // States
  let [message, setMessage] = useState(null);

  // Refs
  let clothesName = useRef();
  let clothesDescription = useRef();
  let clothesStock = useRef();
  let clothesPrice = useRef();
  let clothesAddedDate = useRef();
  let clothesUpdatedDate = useRef();
  let clothesCategoryId = useRef();

  // Create clothes
  const handleCreate = () => {
    axios
      .post(baseAddress + "Clothes", {
        Name: clothesName.current.value,
        Description: clothesDescription.current.value,
        Stock: clothesStock.current.value,
        Price: clothesPrice.current.value,
        AddedDate: "2022-01-01T00:00:00.000Z",
        UpdatedDate: "2022-01-01T00:00:00.000Z",
        CategoryId: clothesCategoryId.current.value,
        CategoryName: "",
      })
      .then((result) => {
        setMessage(result.data);
        history.push({
          pathname: "/clothes",
        });
        alert("Create clothes succesfully!");
      })
      .catch((error) => {
        setMessage(error.response.data);
        alert(message);
      });
  };

  return (
    <div className="mainContainer">
      <Sidebar />
      <div className="clothesContainer">
        <h2>CLOTHES CREATE</h2>
        <div className="tableContainer">
          <div className="button-group">
            <Link to="/clothes" style={{ textDecoration: "none" }}>
              <Button variant="contained" color="secondary" className="button">
                Return to Index
              </Button>
            </Link>
          </div>
          <hr />
          <div className="clothesDetailContainer">
            <div className="clothesInfo">
              <div className="inputGroup">
                <span>Id</span>
                <div>
                  <input type="number" disabled />
                </div>
              </div>
              <div className="inputGroup">
                <span>Name</span>
                <div>
                  <input type="text" ref={clothesName} />
                </div>
              </div>
              <div className="inputGroup">
                <span>Description</span>
                <div>
                  <input type="text" ref={clothesDescription} />
                </div>
              </div>
              <div className="inputGroup">
                <span>Stock</span>
                <div>
                  <input type="number" ref={clothesStock} />
                </div>
              </div>
              <div className="inputGroup">
                <span>Price</span>
                <div>
                  <input type="number" ref={clothesPrice} />
                </div>
              </div>
              <div className="inputGroup">
                <span>Added Date</span>
                <div>
                  <input type="text" ref={clothesAddedDate} disabled />
                </div>
              </div>
              <div className="inputGroup">
                <span>Updated Date</span>
                <div>
                  <input type="text" ref={clothesUpdatedDate} disabled />
                </div>
              </div>
              <div className="inputGroup">
                <span>Category Id</span>
                <div>
                  <input type="number" ref={clothesCategoryId} />
                </div>
              </div>
              <div className="inputGroup">
                <Button
                  variant="contained"
                  color="success"
                  className="button"
                  onClick={handleCreate}
                >
                  Add Clothes
                </Button>
              </div>
            </div>
            <div className="clothesImage">
              <img src="" alt="this is clothes" />
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default clothesCreate;
