import React from "react";

const Error = () => {
  const backToLogin = () => {
    // Remove token
    localStorage.removeItem("token");

    // Redirect user to Login page
    window.location.href = "/login";
  };
  return (
    <div className="container">
      <h2 className="text-center">Error</h2>
      <button className="btn btn-primary" onClick={backToLogin}>
        Back to Login
      </button>
    </div>
  );
};

export default Error;
