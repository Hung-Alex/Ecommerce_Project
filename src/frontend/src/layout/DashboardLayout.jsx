import React from "react";
import SideNav from "../components/SideNav";
import AdminNavbar from "../pages/Admin/comp/AdminNavbar";

const DashboardLayout = ({ children }) => {
  return (
    <>
    <div className=" min-h-screen  bg-slate-100">
      <AdminNavbar />
      <div className="flex justify-center items-start">
      <SideNav />
      {children}
      </div>
    </div>
    </>
  );
};

export default DashboardLayout;
