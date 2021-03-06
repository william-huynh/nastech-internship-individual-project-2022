import "./users.scss";

import UsersTable from "../../components/UsersTable/UsersTable";
import Sidebar from "../../components/Sidebar/Sidebar";

const Users = () => {
  return (
    <div className="mainContainer">
      <Sidebar />
      <div className="usersContainer">
        <h2>USERS</h2>
        <div className="tableContainer">
          <UsersTable />
        </div>
      </div>
    </div>
  );
};

export default Users;
