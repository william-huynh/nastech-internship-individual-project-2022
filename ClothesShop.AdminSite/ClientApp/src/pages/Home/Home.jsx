import "./Home.scss";
import Sidebar from "../../components/Sidebar/Sidebar";

const Home = () => {
  const Logout = () => {
    // Remove token
    localStorage.removeItem("token");

    // Redirect user to Login page
    window.location.href = "/login";
  };
  return (
    <div className="mainContainer">
      <Sidebar />
      <div className="homeContainer">Welcome to Homepage</div>
      {/* <button onClick={Logout} className="btn btn-primary">
        Log Out
      </button> */}
    </div>
  );
};

export default Home;
