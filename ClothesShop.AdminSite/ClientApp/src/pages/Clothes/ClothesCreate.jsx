import { useRef, useState, useEffect } from "react";
import { useHistory } from "react-router";
import { Link } from "react-router-dom";
import axios from "axios";

import "./clothesCreate.scss";
import { Button } from "@mui/material";

import Sidebar from "../../components/Sidebar/Sidebar";

// Base address for api
const baseAddress = "https://localhost:7167/api/";

const ClothesCreate = (props) => {
  // Variables
  const history = useHistory();
  const nextClothesId = props.location.state.id + 1;

  // States
  let [message, setMessage] = useState(null);
  let [imageURL, setImageURL] = useState("");
  let [imageUploadFile, setImageUploadFile] = useState(null);

  // Refs
  let clothesId = useRef();
  let clothesName = useRef();
  let clothesDescription = useRef();
  let clothesStock = useRef();
  let clothesPrice = useRef();
  let clothesAddedDate = useRef();
  let clothesUpdatedDate = useRef();
  let clothesCategoryId = useRef();

  useEffect(() => {
    // Set dummy picture
    setImageURL("./dummy-image.jpg");
    // Set next clothes Id
    clothesId.current.value = nextClothesId;
  }, []);

  // Upload file
  const onFileChange = (event) => {
    setImageUploadFile(event.target.files[0]);
  };

  // Create clothes
  const handleCreate = () => {
    const formData = new FormData();
    formData.append("File", imageUploadFile);
    formData.append("URL", imageUploadFile.name);
    formData.append("ClothesId", nextClothesId);

    // Create clothes request
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
        axios
          .post(baseAddress + "Images", formData)
          .then(() => {
            alert("Create clothes succesfully!");
            history.push({
              pathname: "/clothes",
            });
          })
          .catch((error) => {
            setMessage(error.response.data);
            alert(message);
          });
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
        <h2>CREATE</h2>
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
              <div className="clothesInputGroup">
                <span>Id</span>
                <div>
                  <input
                    className="inputId"
                    type="number"
                    ref={clothesId}
                    disabled
                  />
                </div>
              </div>
              <div className="clothesInputGroup">
                <span>Name</span>
                <div>
                  <input type="text" ref={clothesName} />
                </div>
              </div>
              <div className="clothesInputGroup">
                <span>Description</span>
                <div>
                  <textarea
                    className="inputTextarea"
                    ref={clothesDescription}
                  />
                </div>
              </div>
              <div className="clothesInputGroup">
                <span>Stock</span>
                <div>
                  <input
                    className="inputNumber"
                    type="number"
                    ref={clothesStock}
                  />
                </div>
              </div>
              <div className="clothesInputGroup">
                <span>Price</span>
                <div>
                  <input
                    className="inputNumber"
                    type="number"
                    ref={clothesPrice}
                  />
                </div>
              </div>
              <div className="clothesInputGroup">
                <span>Added Date</span>
                <div>
                  <input
                    className="inputDate"
                    type="text"
                    ref={clothesAddedDate}
                    disabled
                  />
                </div>
              </div>
              <div className="clothesInputGroup">
                <span>Updated Date</span>
                <div>
                  <input
                    className="inputDate"
                    type="text"
                    ref={clothesUpdatedDate}
                    disabled
                  />
                </div>
              </div>
              <div className="clothesInputGroup">
                <span>Category Id</span>
                <div>
                  <input
                    className="inputId"
                    type="number"
                    ref={clothesCategoryId}
                  />
                </div>
              </div>
            </div>
            <div className="clothesImageContainer">
              <div className="clothesImageUpload">
                <label
                  for="imageUpload"
                  style={{
                    backgroundImage: `url(${imageURL})`,
                  }}
                />
                <input
                  type="file"
                  id="imageUpload"
                  onChange={onFileChange}
                  style={{ display: "none" }}
                />
              </div>
              <div className="clothesInputGroup">
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
          </div>
        </div>
      </div>
    </div>
  );
};

export default ClothesCreate;
