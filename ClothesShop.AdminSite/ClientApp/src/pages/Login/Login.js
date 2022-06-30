import React from "react";
import axios from "axios";
import { setAuthToken } from "../../helpers/setAuthToken";

function Login() {
  // Base api URL
  const baseAddress = "https://localhost:7167/api/";

  const handleSubmit = (Username, Password) => {
    const loginPayload = {
      Username: "customer",
      Password: "customer",
    };

    // Login request
    axios
      .post(baseAddress + "users/authenticate", loginPayload)
      .then((response) => {
        // Get token from response
        const token = response.data.token;

        // Set JWT token to local
        localStorage.setItem("token", token);

        // Set token to axios common header
        setAuthToken(token);

        // Redirect user to Home page
        window.location.href = "/";
      })
      .catch((error) => console.log(error));
  };

  return (
    <form
      onSubmit={(event) => {
        event.preventDefault();
        const [Username, Password] = event.target.children;
        handleSubmit(Username, Password);
      }}
    >
      <label for="Username">Username</label>
      <br />
      <input type="text" id="Username" name="Username" />
      <br />
      <label for="Password">Password</label>
      <br />
      <input type="password" id="Password" name="Password" />
      <br />
      <input type="submit" value="Submit" />
    </form>
  );
}

export default Login;
