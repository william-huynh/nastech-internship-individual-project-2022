import { Link } from "react-router-dom";

import "./sidebar.scss";
import TokenIcon from "@mui/icons-material/Token";
import DashboardIcon from "@mui/icons-material/Dashboard";
import CategoryIcon from "@mui/icons-material/Category";
import CheckroomIcon from "@mui/icons-material/Checkroom";
import PersonIcon from "@mui/icons-material/Person";
import LogoutIcon from "@mui/icons-material/Logout";

const Sidebar = () => {
  // Logout function
  const Logout = () => {
    // Remove token
    localStorage.removeItem("token");

    // Redirect user to Login page
    window.location.href = "/login";
  };

  return (
    <div className="sidebar">
      <div className="top">
        <TokenIcon className="logo" />
      </div>
      <hr />
      <div className="center">
        <ul>
          <Link to="/" style={{ textDecoration: "none" }}>
            <li>
              <DashboardIcon className="icon" />
              <span>Dashboard</span>
            </li>
          </Link>
          <Link to="/categories" style={{ textDecoration: "none" }}>
            <li>
              <CategoryIcon className="icon" />
              <span>Categories</span>
            </li>
          </Link>
          <Link to="/clothes" style={{ textDecoration: "none" }}>
            <li>
              <CheckroomIcon className="icon" />
              <span>Clothes</span>
            </li>
          </Link>
          <Link to="/users" style={{ textDecoration: "none" }}>
            <li>
              <PersonIcon className="icon" />
              <span>Users</span>
            </li>
          </Link>
          <li onClick={Logout}>
            <LogoutIcon className="icon" />
            <span>Log out</span>
          </li>
        </ul>
      </div>
    </div>
  );
};

export default Sidebar;
