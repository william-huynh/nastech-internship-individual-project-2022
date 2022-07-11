import "./Sidebar.scss";
// import DashboardIcon from "@mui/icons-material/Dashboard";

const Sidebar = () => {
  return (
    <div className="sidebar">
      <div className="top">
        <h2 className="logo">Admin</h2>
      </div>
      <hr />
      <div className="center">
        <ul>
          <li>
            {/* <DashboardIcon /> */}
            <span>Dashboard</span>
          </li>
          <li>
            <span>Dashboard</span>
          </li>
          <li>
            <span>Dashboard</span>
          </li>
          <li>
            <span>Dashboard</span>
          </li>
        </ul>
      </div>
      <div className="bottom">Option</div>
    </div>
  );
};

export default Sidebar;
