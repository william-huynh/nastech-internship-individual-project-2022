import { useRef, useState, useEffect } from "react";
import { useHistory } from "react-router";
import { Link, useParams } from "react-router-dom";
import axios from "axios";

import "./clothesUpdate.scss";
import { Button } from "@mui/material";

import Sidebar from "../../components/Sidebar/Sidebar";

// Base address for api
const baseAddress = "https://localhost:7167/api/";

const ClothesUpdate = () => {
  // Variables
  let id = useParams();
  const history = useHistory();

  // States
  let [clothes, setClothes] = useState([]);
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
  let clothesCategoryName = useRef();

  // Get clothes
  useEffect(() => {
    axios.get(baseAddress + "Clothes/" + id.clothesId).then((result) => {
      setClothes(result.data);
      clothesId.current.value = result.data.id;
      clothesName.current.value = result.data.name;
      clothesDescription.current.value = result.data.description;
      clothesStock.current.value = result.data.stock;
      clothesPrice.current.value = result.data.price;
      clothesAddedDate.current.value = result.data.addedDate;
      clothesUpdatedDate.current.value = result.data.updatedDate;
      clothesCategoryId.current.value = result.data.categoryId;
      clothesCategoryName.current.value = result.data.categoryName;
      if (result.data.images.length > 0)
        setImageURL(baseAddress + "Images/" + result.data.images[0].id);
      else setImageURL("./dummy-image.jpg");
    });
  }, []);

  // Upload file
  const onFileChange = (event) => {
    setImageUploadFile(event.target.files[0]);
  };

  // Update clothes
  const handleUpdate = () => {
    const formData = new FormData();
    formData.append("File", imageUploadFile);
    formData.append("URL", imageUploadFile.name);
    formData.append("ClothesId", clothes.id);

    // Update clothes request
    axios
      .put(baseAddress + "Clothes", {
        Id: clothesId.current.value,
        Name: clothesName.current.value,
        Description: clothesDescription.current.value,
        Stock: clothesStock.current.value,
        Price: clothesPrice.current.value,
        CategoryId: clothesCategoryId.current.value,
      })
      .then((result) => {
        if (imageUploadFile) {
          if (clothes.images.length != 0) {
            // Update clothes image request
            axios
              .put(baseAddress + "Images/" + clothes.images[0].id, formData)
              .then(() => {
                alert("Update clothes succesfully!");
                window.location.reload(false);
              })
              .catch((error) => {
                setMessage(error.response.data);
                alert(message);
              });
          } else {
            axios
              .post(baseAddress + "Images", formData)
              .then(() => {
                alert("Update clothes succesfully!");
                window.location.reload(false);
              })
              .catch((error) => {
                setMessage(error.response.data);
                alert(message);
              });
          }
        } else {
          alert("Clothes update successfully!");
          window.location.reload(false);
        }
      });
  };

  return (
    <div className="mainContainer">
      <Sidebar />
      <div className="clothesContainer">
        <h2>UPDATE</h2>
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
              <div className="clothesInputGroup">
                <span>Category name</span>
                <div>
                  <input type="text" ref={clothesCategoryName} disabled />
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
                  onClick={handleUpdate}
                >
                  Update Clothes
                </Button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default ClothesUpdate;
