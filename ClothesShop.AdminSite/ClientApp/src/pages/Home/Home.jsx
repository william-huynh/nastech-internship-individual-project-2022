import React from "react";
import Sidebar from "../../components/Sidebar/Sidebar";

const Home = () => {
  const Logout = () => {
    // Remove token
    localStorage.removeItem("token");

    // Redirect user to Login page
    window.location.href = "/login";
  };
  return (
    <div className="container">
      <Sidebar />
      <div className="mt-5 d-flex justify-content-center">
        <h2>This is Home page.</h2>
      </div>
      <button onClick={Logout} className="btn btn-primary">
        Log Out
      </button>
    </div>
  );
};

export default Home;