import "./Home.scss";
import Sidebar from "../../components/Sidebar/Sidebar";

const Home = () => {
  return (
    <div className="mainContainer">
      <Sidebar />
      <div className="homeContainer">
        <h2>HOMEPAGE</h2>
        <h4>Welcome to Clothes Shop Admin dashboard!</h4>
      </div>
    </div>
  );
};

export default Home;
