import { Outlet } from "react-router-dom";
import HomePage from "../pages/Home/Home";

const Layout = () => {
  return (
    <>
      <main className="flex-1">
        <Outlet>
          <HomePage />
        </Outlet>
      </main>
    </>
  );
};

export default Layout;
