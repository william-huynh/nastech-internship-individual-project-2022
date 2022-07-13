import { useRef, useState, useEffect } from "react";
import { useHistory } from "react-router";
import { Link, useParams } from "react-router-dom";
import axios from "axios";

import "./ClothesUpdate.scss";
import { Button } from "@mui/material";

import Sidebar from "../../components/Sidebar/Sidebar";

// Base address for api
const baseAddress = "https://localhost:7167/api/";

const ClothesUpdate = () => {
  // Variables
  let id = useParams();
  const history = useHistory();

  // States
  let [message, setMessage] = useState(null);

  // Refs
  let clothesId = useRef();
  let clothesName = useRef();
  let clothesDescription = useRef();
  let clothesStock = useRef();
  let clothesPrice = useRef();
  let clothesAddedDate = useRef();
  let clothesUpdatedDate = useRef();
  let clothesCategoryId = useRef();
  let clothesCategoryName = useRef();

  // Get clothes
  useEffect(() => {
    axios.get(baseAddress + "Clothes/" + id.clothesId).then((result) => {
      clothesId.current.value = result.data.id;
      clothesName.current.value = result.data.name;
      clothesDescription.current.value = result.data.description;
      clothesStock.current.value = result.data.stock;
      clothesPrice.current.value = result.data.price;
      clothesAddedDate.current.value = result.data.addedDate;
      clothesUpdatedDate.current.value = result.data.updatedDate;
      clothesCategoryId.current.value = result.data.categoryId;
      clothesCategoryName.current.value = result.data.categoryName;
    });
  });

  // Update clothes
  const handleUpdate = () => {
    axios
      .put(baseAddress + "Clothes", {
        ID: clothesId.current.value,
        Name: clothesName.current.value,
        Description: clothesDescription.current.value,
        Stock: clothesStock.current.value,
        Price: clothesPrice.current.value,
        AddedDate: clothesAddedDate.current.value,
        UpdatedDate: clothesUpdatedDate.current.value,
        CategoryId: clothesCategoryId.current.value,
        CategoryName: clothesCategoryName.current.value,
      })
      .then((result) => {
        setMessage(result.data);
        history.push({
          pathname: "/clothes",
        });
        alert("Update clothes succesfully!");
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
        <h2>CLOTHES UPDATE</h2>
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
                  <input type="number" ref={clothesId} disabled />
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
                <span>Category name</span>
                <div>
                  <input type="text" ref={clothesCategoryName} disabled />
                </div>
              </div>
              <div className="inputGroup">
                <Button
                  variant="contained"
                  color="success"
                  className="button"
                  onClick={handleUpdate}
                >
                  Update Clothes
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

export default ClothesUpdate;
